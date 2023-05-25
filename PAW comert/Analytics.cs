using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAW_comert
{
    public partial class Analytics : Form
    {
        public Analytics()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAuchan_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void buttonCarrefour_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void buttonCora_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void buttonKaufland_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void buttonLidl_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void buttonMega_Image_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void buttonMetro_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void buttonPenny_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void buttonProfi_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void buttonSelgros_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Analytics_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Desfaceri' table. You can move, or remove it, as needed.
            this.desfaceriTableAdapter.Fill(this.database1DataSet.Desfaceri);

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            chart1.Series[0].XValueMember = "Magazine";
            chart1.Series[0].YValueMembers = "Magazine";

            chart1.Series[1].XValueMember = "Products";
            chart1.Series[1].YValueMembers = "Products";

            chart1.DataSource = database1DataSet.Desfaceri;
            chart1.DataBind();
        }
    }
}
