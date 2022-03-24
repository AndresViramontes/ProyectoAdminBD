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
            conexion = new SqlConnection("server=ANDREW-PC\\SQLEXPRESS;" +
             "database=Sistema_Musica; integrated security = true");
            conexion.Open();
            InitializeComponent();
            llenaLista();
            muestraDatos("Genero", dataGridView2);
            muestraDatos("Disquera", dataGridView3);
            muestraDatos("Miembro", dataGridView1);
            muestraDatos("Compositor", dataGridView4);
            muestraDatos("Artista", dataGridView5);
            muestraDatos("Playlist", dataGridView6);
            muestraDatos("Album", dataGridAlbum);
            llaveforandisq(comboBox2);
            llaveforanMimb(comboBox3);
            llaveforanGen(comboBoxGen);
            llaveforanArt(comboBoxArt);
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
            button14.Enabled = false;
            button13.Enabled = false;
            button17.Enabled = false;
            button16.Enabled = false;
            buttonModAlb.Enabled = false;
            buttonDelAlb.Enabled = false;
            buttonModCan.Enabled = false;
            buttonDelCan.Enabled = false;
            buttonModDet.Enabled = false;
            buttonDelDet.Enabled = false;
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
            llaveforanMimb(comboBox3);
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
            llaveforanGen(comboBoxGen);
        }
        public void muestraDatos(string Tabla,DataGridView dataGridView)//FUNCION PARA MOSTRAR DATOS EN EL DATAGRIDVIEW
        {
            string sql = "SELECT * FROM "+ Tabla;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            dataGridView.DataSource = dt;
        }
        public void llaveforandisq(ComboBox cb)//carga los valores de disquera en un combobox 
        {
            cb.Items.Clear();
            string sql = "SELECT idDisquera,NombreDisquera FROM Disquera";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            int i = 0;
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                string ad;
                ad = row["idDisquera"].ToString()+ ","+ row["NombreDisquera"].ToString();
                cb.Items.Add(ad);
                i++;
            }

        }
        public void llaveforanMimb(ComboBox cb)//carga los valores de disquera en un combobox 
        {
            cb.Items.Clear();
            string sql = "SELECT idMiembro,NombreMiembro,Edad FROM Miembro ";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            int i = 0;
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                string ad;
                ad = row["idMiembro"].ToString() + "," + row["NombreMiembro"].ToString() + "("+row["Edad"].ToString() +")";
                cb.Items.Add(ad);
                i++;
            }

        }
        public void llaveforanGen(ComboBox cb)//carga los valores de disquera en un combobox 
        {
            cb.Items.Clear();
            string sql = "SELECT idGenero,NombreGenero FROM Genero ";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            int i = 0;
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                string ad;
                ad = row["idGenero"].ToString() + "," + row["NombreGenero"].ToString();
                cb.Items.Add(ad);
                i++;
            }

        }
        public void llaveforanArt(ComboBox cb)//carga los valores de disquera en un combobox 
        {
            cb.Items.Clear();
            string sql = "SELECT idArtista,NombreArtista,idDisquera FROM Artista ";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            int i = 0;
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                string sql2 = "SELECT NombreDisquera FROM Disquera WHERE idDisquera=" + row["idDisquera"];
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sql2, conexion);
                DataSet dt2 = new DataSet();
                dataAdapter2.Fill(dt2);
                string ad;
                DataRow row2 = dt2.Tables[0].Rows[0];
                ad = row["idArtista"].ToString() + "," + row["NombreArtista"].ToString()+", "+row2["NombreDisquera"];
                cb.Items.Add(ad);
                i++;
            }

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
            llaveforanGen(comboBoxGen);
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
            llaveforanGen(comboBoxGen);
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
            llaveforandisq(comboBox2);
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
            llaveforandisq(comboBox2);
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
            llaveforandisq(comboBox2);
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
            llaveforanMimb(comboBox3);
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
            llaveforanMimb(comboBox3);

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
        public string separaId(string a)//obtiene el id despues de una concatenacion 
        {
            string x="";
            char[] car = a.ToCharArray();
            foreach(char e in car)
            {
                if (e == ',')
                    return x;
                x = x + e;
            }
            return x;
        }
        private void button15_Click(object sender, EventArgs e)
        {
            string id_Disquera = separaId(comboBox2.Text);
            string nombre = textBox6.Text;
            //MessageBox.Show(fechaFun);
            string cadena = "INSERT INTO Artista(idDisquera,NombreArtista)" +
               "VALUES ('" + id_Disquera + "','" + nombre + "')";
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBox2.SelectedItem = null;
            textBox6.Text = "";
            muestraDatos("Artista", dataGridView5);
            muestraDatos("Disquera", dataGridView3);
            llaveforanArt(comboBoxArt);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string id_Disquera = separaId(comboBox2.Text);
            string nombre = textBox6.Text;
            //MessageBox.Show(fechaFun);
            string cadena = "DELETE FROM Artista WHERE idArtista=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBox2.SelectedItem = null;
            textBox6.Text = "";
            button13.Enabled = false;
            button14.Enabled = false;
            muestraDatos("Artista", dataGridView5);
            muestraDatos("Disquera", dataGridView3);
            llaveforanArt(comboBoxArt);
        }

        private void dataGridView5_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridView5.CurrentRow.Index;

            string id = dataGridView5.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            //comboBox2.SelectedItem = comboBox2.SelectedIndex\
            foreach (var item in comboBox2.Items)
            {
                if (separaId(item.ToString()) == dataGridView5.Rows[index].Cells[1].Value.ToString())
                   comboBox2.Text =item.ToString();
            }
            textBox6.Text = dataGridView5.Rows[index].Cells[2].Value.ToString();
            button14.Enabled = true;
            button13.Enabled = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string id_Disquera = separaId(comboBox2.Text);
            string nombre = textBox6.Text;
            //MessageBox.Show(fechaFun);
            string cadena = "UPDATE Artista SET idDisquera=" + "'" + id_Disquera + "'," + "NombreArtista=" + "'" + nombre +
               "' WHERE idArtista=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBox2.SelectedItem = null;
            textBox6.Text = "";
            button13.Enabled = false;
            button14.Enabled = false;
            muestraDatos("Artista", dataGridView5);
            muestraDatos("Disquera", dataGridView3);
            llaveforanArt(comboBoxArt);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string id_Miembro = separaId(comboBox3.Text);
            string nombre = textBox8.Text;
            string priv = comboBox4.Text;
            string cadena = "INSERT INTO Playlist(idMiembro,NombrePlaylist,FechaCreacion,Privada)" +
               "VALUES ('" + id_Miembro + "','" + nombre + "','" + DateTime.Today.ToString("yyyy/MM/dd") +"','" + priv + "')";
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
            textBox8.Text = "";
            muestraDatos("Playlist", dataGridView6);
            //muestraDatos("Disquera", dataGridView6);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string id_Miembro = separaId(comboBox3.Text);
            string nombre = textBox8.Text;
            string priv = comboBox4.Text;
            //MessageBox.Show(fechaFun);
            string cadena = "UPDATE Playlist SET idMiembro=" + "'" + id_Miembro + "'," + "NombrePlaylist='" + nombre + "'," + "Privada='" + priv +
              "' WHERE idPlaylist=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
            textBox8.Text = "";
            muestraDatos("Playlist", dataGridView6);
            //muestraDatos("Disquera", dataGridView6);
            button16.Enabled = false;
            button17.Enabled = false;
        }

        private void dataGridView6_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridView6.CurrentRow.Index;

            string id = dataGridView6.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            //comboBox2.SelectedItem = comboBox2.SelectedIndex\
            foreach (var item in comboBox3.Items)
            {
                if (separaId(item.ToString()) == dataGridView6.Rows[index].Cells[1].Value.ToString())
                    comboBox3.Text = item.ToString();
            }
            textBox8.Text = dataGridView6.Rows[index].Cells[2].Value.ToString();
            comboBox4.Text = dataGridView6.Rows[index].Cells[5].Value.ToString();
            
            button16.Enabled = true;
            button17.Enabled = true;
        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void buttonModCan_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelCan_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            string id_Miembro = separaId(comboBox3.Text);
            string nombre = textBox8.Text;
            string priv = comboBox4.Text;
            //MessageBox.Show(fechaFun);
            string cadena = "DELETE FROM Playlist  WHERE idPlaylist=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
            textBox8.Text = "";
            muestraDatos("Playlist", dataGridView6);
            //muestraDatos("Disquera", dataGridView6);
            button16.Enabled = false;
            button17.Enabled = false;
        }

        private void buttonAddAlb_Click(object sender, EventArgs e)
        {
            string id_Genero = separaId(comboBoxGen.Text);
            string id_Artista = separaId(comboBoxArt.Text);
            string nombre = textBoxAlbum.Text;
            string noCanciones = textBoxNumCa.Text;
            //MessageBox.Show(fechaFun);
            string cadena = "INSERT INTO Album(idGenero,idArtista,NombreAlbum,CantidadCanciones)" +
               "VALUES ('" + id_Genero + "','" + id_Artista + "','" + nombre +"','" + noCanciones + "')";
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBoxGen.SelectedItem = null;
            comboBoxArt.SelectedItem = null;
            textBoxAlbum.Text = "";
            textBoxNumCa.Text = "";
            muestraDatos("Album", dataGridAlbum);
            muestraDatos("Artista", dataGridView5);
            //muestraDatos("Disquera", dataGridView6);
        }

        private void dataGridAlbum_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridAlbum.CurrentRow.Index;

            string id = dataGridAlbum.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            //comboBox2.SelectedItem = comboBox2.SelectedIndex\
            foreach (var item in comboBoxGen.Items)
            {
                if (separaId(item.ToString()) == dataGridAlbum.Rows[index].Cells[1].Value.ToString())
                    comboBoxGen.Text = item.ToString();
            }
            foreach (var item in comboBoxArt.Items)
            {
                if (separaId(item.ToString()) == dataGridAlbum.Rows[index].Cells[2].Value.ToString())
                    comboBoxArt.Text = item.ToString();
            }
            textBoxAlbum.Text = dataGridAlbum.Rows[index].Cells[3].Value.ToString();
            textBoxNumCa.Text = dataGridAlbum.Rows[index].Cells[4].Value.ToString();

            buttonModAlb.Enabled = true;
            buttonDelAlb.Enabled = true;
        }

        private void buttonModAlb_Click(object sender, EventArgs e)
        {
            string id_Genero = separaId(comboBoxGen.Text);
            string id_Artista = separaId(comboBoxArt.Text);
            string nombre = textBoxAlbum.Text;
            string noCanciones = textBoxNumCa.Text;
            //MessageBox.Show(fechaFun);
            string cadena = "UPDATE Album SET idGenero=" + "'" + id_Genero + "'," + "idArtista='" + id_Artista + "'," + "NombreAlbum='" + nombre + "'," + "CantidadCanciones='" + noCanciones +
              "' WHERE idAlbum=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);
            
            comando.ExecuteNonQuery();
            comboBoxGen.SelectedItem = null;
            comboBoxArt.SelectedItem = null;
            textBoxAlbum.Text = "";
            textBoxNumCa.Text = "";
            muestraDatos("Album", dataGridAlbum);
            buttonModAlb.Enabled = false;
            buttonDelAlb.Enabled = false;
            muestraDatos("Artista", dataGridView5);
        }

        private void buttonDelAlb_Click(object sender, EventArgs e)
        {
            string id_Genero = separaId(comboBoxGen.Text);
            string id_Artista = separaId(comboBoxArt.Text);
            string nombre = textBoxAlbum.Text;
            string noCanciones = textBoxNumCa.Text;
            //MessageBox.Show(fechaFun);
            string cadena = "DELETE FROM Album  WHERE idAlbum=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBoxGen.SelectedItem = null;
            comboBoxArt.SelectedItem = null;
            textBoxAlbum.Text = "";
            textBoxNumCa.Text = "";
            muestraDatos("Album", dataGridAlbum);
            buttonModAlb.Enabled = false;
            buttonDelAlb.Enabled = false;
            muestraDatos("Artista", dataGridView5);
        }
    }
}
