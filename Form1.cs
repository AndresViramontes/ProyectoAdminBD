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

namespace Proyecto_Adimn_BD
{
    public partial class Form1 : Form
    {
        SqlConnection conexion;
        public Form1()
        {
            //conexion = new SqlConnection("server=ANDREW-PC\\SQLEXPRESS; database = Universidad; integrated security = true");
            //conexion.Open();
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string email = textBox2.Text;
            string fechaN = dateTimePicker1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string nombreDisc = textBox5.Text;
            string paginaWeb = textBox4.Text;
            string fechaFun = dateTimePicker2.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string nombreGen = textBox3.Text;
        }
    }
}
