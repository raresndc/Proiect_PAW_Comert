using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PAW_comert
{
    
    public partial class Data : Form
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

        private const string magazinDefault = "Na";
        private const string raionDefault = "Na";

        private int count = 0;

        private const string ConnectionString = "Data Source=bazaDeDate.db";
        private List<Desfaceri> desfaceri_;
        public Data()
        {
            InitializeComponent();
            comboBoxMagazin.SelectedItem = magazinDefault;
            comboBoxRaion.SelectedItem = raionDefault;
            desfaceri_ = new List<Desfaceri>();
            //count++;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBoxNumeProdus_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNumeProdus.Text))
            {
                errorProvider1.SetError((Control)sender, "Campul nume este obligatoriu!");
            }
            else if (!(System.Text.RegularExpressions.Regex.IsMatch(textBoxNumeProdus.Text, "[^0-9]")))
            {
                errorProvider1.SetError((Control)sender, "Campul nume accepta doar litere!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBoxCantitate_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxCantitate.Text.Length < 1)
            {
                errorProvider1.SetError((Control)sender, "Campul cantitate este obligatoriu!");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBoxCantitate.Text, "[^0-9]"))
            {
                errorProvider1.SetError((Control)sender, "Campul cantitate accepta doar cifre!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBoxPret_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxPret.Text.Length < 1)
            {
                errorProvider1.SetError((Control)sender, "Campul pret este obligatoriu!");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBoxPret.Text, "[^0-9]"))
            {
                errorProvider1.SetError((Control)sender, "Campul cantitate accepta doar cifre!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void Refresh()
        {
            listView1.Items.Clear();
            foreach (var desfaceri in desfaceri_)
            {
                var lvi = new ListViewItem(desfaceri.numeProdus);

                lvi.SubItems.Add(desfaceri.cantitate.ToString());
                lvi.SubItems.Add(desfaceri.pret.ToString());
                lvi.SubItems.Add(desfaceri.raion.tip.ToString());
                lvi.SubItems.Add(desfaceri.magazin.nume.ToString());

                lvi.Tag = desfaceri;

                
                listView1.Items.Add(lvi);
            }
        }

        //insert in database

        private void AddDesf(Desfaceri desfaceri)
        {
            var query = "INSERT INTO Desfaceri(numeProdus, cantitate, pret, raion, magazin)" +
                "VALUES(@numeProdus, @cantitate, @pret, @raion, @magazin); " +
                "SELECT last_insert_rowid()";

            using(SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@numeProdus", desfaceri.numeProdus);
                command.Parameters.AddWithValue("@cantitate", desfaceri.cantitate);
                command.Parameters.AddWithValue("@pret", desfaceri.pret);
                command.Parameters.AddWithValue("@raion", desfaceri.raion);
                command.Parameters.AddWithValue("@magazin", desfaceri.magazin);

                //desfaceri.Id = (long)command.ExecuteScalar();

                desfaceri_.Add(desfaceri);
            }
        }
        
        private bool isInCollection(ListViewItem lvi)
        {
            foreach(ListViewItem item in listView1.Items)
            {
                bool subItemEqualFlag = true;
                for(int i=0; i<item.SubItems.Count; i++)
                {
                    string sub1 = item.SubItems[i].Text;
                    string sub2 = lvi.SubItems[i].Text;
                    if(sub1!=sub2)
                    {
                        subItemEqualFlag = false;
                    }
                }
                if (subItemEqualFlag)
                    return true;
            }
            return false;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var numeProdus = textBoxNumeProdus.Text;
            var cantitate = textBoxCantitate.Text;
            var pret = textBoxPret.Text;
            var raion = new Raioane();
            var magazin = new Magazine();
            raion.tip = (tip)comboBoxRaion.SelectedIndex;
            magazin.nume = (NumeMagazine)comboBoxMagazin.SelectedIndex;

            var desfaceri = new Desfaceri(numeProdus, int.Parse(cantitate), int.Parse(pret), raion, magazin);

            try
            {
                AddDesf(desfaceri);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            //try
            //{
            //    var magazin = new Magazine();
            //    var raion = new Raioane();
            //    magazin.nume = (NumeMagazine)comboBoxMagazin.SelectedIndex;
            //    raion.tip = (tip)comboBoxRaion.SelectedIndex;
            //    var desfacere = new Desfaceri(textBoxNumeProdus.Text, int.Parse(textBoxCantitate.Text), int.Parse(textBoxPret.Text), raion, magazin);
            //    desfaceri_.Add(desfacere);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //Refresh();        
        }

        private void LoadDesfaceri()
        {
            const string query = "SELECT * FROM Desfaceri";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string numeProdus = (string)reader["numeProdus"];
                        int cantitate = (int)reader["cantitate"];
                        int pret = (int)reader["pret"];
                        Raioane raion = (Raioane)reader["raion"];
                        Magazine magazin = (Magazine)reader["magazin"];

                        Desfaceri desfaceri = new Desfaceri(numeProdus, cantitate, pret, raion, magazin);
                        desfaceri_.Add(desfaceri);
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxPret.Clear();
            textBoxCantitate.Clear();
            textBoxNumeProdus.Clear();
            comboBoxRaion.SelectedItem = raionDefault;
            comboBoxMagazin.SelectedItem = magazinDefault;
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog.PageSettings = printDocument.DefaultPageSettings;

            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
                printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("Microsoft Sans Serif", 24);

            var pageSettings = e.PageSettings;

            var printAreaHeight = e.MarginBounds.Height;

            var printAreaWidth = e.MarginBounds.Width;

            var marginLeft = pageSettings.Margins.Left;
            var marginTop = pageSettings.Margins.Top;

            if (pageSettings.Landscape)
            {
                var intTemp = printAreaHeight;
                printAreaHeight = printAreaWidth;
                printAreaWidth = intTemp;
            }

            const int rowHeight = 40;
            var columnWidth = printAreaWidth / 3;

            StringFormat fmt = new StringFormat(StringFormatFlags.LineLimit);
            fmt.Trimming = StringTrimming.EllipsisCharacter;

            var currentY = marginTop;
            while (count < desfaceri_.Count)
            {
                var currentX = marginLeft;

                e.Graphics.DrawRectangle(
                Pens.Black,
                currentX,
                currentY,
                columnWidth,
                rowHeight);

                e.Graphics.DrawString(
                desfaceri_[count].numeProdus,
                font,
                Brushes.Black,
                new RectangleF(currentX, currentY, columnWidth, rowHeight),
                fmt);

                currentX += columnWidth;

                count++;


            }
        }

        private void standardizedFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new imageGallery().Show();
        }

        private void serializationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Create("serialized.bin"))
                formatter.Serialize(stream, desfaceri_);
        }

        private void deserializationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead("serialized.bin"))
            {
                desfaceri_ = (List<Desfaceri>)formatter.Deserialize(stream);
                Refresh();
            }
        }

        private void serializationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Desfaceri>));

            using (FileStream stream = File.Create("SerializedXML.xml"))
                serializer.Serialize(stream, desfaceri_);
        }

        private void deserializationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Desfaceri>));

            using (FileStream stream = File.OpenRead("SerializedXML.xml"))
            {
                desfaceri_ = (List<Desfaceri>)serializer.Deserialize(stream);
                Refresh();
            }
        }

        private void textBoxCantitate_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxCantitate.DoDragDrop(textBoxCantitate.Text, DragDropEffects.Copy);
        }

        private void textBoxPret_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBoxPret_DragDrop(object sender, DragEventArgs e)
        {
            textBoxPret.Text=e.Data.GetData(DataFormats.Text).ToString();
        }

        private void buttonTotal_Click(object sender, EventArgs e)
        {
            double suma = 0;
            foreach (ListViewItem lvi in listView1.Items)
            {
                suma += Convert.ToDouble(Convert.ToDouble(lvi.SubItems[1].Text) * Convert.ToDouble(lvi.SubItems[2].Text));
            }
            textBoxTotal.Text = suma.ToString();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if(listView1.Items.Count>0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
        }

        private void Data_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDesfaceri();
                Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
