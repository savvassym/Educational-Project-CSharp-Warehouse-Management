using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlDataAdapter daPel, daApo, daPar;
        DataSet dsPel, dsApo, dsPar;
        BindingSource bsPel, bsApo, bsPar;
        SqlCommandBuilder cmdbl;
        SqlCommand command;

        private void button3_Click(object sender, EventArgs e)
        {
            
            String openPath1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openPath1 = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
                textBox12.Text = openPath1;
                pictureBox1.Image = Image.FromFile(openPath1);
                command = new SqlCommand("UPDATE PELATHS SET FOTO = '" + openPath1 + "'WHERE KOD_PELATH= '" + textBox12.Text + ";", connection);
                command.ExecuteNonQuery();
            }
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {
            refreshImageTablePelaths();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String openPath2;
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                openPath2 = openFileDialog2.InitialDirectory + openFileDialog2.FileName;
                textBox25.Text = openPath2;
                pictureBox2.Image = Image.FromFile(openPath2);
                command = new SqlCommand("UPDATE APOTHIKI SET PHOTO= '" + openPath2 + "'WHERE KE= " + textBox25.Text + ";", connection);
                command.ExecuteNonQuery();
                refreshImageTableApothiki();
            }
        }

        private void bindingNavigator2_RefreshItems(object sender, EventArgs e)
        {
            refreshImageTableApothiki();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           

        }
        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(@"Data Source=DESKTOP-2D4FCOM;Initial Catalog=APOTHIKI_AEM_4268;Integrated Security=True");
            connection.Open();
            refreshImageTablePelaths();
            refreshImageTableApothiki();

        }
        public void refreshImageTablePelaths()
        {
            String photoPath = textBox12.Text.Trim();
            if (photoPath != null && File.Exists(photoPath))
            {
                pictureBox1.Image = Image.FromFile(photoPath);
            }
            else
            {
                pictureBox1.Image = Image.FromFile("C:/Users/Savvas/Desktop/Baseis2Project/error.bmp");
            }
        }
        public void refreshImageTableApothiki()
        {
            String photoPath = textBox25.Text.Trim();
            if (photoPath != null && File.Exists(photoPath))
            {
                pictureBox2.Image = Image.FromFile(photoPath);
            }
            else
            {
                pictureBox2.Image = Image.FromFile("C:/Users/Savvas/Desktop/Baseis2Project/error.bmp");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void saveToolStripButton2_Click(object sender, EventArgs e)
        {
            cmdbl = new SqlCommandBuilder(daPar);
            daPar.Update(dsPar, "Paragelia_Table");
            MessageBox.Show("Information Updated");
        }

        private void saveToolStripButton1_Click(object sender, EventArgs e)
        {
            cmdbl = new SqlCommandBuilder(daApo);
            daApo.Update(dsApo, "Apothiki_Table");
            MessageBox.Show("Information Updated");
        }

        private void saveToolStripButton_Click_1(object sender, EventArgs e)
        {
            cmdbl = new SqlCommandBuilder(daPel);
            daPel.Update(dsPel, "Pelates_Table");
            MessageBox.Show("Information Updated");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            daPel = new SqlDataAdapter("Select * from PELATHS", connection);
            dsPel = new DataSet();
            daPel.Fill(dsPel, "Pelates_Table");
            bsPel = new BindingSource();
            bsPel.DataSource = dsPel.Tables[0].DefaultView;
            bindingNavigator1.BindingSource = bsPel;
            textBox1.DataBindings.Add(new Binding("Text", bsPel, "KOD_PELATH", true));
            textBox2.DataBindings.Add(new Binding("Text", bsPel, "EPONYMIA", true));
            textBox3.DataBindings.Add(new Binding("Text", bsPel, "EPITHETO", true));
            textBox4.DataBindings.Add(new Binding("Text", bsPel, "ONOMA", true));
            textBox5.DataBindings.Add(new Binding("Text", bsPel, "HM_GENHSHS", true));
            textBox6.DataBindings.Add(new Binding("Text", bsPel, "HLIKIA", true));
            textBox7.DataBindings.Add(new Binding("Text", bsPel, "AFM", true));
            textBox8.DataBindings.Add(new Binding("Text", bsPel, "DOY", true));
            textBox9.DataBindings.Add(new Binding("Text", bsPel, "DIEYTHINSI", true));
            textBox10.DataBindings.Add(new Binding("Text", bsPel, "POLH", true));
            textBox11.DataBindings.Add(new Binding("Text", bsPel, "THL", true));
            textBox12.DataBindings.Add(new Binding("Text", bsPel, "FOTO", true));
            textBox13.DataBindings.Add(new Binding("Text", bsPel, "SXOLIA", true));

            daApo = new SqlDataAdapter("Select * from APOTHIKI", connection);
            dsApo = new DataSet();
            daApo.Fill(dsApo, "Apothiki_Table");
            bsApo = new BindingSource();
            bsApo.DataSource = dsApo.Tables[0].DefaultView;
            bindingNavigator2.BindingSource = bsApo;
            textBox14.DataBindings.Add(new Binding("Text", bsApo, "KE", true));
            textBox15.DataBindings.Add(new Binding("Text", bsApo, "EIDOS", true));
            textBox16.DataBindings.Add(new Binding("Text", bsApo, "KATHGORIA", true));
            textBox17.DataBindings.Add(new Binding("Text", bsApo, "APOTHEMA", true));
            textBox18.DataBindings.Add(new Binding("Text", bsApo, "TIMH_POLHSHS", true));
            textBox19.DataBindings.Add(new Binding("Text", bsApo, "FPA", true));
            textBox25.DataBindings.Add(new Binding("Text", bsApo, "FOTO", true));


            daPar = new SqlDataAdapter("Select * from PARAGELIA", connection);
            dsPar = new DataSet();
            daPar.Fill(dsPar, "Paragelia_Table");
            bsPar = new BindingSource();
            bsPar.DataSource = dsPar.Tables[0].DefaultView;
            bindingNavigator3.BindingSource = bsPar;
            textBox20.DataBindings.Add(new Binding("Text", bsPar, "KOD_PAR", true));
            textBox21.DataBindings.Add(new Binding("Text", bsPar, "HMER_PARAGELIAS", true));
            textBox22.DataBindings.Add(new Binding("Text", bsPar, "K_PEL", true));
            textBox23.DataBindings.Add(new Binding("Text", bsPar, "TROPOS_PLHROMHS", true));
            textBox24.DataBindings.Add(new Binding("Text", bsPar, "TOPOS_PARADOSHS", true));

            refreshImageTablePelaths();
            refreshImageTableApothiki();
        }

    }
}
