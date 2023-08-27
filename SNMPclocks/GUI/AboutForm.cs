using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNMPclocks.GUI
{
    public partial class AboutForm : Form
    {

        public AboutForm() => InitializeComponent();

        private void closeButton_Click(object sender, EventArgs e) => Close();

        private void githubLinkLabel_Click(object sender, EventArgs e) => openUrl(githubLinkLabel.Text);
        private void thirdpartyIcons8LinkLabel_Click(object sender, EventArgs e) => openUrl(thirdpartyIcons8LinkLabel.Text);
        private void thirdpartySharpsnmpLinkLabel_Click(object sender, EventArgs e) => openUrl(thirdpartySharpsnmpLinkLabel.Text);

        private static void openUrl(string url)
            => System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });

    }
}
