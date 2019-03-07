using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs_go_wallhack {
    public partial class GUI : Form {

        public GUI() {
            InitializeComponent();
        }

        private void EnableBtn_Click(object sender, EventArgs e) {
            Cheat.running = !Cheat.running;
            if (!Cheat.running)
                EnableBtn.Text = "Enable";
            else
                EnableBtn.Text = "Disable";
        }
    }
}
