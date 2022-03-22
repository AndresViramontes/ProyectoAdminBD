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
        int idRow;
        List<string> paises;
        SqlConnection conexion;
        public Form1()
        {
            conexion = new SqlConnection("server=DESKTOP-N3D010C\\SQLEXPRESS;" +
             "database=Sistema_Musica; integrated security = true");
            conexion.Open();
            InitializeComponent();
            llenaLista();
            muestraDatos("Genero", dataGridView2);
            muestraDatos("Disquera", dataGridView3);
            muestraDatos("Miembro", dataGridView1);
            muestraDatos("Compositor", dataGridView4);
            foreach (string s in paises)
            {
                comboBox1.Items.Add(s);
            }
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button11.Enabled = false;
            button10.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void llenaLista()
        {
            paises = new List<string>();
            paises.Add("Reino Unido");
            paises.Add("Estados Unidos");
            paises.Add("Japón");
            paises.Add("México");
            paises.Add("Brasil");
            paises.Add("Canadá");
            paises.Add("China");
            paises.Add("Alemania");
            paises.Add("Rusia");
            paises.Add("India");
            paises.Add("Colombia");
            paises.Add("Argentina");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string email = textBox2.Text;
            string fechaN = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            DateTime thisDay = DateTime.Today;
            string fechaAc = thisDay.ToString("yyyy/MM/dd");
            string cadena = "INSERT INTO Miembro(NombreMiembro,Email,FechaNacimiento,MiembroDesde)" +
               "VALUES ('" + nombre + "','" + email + "','" + fechaN + "','" + fechaAc +"')";
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Text = "";
            muestraDatos("Miembro", dataGridView1);
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
                + "," + "FechaFundacion=" + "'" + fechaFun + "'"+ " WHERE idDisquera=" + idAux.ToString();
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
            string cadena = "DELETE FROM Disquera WHERE idDisquera=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker2.Value = DateTime.Today;
            muestraDatos("Disquera", dataGridView3);
            button7.Enabled = false;
            button8.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string email = textBox2.Text;
            string fechaN = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            DateTime thisDay = DateTime.Today;
            string fechaAc = thisDay.ToString("yyyy/MM/dd");
            string cadena = "UPDATE Miembro SET NombreMiembro = '" + nombre + "' , Email = '" + email + "' , FechaNacimiento = '" + fechaN + "' , MiembroDesde = '" + fechaAc + "' WHERE idMiembro=" + idAux.ToString();
              
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Text = "";
            muestraDatos("Miembro", dataGridView1);
            button3.Enabled = false;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string email = textBox2.Text;
            string fechaN = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            string cadena = "DELETE FROM Miembro WHERE idMiembro=" + idAux.ToString();

            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Text = "";

            muestraDatos("Miembro", dataGridView1);
            button2.Enabled = false;
            button3.Enabled = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridView1.CurrentRow.Index;

            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            textBox1.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string nombre = textBox7.Text;
            string pais = comboBox1.Text;
            string cadena = "INSERT INTO Compositor(NombreCompositor,PaisOrigen,NumeroCanciones)" +
               "VALUES ('" + nombre + "','" + pais + "',"+ 0 +")";
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox7.Text = "";
            comboBox1.Text = "";
            muestraDatos("Compositor", dataGridView4);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string nombre = textBox7.Text;
            string pais = comboBox1.Text;
            string cadena = "UPDATE Compositor SET NombreCompositor = '" + nombre + "' , PaisOrigen = '" + pais +"' WHERE idCompositor=" + idAux.ToString();

            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox7.Text = "";
            comboBox1.Text = "";
            muestraDatos("Compositor", dataGridView4);
            button11.Enabled = false;
            button10.Enabled = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string nombre = textBox7.Text;
            string pais = comboBox1.Text;
            
            string cadena = "DELETE FROM Compositor WHERE idCompositor=" + idAux.ToString();

            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            textBox7.Text = "";
            comboBox1.Text = "";
           
            muestraDatos("Compositor", dataGridView4);
            button11.Enabled = false;
            button10.Enabled = false;
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //nada
        }

        private void dataGridView4_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridView4.CurrentRow.Index;

            string id = dataGridView4.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            textBox7.Text = dataGridView4.Rows[index].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView4.Rows[index].Cells[3].Value.ToString();
            button11.Enabled = true;
            button10.Enabled = true;
        }
    }
}
