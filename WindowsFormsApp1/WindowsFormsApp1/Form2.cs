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

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        SqlConnection connection;
        SqlDataAdapter DataAdapter1, DataAdapter2;
        DataSet DataSet1, DataSet2;
        BindingSource BindingSource1, BindingSource2;


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDataSet();
        }


        public Form2()
        { 
            InitializeComponent();
            connection = new SqlConnection(@"Data Source=DESKTOP-2D4FCOM;Initial Catalog=APOTHIKI_AEM_4268;Integrated Security=True");
            connection.Open();

            DataAdapter1 = new SqlDataAdapter("Select * from PELATHS", connection);
            DataTable dt1 = new DataTable();
            DataAdapter1.Fill(dt1);
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "EPITHETO";
        }
        public void fillDataSet()
        {
            DataAdapter2 = new SqlDataAdapter("Select EPONYMIA, AFM, EIDOS, KATHGORIA, TIMH_POLHSHS, FPA, POSOTHTA " +
                "FROM PELATHS INNER JOIN PARAGELIA INNER JOIN PROIONTA_PARAGELIAS " +
                "ON PARAGELIA.KOD_PAR = PROIONTA_PARAGELIAS.K_PAR INNER JOIN APOTHIKI ON PROIONTA_PARAGELIAS.K_E = APOTHIKI.KE  " +
                "ON PELATHS.KOD_PELATH = PARAGELIA.K_PEL WHERE EPITHETO= '" + comboBox1.Text.ToString() + "'", connection);

            DataSet2 = new DataSet();
            BindingSource2 = new BindingSource();
            DataAdapter2.Fill(DataSet2);
            DataTable dt = new DataTable();
            dataGridView1.DataSource = DataSet2.Tables[0].DefaultView;
            Double sum = 0; 
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value)*
                       Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value)*
                       Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
            }

            label4.Text = sum.ToString("F1");


        }
    }
}
