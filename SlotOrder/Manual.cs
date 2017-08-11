using System;
using System.Windows.Forms;

namespace SlotOrder
{
    public partial class Manual : Form
    {
        public Manual()
        {
            InitializeComponent();
            textBox1.GotFocus += TextBox1_GotFocus;
        }

        private void TextBox1_GotFocus(object sender, EventArgs e)
        {
            Focus();
        }
    }
}
