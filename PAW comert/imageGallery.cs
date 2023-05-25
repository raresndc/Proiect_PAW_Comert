using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAW_comert
{
    public partial class imageGallery : Form
    {
        int i = 1;

        public imageGallery()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void goNext(object sender, EventArgs e)
        {
            i++;

            if(i>2)
            {
                i = 1;
            }

            changeImage(i);
        }

        private void goBack(object sender, EventArgs e)
        {
            i--;

            if(i<1)
            {
                i = 2;
            }

            changeImage(i);
        }

        private void changeImage(int num)
        {
            switch(num)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.comanda_formular_a4_7528888;
                    break;
                case 2:
                    pictureBox1.Image = Properties.Resources.dispozitie_de_plata_incasare_catre_casierie;
                    break;
            }
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            printBarCode();
        }

        private void printBarCode()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;
            pd.Document = doc;
            if(pd.ShowDialog()==DialogResult.OK)
            {
                doc.Print();
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bm, new Rectangle(0,0,pictureBox1.Width, pictureBox1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            bm.Dispose();
        }
    }
}
