using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ArmaClassParser;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SlotOrder
{
    public partial class Form1 : Form
    {
        private Document _mission;
        private readonly Image _arrowDown, _arrowUp;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            progressBar1.MarqueeAnimationSpeed = 10;
            groupsPanel.WrapContents = false;
            groupsPanel.HorizontalScroll.Visible = true;

            var config = new LoggingConfiguration();

            var file = new FileTarget("file") {FileName = "logs/log.log"};

            var rule = new LoggingRule("*", LogLevel.Info, file);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var arrowDownStream = assembly.GetManifestResourceStream("SlotOrder.arrow_down.gif") ??
                                  throw new NullReferenceException("arrow_down.gif not found");
            _arrowDown = Image.FromStream(arrowDownStream);
            var arrowUpStream = assembly.GetManifestResourceStream("SlotOrder.arrow_up.gif") ??
                                  throw new NullReferenceException("arrow_up.gif not found");
            _arrowUp = Image.FromStream(arrowUpStream);
        }

        private void ChoosePath_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog {Filter = @"Mission files (mission.sqm)|mission.sqm"};
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            PathBox.Text = dialog.FileName;
            StartLoadMission();
        }

        private void StartLoadMission()
        {
            groupsPanel.Controls.Clear();
            unitsPanel.Controls.Clear();
            saveButton.Text = @"Save";
            saveButton.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            var t = new Thread(LoadMission);
            t.Start();
        }

        private void LoadMission()
        {
            try
            {
                var directory = new FileInfo(PathBox.Text).DirectoryName;

                if (DeRapUtility.IsBinarized(directory))
                    if (!DeRapUtility.DeRap(directory, out directory))
                    {
                        MessageBox.Show(@"Couldn't DeRap mission. Is DeRap installed?");
                    }

                _mission = ConfigParser.Parse(directory, new FileInfo(PathBox.Text).Name);
                if (_mission == null)
                {
                    MessageBox.Show(@"Failed to load mission. See log file for details.");
                    return;
                }
                var name = _mission.GetClass("Mission").GetClass("Intel").GetString("briefingName", null) ??
                           "No Mission Name";
                Invoke((Action) (() =>
                {
                    Text = name;
                }));
                UpdatePanels();
            }
            catch (Exception e)
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                MessageBox.Show(@"Unexpected error. See log file for details.");
                LogManager.GetCurrentClassLogger().Error(e);
            }
        }

        private void UpdatePanels(int selectedGroupIndex = -1, int selectedUnitIndex = -1)
        {
            var entities = _mission.GetClass("Mission").GetClass("Entities");

            var groups = new List<Group>();

            for (var i = 0; i < entities.Count(); i++)
            {
                var entity = entities[i] as Class;
                if (entity == null) continue;
                if (entity.GetString("dataType", "") != "Group") continue;
                if (!entity.HasClass("Entities")) continue;

                string groupname = null;

                var elements = entity.GetClass("Entities");
                var units = new List<Unit>();

                for (var j = 0; j < elements.Count(); j++)
                {
                    var element = elements[j] as Class;
                    if (element == null) continue;
                    if (element.GetString("dataType", "") != "Object") continue;
                    if (element.GetClass("Attributes").GetNumber("isPlayer", 0) < 0.5 &&
                        element.GetClass("Attributes").GetNumber("isPlayable", 0) < 0.5) continue;
                    var unit = new Unit
                    {
                        Description = element.GetClass("Attributes").GetString("description", ""),
                        ConfigClass = element,
                        Index = j
                    };
                    var init = element.GetClass("Attributes").GetString("init", "");
                    var groupnamematch = Regex.Match(init, @"this setGroupid \[""([^""]*)""\]");
                    if (groupnamematch.Success)
                    {
                        groupname = groupnamematch.Groups[1].Value;
                    }
                    units.Add(unit);
                }

                if (units.Count <= 0) continue;
                var group = new Group
                {
                    Groupname = groupname,
                    Index = i,
                    Units = units,
                    Side = entity.GetString("side", ""),
                    ConfigClass = entity
                };
                groups.Add(group);
            }

            Invoke((Action) (() =>
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                groupsPanel.Controls.Clear();
                unitsPanel.Controls.Clear();

                Group selectedGroup = null;

                for (var i = 0; i < groups.Count; i++)
                {
                    var group = groups[i];
                    var click = (EventHandler) ((s, a) =>
                    {
                        UpdatePanels(group.Index);
                    });
                    EventHandler up = null, down = null;
                    if (i > 0)
                        up = SwapContentsAction(group.ConfigClass, groups[i - 1].ConfigClass, groups[i - 1].Index, selectedUnitIndex);
                    if (i < groups.Count - 1)
                        down = SwapContentsAction(group.ConfigClass, groups[i + 1].ConfigClass, groups[i + 1].Index, selectedUnitIndex);
                    var color = GetColor(group.Side, group.Index == selectedGroupIndex);
                    if (group.Index == selectedGroupIndex)
                        selectedGroup = group;
                    AddEntry(groupsPanel, group.Groupname, color, click, up, down, i != 0, i != groups.Count - 1);
                    Application.DoEvents();
                }

                if (selectedGroup == null)
                {
                    reloadButton.Enabled = true;
                    return;
                }
                for (var i = 0; i < selectedGroup.Units.Count; i++)
                {
                    var unit = selectedGroup.Units[i];
                    var click = (EventHandler) ((s, a) => { UpdatePanels(selectedGroup.Index, unit.Index); });
                    EventHandler up = null, down = null;
                    if (i > 0)
                        up = SwapContentsAction(unit.ConfigClass, selectedGroup.Units[i - 1].ConfigClass,
                            selectedGroupIndex, selectedGroup.Units[i - 1].Index);
                    if (i < selectedGroup.Units.Count - 1)
                        down = SwapContentsAction(unit.ConfigClass, selectedGroup.Units[i + 1].ConfigClass,
                            selectedGroupIndex, selectedGroup.Units[i + 1].Index);
                    var color = GetColor(selectedGroup.Side, unit.Index == selectedUnitIndex);
                    AddEntry(unitsPanel, unit.Description, color, click, up, down, i != 0,
                        i != selectedGroup.Units.Count - 1);
                    Application.DoEvents();
                }

                reloadButton.Enabled = true;
            }));
        }

        private EventHandler SwapContentsAction(Class item1, Class item2, int selectedGroupIndex, int selectedUnitIndex)
        {
            void Handler(object sender, EventArgs args)
            {
                Invoke((Action) (() =>
                {
                    saveButton.Enabled = true;
                    saveButton.Text = @"*Save*";
                }));

                item1.Swap(item2);

                UpdatePanels(selectedGroupIndex, selectedUnitIndex);
            }

            return Handler;
        }

        private static Color GetColor(string side, bool highlighted = false)
        {
            switch (side.ToLower())
            {
                case "west":
                    return highlighted ? Color.FromArgb(255, 0, 114, 230): Color.FromArgb(255, 110, 134, 250);
                case "east":
                    return highlighted ? Color.FromArgb(255, 191, 0, 0) : Color.FromArgb(255, 191, 90, 90);
                case "resistance":
                case "independent":
                    return highlighted ? Color.FromArgb(255, 0, 191, 0) : Color.FromArgb(255, 70, 250, 70);
                case "civilian":
                    return highlighted ? Color.FromArgb(255, 153, 0, 191) : Color.FromArgb(255, 153, 100, 191);
                default:
                    return highlighted ? Color.White : DefaultBackColor;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartLoadMission();
        }

        private void AddEntry(FlowLayoutPanel parent, string text, Color bgcolor, EventHandler click, EventHandler upAction, EventHandler downAction, bool showUp = true, bool showDown = true)
        {
            var panel = new Panel
            {
                BackColor = bgcolor,
                BorderStyle = BorderStyle.FixedSingle,
                Width = parent.Width - 10,
                Height = 25
            };
            panel.Click += click;

            var description = new Label
            {
                Width = panel.Width - 2 * 40 - 5,
                Height = 15,
                Left = 2,
                Top = 5,
                Text = text
            };
            description.Click += click;
            panel.Controls.Add(description);

            if (showUp)
            {
                var up = new Button
                {
                    Width = 40,
                    Height = 20,
                    Left = description.Width,
                    Top = 2,
                    BackColor = DefaultBackColor,
                    Image = _arrowUp
                };
                up.Click += upAction;
                panel.Controls.Add(up);
            }
            
            if (showDown)
            {
                var down = new Button
                {
                    Width = 40,
                    Height = 20,
                    Left = description.Width + 40,
                    Top = 2,
                    BackColor = DefaultBackColor,
                    Image = _arrowDown
                };
                down.Click += downAction;
                panel.Controls.Add(down);
            }

            parent.Controls.Add(panel);
        }

        private class Group
        {
            public List<Unit> Units;
            public string Groupname, Side;
            public int Index;
            public Class ConfigClass;

            public Group()
            {
                Units = new List<Unit>();
            }
        }

        private class Unit
        {
            public string Description;
            public Class ConfigClass;
            public int Index;
        }

        private void Path_TextChanged(object sender, EventArgs e)
        {
            reloadButton.Enabled = false;
            saveButton.Enabled = false;
            saveButton.Text = @"Save";
            groupsPanel.Controls.Clear();
            unitsPanel.Controls.Clear();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var directory = new FileInfo(PathBox.Text).DirectoryName;
            if (directory == null)
                throw new ArgumentException("Path not found");
            var backupNumber = 0;
            while (backupNumber < 1000)
            {
                if (!File.Exists(Path.Combine(directory, "mission.sqm.backup" + backupNumber)))
                    break;
                backupNumber++;
            }
            if (backupNumber == 1000)
            {
                MessageBox.Show(@"1000 Backups? That can't be right. Something's gotta be broken.");
                return;
            }
            File.Copy(PathBox.Text, Path.Combine(directory, "mission.sqm.backup" + backupNumber));
            File.WriteAllText(PathBox.Text, _mission.GenerateCode());
            saveButton.Text = @"Save";
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Keithenneu/slotorder/issues");
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Manual().ShowDialog();
        }

        private void versionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Mk1", @"Version");
        }
    }
}
