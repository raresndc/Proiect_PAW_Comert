using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PAW_comert
{
    public partial class Dashboard : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public Dashboard()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            panelNav.Height = buttonDashboard.Height;
            panelNav.Top = buttonDashboard.Top;
            panelNav.Left = buttonDashboard.Left;
            buttonDashboard.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            panelNav.Height = buttonDashboard.Height;
            panelNav.Top = buttonDashboard.Top;
            panelNav.Left = buttonDashboard.Left;
            buttonDashboard.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonAnalystics_Click(object sender, EventArgs e)
        {
            panelNav.Height = buttonAnalystics.Height;
            panelNav.Top = buttonAnalystics.Top;
            buttonAnalystics.BackColor = Color.FromArgb(46, 51, 73);
            new Analytics().Show();
        }

        private void buttonData_Click(object sender, EventArgs e)
        {
            panelNav.Height = buttonData.Height;
            panelNav.Top = buttonData.Top;
            buttonData.BackColor = Color.FromArgb(46, 51, 73);
            new Data().Show();
        }

        private void buttonContact_Click(object sender, EventArgs e)
        {
            panelNav.Height = buttonContact.Height;
            panelNav.Top = buttonContact.Top;
            buttonContact.BackColor = Color.FromArgb(46, 51, 73);
            new Contact().Show();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            panelNav.Height = buttonSettings.Height;
            panelNav.Top = buttonSettings.Top;
            buttonSettings.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonDashboard_Leave(object sender, EventArgs e)
        {
            buttonDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void buttonAnalystics_Leave(object sender, EventArgs e)
        {
            buttonAnalystics.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void buttonCalendar_Leave(object sender, EventArgs e)
        {
            buttonData.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void buttonContact_Leave(object sender, EventArgs e)
        {
            buttonContact.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void buttonSettings_Leave(object sender, EventArgs e)
        {
            buttonSettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
        }
    }
}
