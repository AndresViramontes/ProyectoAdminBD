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
        List<string> idAlbum;
        List<string> idCancion;
        List<string> idPlay;
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
            muestraDatos("Artista", dataGridView5);
            muestraDatos("Playlist", dataGridView6);
            //muestraDatosPlaylist("Playlist", dataGridView6);
            muestraDatos("Album", dataGridAlbum);
            muestraDatos("Cancion", dataGridCancion);
            muestraDatos("DetallePlaylist", dataGridDetalle);
            llaveforandisq(comboBox2);
            llaveforanMimb(comboBox3);
            llaveforanGen(comboBoxGen);
            llaveforanArt(comboBoxArt);
            llaveforanAlbm(comboBoxAlb);
            llaveforanComp(comboBoxComp);
            llaveforacan(comboBoxCan);
            llaveforaPlay(comboBoxPlayList);
            muestraDatosPlaylist("Playlist", dataGridView6);
            muestradatosAlbn("Album", dataGridAlbum);
            muestradatosCan("Cancion", dataGridCancion);
            muestradatosDetallP("DetallePlaylist", dataGridDetalle);
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
        public void muestraDatosPlaylist(string Tabla, DataGridView dataGridView)//FUNCION PARA MOSTRAR DATOS EN EL DATAGRIDVIEW
        {
            string sql = "SELECT * FROM " + Tabla;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            DataTable dta = new DataTable();
            dta.Columns.Add("idPlaylist");
            dta.Columns.Add("Miembro");
            dta.Columns.Add("NombrePlaylist");
            dta.Columns.Add("Duracion");
            dta.Columns.Add("Fecha Creacion");
            dta.Columns.Add("Privacidad");
            //dataAdapter.Fill(dt);
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                string sql2 = "SELECT idMiembro,NombreMiembro,Edad FROM Miembro WHERE idMiembro="+row["idMiembro"];
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sql2, conexion);
                DataSet dt2 = new DataSet();
                dataAdapter2.Fill(dt2); DataRow row2 = dt2.Tables[0].Rows[0];
                string ad = row2["idMiembro"].ToString() + "," + row2["NombreMiembro"].ToString() + "(" + row2["Edad"].ToString() + ")";
                dta.Rows.Add(row["idPlaylist"], ad,row["NombrePlaylist"], row["DuracionTotal"], row["FechaCreacion"], row["Privada"]);
            }
            dataGridView.DataSource = dta;
        }
        public void muestradatosAlbn(string Tabla, DataGridView dataGridView)
        {
            string sql = "SELECT * FROM " + Tabla;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            DataTable dta = new DataTable();
            dta.Columns.Add("idAlbum");
            dta.Columns.Add("Genero");
            dta.Columns.Add("Artista");
            dta.Columns.Add("Nombre album");
            dta.Columns.Add("Cantidad de canciones");
            dta.Columns.Add("Tipo de album");
            //dataAdapter.Fill(dt);
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                string sql2 = "SELECT idGenero,NombreGenero FROM Genero WHERE idGenero=" + row["idGenero"];
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sql2, conexion);
                DataSet dt2 = new DataSet();
                dataAdapter2.Fill(dt2); 
                DataRow row2 = dt2.Tables[0].Rows[0];
                string ad = row2["idGenero"].ToString() + "," + row2["NombreGenero"].ToString() ;
                string sql3 = "SELECT idArtista,NombreArtista,idDisquera FROM Artista WHERE idArtista=" + row["idArtista"];
                SqlDataAdapter dataAdapter3 = new SqlDataAdapter(sql3, conexion);
                DataSet dt3 = new DataSet();
                dataAdapter3.Fill(dt3); 
                DataRow row3 = dt3.Tables[0].Rows[0];
                string sql4 = "SELECT NombreDisquera FROM Disquera WHERE idDisquera=" + row3["idDisquera"];
                SqlDataAdapter dataAdapter4 = new SqlDataAdapter(sql4, conexion);
                DataSet dt4 = new DataSet();
                dataAdapter4.Fill(dt4);
                DataRow row4 = dt4.Tables[0].Rows[0];
                string ad2 = row3["idArtista"].ToString() + "," + row3["NombreArtista"].ToString() + ", " + row4["NombreDisquera"].ToString();
                dta.Rows.Add(row["idAlbum"], ad, ad2, row["NombreAlbum"], row["CantidadCanciones"], row["TipoAlbum"]);
            }
            dataGridView.DataSource = dta;
        }
        public void muestradatosCan(string Tabla, DataGridView dataGridView)
        {
            string sql = "SELECT * FROM " + Tabla;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            DataTable dta = new DataTable();
            dta.Columns.Add("idCancion");
            dta.Columns.Add("Album");
            dta.Columns.Add("Compositor");
            dta.Columns.Add("NombreCancion");
            dta.Columns.Add("Fecha de lanazmiento");
            dta.Columns.Add("Explicita");
            dta.Columns.Add("Duracion");
            //dataAdapter.Fill(dt);
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                //string sql2 = "SELECT idMiembro,NombreMiembro,Edad FROM Miembro WHERE idMiembro=" + row["idMiembro"];
                //SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sql2, conexion);
                //DataSet dt2 = new DataSet();
                //dataAdapter2.Fill(dt2); DataRow row2 = dt2.Tables[0].Rows[0];
                string ads = comboBoxAlb.Items[idAlbum.IndexOf(row["idAlbum"].ToString())].ToString();
                string ad = "";
                foreach (var item in comboBoxComp.Items)
                {
                    if (separaId(item.ToString()) == row["idCompositor"].ToString())
                        ad = item.ToString();
                }
                //string ad = row2["idMiembro"].ToString() + "," + row2["NombreMiembro"].ToString() + "(" + row2["Edad"].ToString() + ")";
                dta.Rows.Add(row["idCancion"],ads , ad, row["NombreCancion"], row["FechaLanzamiento"], row["Explicita"], row["Duracion"]);
            }
            dataGridView.DataSource = dta;
        }
        public void muestradatosDetallP(string Tabla, DataGridView dataGridView)
        {
            string sql = "SELECT * FROM " + Tabla;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            DataTable dta = new DataTable();
            dta.Columns.Add("idDetallePlaylist");
            dta.Columns.Add("Playlist");
            dta.Columns.Add("Cancion");
            //dataAdapter.Fill(dt);
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                //string sql2 = "SELECT idMiembro,NombreMiembro,Edad FROM Miembro WHERE idMiembro=" + row["idMiembro"];
                //SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sql2, conexion);
                //DataSet dt2 = new DataSet();
                //dataAdapter2.Fill(dt2); DataRow row2 = dt2.Tables[0].Rows[0];
                string ads = comboBoxPlayList.Items[idPlay.IndexOf(row["idPlaylist"].ToString())].ToString() ;
                string ad = comboBoxCan.Items[idCancion.IndexOf(row["idCancion"].ToString())].ToString() ;
                //string ad = row2["idMiembro"].ToString() + "," + row2["NombreMiembro"].ToString() + "(" + row2["Edad"].ToString() + ")";
                dta.Rows.Add(row["idDetallePlaylist"], ads, ad);
            }
            dataGridView.DataSource = dta;
        }
        public void llaveforaPlay(ComboBox cb)//carga los valores de disquera en un combobox 
        {
            cb.Items.Clear();
            idPlay = new List<string>();
            string sql = "SELECT idPlaylist,NombrePlaylist FROM Playlist";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            int i = 0;
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                string ad;
                ad = row["NombrePlaylist"].ToString();
                idPlay.Add(row["idPlaylist"].ToString());
                cb.Items.Add(ad);
                i++;
            }

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
       
        public void llaveforacan(ComboBox cb)//carga los valores de disquera en un combobox 
        {
            cb.Items.Clear();
            idCancion = new List<string>();
            string sql = "SELECT idCancion,idAlbum,NombreCancion,YEAR(FechaLanzamiento) as Anio FROM Cancion";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            int i = 0;
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                string sql2 = "SELECT idArtista FROM Album WHERE idAlbum=" + row["idAlbum"];
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sql2, conexion);
                DataSet dt2 = new DataSet();
                dataAdapter2.Fill(dt2);
                DataRow row2 = dt2.Tables[0].Rows[0];
                string sql3 = "SELECT NombreArtista FROM Artista WHERE idArtista=" + row2["idArtista"];
                SqlDataAdapter dataAdapter3 = new SqlDataAdapter(sql3, conexion);
                DataSet dt3 = new DataSet();
                dataAdapter3.Fill(dt3);
                DataRow row3 = dt3.Tables[0].Rows[0];
                string ad;
                ad = row["NombreCancion"] + ", " + row3["NombreArtista"].ToString() + "(" + row["Anio"] + ")";;
                idCancion.Add(row["idCancion"].ToString());
                cb.Items.Add(ad);
                i++;
            }

        }
        public void llaveforanAlbm(ComboBox cb)
        {
            cb.Items.Clear();
            idAlbum = new List<string>();
            string sql = "SELECT idAlbum,NombreAlbum,idArtista,CantidadCanciones FROM Album";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            int i = 0;
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                string sql2 = "SELECT NombreArtista FROM Artista WHERE idArtista=" + row["idArtista"];
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sql2, conexion);
                DataSet dt2 = new DataSet();
                dataAdapter2.Fill(dt2);
                string ad;
                DataRow row2 = dt2.Tables[0].Rows[0];
                ad = row2["NombreArtista"] + ", " + row["NombreAlbum"].ToString() + "(" + row["CantidadCanciones"] + ")";
                idAlbum.Add(row["idAlbum"].ToString());
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
        public void llaveforanComp(ComboBox cb)//carga los valores de disquera en un combobox 
        {
            cb.Items.Clear();
            string sql = "SELECT idCompositor,NombreCompositor FROM Compositor ";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);
            int i = 0;
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                //string sql2 = "SELECT NombreDisquera FROM Disquera WHERE idDisquera=" + row["idDisquera"];
                //SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sql2, conexion);
                //DataSet dt2 = new DataSet();
                //dataAdapter2.Fill(dt2);
                string ad;
                //DataRow row2 = dt2.Tables[0].Rows[0];
                ad = row["idCompositor"].ToString() + "," + row["NombreCompositor"].ToString() ;
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
            llaveforanComp(comboBoxComp);
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
            llaveforanComp(comboBoxComp);
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
            llaveforanComp(comboBoxComp);
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
        public string separaIddif(string a)//obtiene el id despues de una concatenacion 
        {
            
            string x = "";
            char[] car = a.ToCharArray();
            foreach (char e in car)
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
            muestraDatosPlaylist("Playlist", dataGridView6);
            llaveforaPlay(comboBoxPlayList);
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
            muestraDatosPlaylist("Playlist", dataGridView6);
            //muestraDatos("Disquera", dataGridView6);
            button16.Enabled = false;
            button17.Enabled = false;
            llaveforaPlay(comboBoxPlayList);
            llaveforaPlay(comboBoxPlayList);
            llaveforaPlay(comboBoxPlayList);
        }

        private void dataGridView6_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridView6.CurrentRow.Index;

            string id = dataGridView6.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            //comboBox2.SelectedItem = comboBox2.SelectedIndex\
            //foreach (var item in comboBox3.Items)
            //{
            //    if (separaId(item.ToString()) == dataGridView6.Rows[index].Cells[1].Value.ToString())
            //        comboBox3.Text = item.ToString();
            //}
            comboBox3.Text = dataGridView6.Rows[index].Cells[1].Value.ToString();
            textBox8.Text = dataGridView6.Rows[index].Cells[2].Value.ToString();
            comboBox4.Text = dataGridView6.Rows[index].Cells[5].Value.ToString();
            
            button16.Enabled = true;
            button17.Enabled = true;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string id_Album = idAlbum[comboBoxAlb.SelectedIndex];
            string id_Compositor = separaId(comboBoxComp.Text);
            string nombre = textBoxCan.Text;
            string Duracion = textBox10.Text + ":" + textBox9.Text;
            string fechaLan = dateTimeLanza.Value.ToString("yyyy/MM/dd");
            string explicita = comboBox5.Text;
            //MessageBox.Show(fechaFun);
            string cadena = "INSERT INTO Cancion(idAlbum,idCompositor,NombreCancion,FechaLanzamiento,Explicita,Duracion)" +
               "VALUES ('" + id_Album + "','" + id_Compositor + "','" + nombre + "','" + fechaLan + "','" + explicita + "','" + "00:"+Duracion + "')";
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBoxAlb.SelectedItem = null;
            comboBoxComp.SelectedItem = null;
            textBoxCan.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            comboBox5.SelectedItem = null;
            muestradatosAlbn("Album", dataGridAlbum);
            muestraDatos("Compositor", dataGridView4);
            muestradatosCan("Cancion", dataGridCancion);
            llaveforacan(comboBoxCan);
            //llaveforanAlbm(comboBoxAlb);
            //muestraDatos("Disquera", dataGridView6);
        }

        private void buttonModCan_Click(object sender, EventArgs e)
        {
            
            string id_Album = idAlbum[comboBoxAlb.SelectedIndex];
            string id_Compositor = separaId(comboBoxComp.Text);
            string nombre = textBoxCan.Text;
            string Duracion = textBox10.Text + ":" + textBox9.Text;
            string fechaLan = dateTimeLanza.Value.ToString("yyyy/MM/dd");
            string explicita = comboBox5.Text;
            //MessageBox.Show(fechaFun);
            
            string cadena = "UPDATE Cancion SET idAlbum=" + "'" + id_Album + "'," + "idCompositor=" + "'" + id_Compositor +
                "'," + "NombreCancion=" + "'" + nombre + "'," + "FechaLanzamiento=" + "'" + fechaLan + "'," + "Explicita=" + "'" + explicita +
                "'," + "Duracion=" + "'" + Duracion +
               "' WHERE idCancion=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBoxAlb.SelectedItem = null;
            comboBoxComp.SelectedItem = null;
            textBoxCan.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            comboBox5.SelectedItem = null;
            muestradatosAlbn("Album", dataGridAlbum);
            muestraDatos("Compositor", dataGridView4);
            muestradatosCan("Cancion", dataGridCancion);
            llaveforacan(comboBoxCan);
            //llaveforanAlbm(comboBoxAlb);
            //muestraDatos("Disquera", dataGridView6);
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
            muestraDatosPlaylist("Playlist", dataGridView6);
            //muestraDatos("Disquera", dataGridView6);
            button16.Enabled = false;
            button17.Enabled = false;
            llaveforaPlay(comboBoxPlayList);
            llaveforaPlay(comboBoxPlayList);
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
            muestradatosAlbn("Album", dataGridAlbum);
            muestraDatos("Artista", dataGridView5);
            llaveforanAlbm(comboBoxAlb);
            //muestraDatos("Disquera", dataGridView6);
        }

        private void dataGridAlbum_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridAlbum.CurrentRow.Index;

            string id = dataGridAlbum.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            //comboBox2.SelectedItem = comboBox2.SelectedIndex\
            //foreach (var item in comboBoxGen.Items)
            //{
            //    if (separaId(item.ToString()) == dataGridAlbum.Rows[index].Cells[1].Value.ToString())
            //        comboBoxGen.Text = item.ToString();
            //}
            //foreach (var item in comboBoxArt.Items)
            //{
            //    if (separaId(item.ToString()) == dataGridAlbum.Rows[index].Cells[2].Value.ToString())
            //        comboBoxArt.Text = item.ToString();
            //}
            comboBoxGen.Text = dataGridAlbum.Rows[index].Cells[1].Value.ToString();
            comboBoxArt.Text = dataGridAlbum.Rows[index].Cells[2].Value.ToString();
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
            muestradatosAlbn("Album", dataGridAlbum);
            buttonModAlb.Enabled = false;
            buttonDelAlb.Enabled = false;
            muestraDatos("Artista", dataGridView5);
            llaveforanAlbm(comboBoxAlb);
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
            muestradatosAlbn("Album", dataGridAlbum);
            buttonModAlb.Enabled = false;
            buttonDelAlb.Enabled = false;
            muestraDatos("Artista", dataGridView5);
            llaveforanAlbm(comboBoxAlb);
        }

        private void buttonAddDet_Click(object sender, EventArgs e)
        {
            string nombre = idPlay[comboBoxPlayList.SelectedIndex];
            string pais = idCancion[comboBoxCan.SelectedIndex];
            string cadena = "INSERT INTO DetallePlaylist(idPlaylist,idCancion)" +
               "VALUES ('" + nombre + "','" + pais + "')";
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBoxPlayList.SelectedItem = null;
            comboBoxCan.SelectedItem = null;
            muestradatosDetallP("DetallePlaylist", dataGridDetalle);
            muestraDatosPlaylist("Playlist", dataGridView6);
        }

        private void dataGridDetalle_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridDetalle.CurrentRow.Index;

            string id = dataGridDetalle.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            //comboBox2.SelectedItem = comboBox2.SelectedIndex\
            comboBoxPlayList.SelectedItem = dataGridDetalle.Rows[index].Cells[1].Value.ToString();
            comboBoxCan.SelectedItem = dataGridDetalle.Rows[index].Cells[2].Value.ToString();
            buttonModDet.Enabled = true;
            buttonDelDet.Enabled = true;
        }

        private void dataGridCancion_Click(object sender, EventArgs e)
        {
            int index;
            index = dataGridCancion.CurrentRow.Index;

            string id = dataGridCancion.Rows[index].Cells[0].Value.ToString();
            idAux = int.Parse(id);
            //comboBox2.SelectedItem = comboBox2.SelectedIndex\
            comboBoxAlb.Text = dataGridCancion.Rows[index].Cells[1].Value.ToString();
            comboBoxComp.Text = dataGridCancion.Rows[index].Cells[2].Value.ToString();
            textBoxCan.Text = dataGridCancion.Rows[index].Cells[3].Value.ToString();
            string[] pal = dataGridCancion.Rows[index].Cells[6].Value.ToString().Split(':');
            textBox10.Text = pal[1];
            textBox9.Text = pal[2];
            comboBox5.Text = dataGridCancion.Rows[index].Cells[5].Value.ToString();
            dateTimeLanza.Text = dataGridCancion.Rows[index].Cells[4].Value.ToString();
            buttonModCan.Enabled = true;
            buttonDelCan.Enabled = true;
        }


        private void buttonDelCan_Click_1(object sender, EventArgs e)
        {
            string id_Album = idAlbum[comboBoxAlb.SelectedIndex];
            string id_Compositor = separaId(comboBoxComp.Text);
            string nombre = textBoxCan.Text;
            string Duracion = textBox10.Text + ":" + textBox9.Text;
            string fechaLan = dateTimeLanza.Value.ToString("yyyy/MM/dd");
            string explicita = comboBox5.Text;
            //MessageBox.Show(fechaFun);

            string cadena = "DELETE FROM Cancion  WHERE idCancion=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBoxAlb.SelectedItem = null;
            comboBoxComp.SelectedItem = null;
            textBoxCan.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            comboBox5.SelectedItem = null;
            muestradatosAlbn("Album", dataGridAlbum);
            muestraDatos("Compositor", dataGridView4);
            muestradatosCan("Cancion", dataGridCancion);
            llaveforacan(comboBoxCan);
            //llaveforanAlbm(comboBoxAlb);
            //muestraDatos("Disquera", dataGridView6);
        }

        private void buttonModDet_Click(object sender, EventArgs e)
        {
            string nombre = idPlay[comboBoxPlayList.SelectedIndex];
            string pais = idCancion[comboBoxCan.SelectedIndex];
            
            string cadena = "UPDATE DetallePlaylist SET idPlaylist = '" + nombre + "' , idCancion = '" + pais + "' WHERE idDetallePlaylist=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBoxPlayList.SelectedItem = null;
            comboBoxCan.SelectedItem = null;
            muestradatosDetallP("DetallePlaylist", dataGridDetalle);
            muestraDatosPlaylist("Playlist", dataGridView6);
            buttonModDet.Enabled = false;
            buttonDelDet.Enabled = false;
        }

        private void buttonDelDet_Click(object sender, EventArgs e)
        {
            string nombre = idPlay[comboBoxPlayList.SelectedIndex];
            string pais = idCancion[comboBoxCan.SelectedIndex];
            string cadena = "DELETE FROM DetallePlaylist  WHERE idDetallePlaylist=" + idAux.ToString();
            SqlCommand comando = new SqlCommand(cadena, conexion);

            comando.ExecuteNonQuery();
            comboBoxPlayList.SelectedItem = null;
            comboBoxCan.SelectedItem = null;
            muestradatosDetallP("DetallePlaylist", dataGridDetalle);
            muestraDatosPlaylist("Playlist", dataGridView6);
            buttonModDet.Enabled = false;
            buttonDelDet.Enabled = false;
        }
    }
}
