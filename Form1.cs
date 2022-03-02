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
        int idAux;
        SqlConnection conexion;
        public Form1()
        {
            //conexion = new SqlConnection("server=DESKTOP-N3D010C\\SQLEXPRESS;" +
              //  "database=Sistema_Musica; integrated security = true");
            conexion.Open();
            InitializeComponent();
            muestraDatos("Genero", dataGridView2);
            muestraDatos("Disquera", dataGridView3);
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

       

        private void button6_Click(object sender, EventArgs e)
        {
            string nombreGen = textBox3.Text;
            
            string cadena = "INSERT INTO Genero(NombreGenero)" +
               "VALUES ('" +nombreGen +"')";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            textBox3.Text = "";
            muestraDatos("Genero",dataGridView2);
        }
        public void muestraDatos(string Tabla,DataGridView dataGridView)
        {
            string sql = "SELECT * FROM "+ Tabla;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            dataGridView.DataSource = dt;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = dataGridView2.CurrentRow.Index;

            string id = dataGridView2.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            textBox3.Text = dataGridView2.Rows[index].Cells[1].Value.ToString();
            button4.Enabled=true;
            button5.Enabled=true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string nombreGen = textBox3.Text;

            string cadena = "UPDATE Genero SET NombreGenero=" + "'" + nombreGen + "'" + " WHERE idGenero=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            textBox3.Text = "";
            muestraDatos("Genero", dataGridView2);
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nombreGen = textBox3.Text;

            string cadena = "DELETE FROM Genero" + " WHERE idGenero=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            textBox3.Text = "";
            muestraDatos("Genero", dataGridView2);
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string nombreDisc = textBox5.Text;
            string paginaWeb = textBox4.Text;
            string fechaFun = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            //MessageBox.Show(fechaFun);
            string cadena = "INSERT INTO Disquera(NombreDisquera,PaginaWeb,FechaFundacion)" +
               "VALUES ('" + nombreDisc + "','" + paginaWeb + "','" + fechaFun + "')";
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker2.Text = "";
            muestraDatos("Disquera", dataGridView3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string nombreDisc = textBox5.Text;
            string paginaWeb = textBox4.Text;
            string fechaFun = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            //MessageBox.Show(fechaFun);
            string cadena = "UPDATE Disquera SET NombreDisquera=" + "'" + nombreDisc + "'" + "," + "PaginaWeb=" + "'" + paginaWeb + "'"
                + "," + "FechaFundacion=" + "'" + fechaFun + "'"+ " WHERE idDisquera=" + idAux.ToString(); ;
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker2.Value=DateTime.Today;
            muestraDatos("Disquera", dataGridView3);
            button7.Enabled = false;
            button8.Enabled = false;
        }

        private void dataGridView3_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridView3.CurrentRow.Index;

            string id = dataGridView3.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            textBox5.Text = dataGridView3.Rows[index].Cells[1].Value.ToString();
            textBox4.Text = dataGridView3.Rows[index].Cells[2].Value.ToString();
            dateTimePicker2.Text = dataGridView3.Rows[index].Cells[3].Value.ToString();
            button7.Enabled = true;
            button8.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string nombreDisc = textBox5.Text;
            string paginaWeb = textBox4.Text;
            string fechaFun = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            //MessageBox.Show(fechaFun);
            string cadena = "DELETE FROM Disquera WHERE idDisquera=" + idAux.ToString(); ;
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker2.Value = DateTime.Today;
            muestraDatos("Disquera", dataGridView3);
            button7.Enabled = false;
            button8.Enabled = false;
        }
    }
}
