using stajHGSEtiket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using log4net;

namespace stajHGSEtiket
{
    public partial class Form1 : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Form1));

        string connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        SqlDataAdapter dataAdapter;
        DataSet dataSet;
        List<Changed> lst;
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hGSStajDataSet3.Table_2' table. You can move, or remove it, as needed.
            this.table_2TableAdapter3.Fill(this.hGSStajDataSet3.Table_2);
                       
            LoadData();
            lst = new List<Changed>();
        }
        private void dgvCellChanged()
        {

        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    string query = "SELECT * FROM [HGSStaj].[dbo].[Table_2]";
                    dataAdapter = new SqlDataAdapter(query, connection);


                    dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "[HGSStaj].[dbo].[Table_2]");


                    dataGridView1.DataSource = dataSet.Tables["[HGSStaj].[dbo].[Table_2]"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken hata oluştu: " + ex.Message);
            }

        }


        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                int effectedRows = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var item in lst)
                    {
                        string query = "UPDATE [HGSStaj].[dbo].[Table_2] SET " + item.columnName + " = '" + item.cellValue + "'  WHERE id = " + item.idValue;

                        // UPDATE[HGSStaj].[dbo].[Table_2] SET ad = 'denizz' WHERE id = 1
                        //columname = ad , cellvalue = denizz , 1 =idvalue
                        // string query = "UPDATE * FROM [HGSStaj].[dbo].[Table_2] WHERE [id] = @idValue";

                        SqlCommand command = new SqlCommand(query, connection);
                        effectedRows += command.ExecuteNonQuery();

                    }
                    //ternary operation
                    MessageBox.Show(effectedRows == lst.Count ? "Veriler güncellendi." : "Hata");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler güncellenirken hata oluştu: " + ex.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = new DataGridView();
            //dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
        }



        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void kayıtol_Click(object sender, EventArgs e)
        {
            #region Params


            string ad = txtAd.Text;
            string soyad = txtSoyad.Text;
            string tc = txtTc.Text;
            string tel = txtTel.Text;
            string mail = txtMail.Text;
            string hgs = txtHgsEtiket.Text;
            string aracsınıfı = txtAracSınıfı.Text;
            string bakiyebilgisi = txtBakiye.Text;
            string plaka = txtPlaka.Text;

            errorProvider1.SetError(txtAd, "Ad kısmında numeric karakter olamaz veya boş geçilemez.");
            errorProvider1.SetError(txtSoyad, "Soyad kısmında numeric karakter olamaz veya boş geçilemez.");
            errorProvider1.SetError(txtTc, "TC kısmında karakter olamaz veya boş geçilemez.");
            errorProvider1.SetError(txtTel, "Telefon kısmında karakter olamaz veya boş geçilemez.");
            errorProvider1.SetError(txtMail, "Mail boş geçilemez.");
            errorProvider1.SetError(txtHgsEtiket, "HGS Etiket kısmında karakter olamaz veya boş geçilemez.");
            errorProvider1.SetError(txtAracSınıfı, "Arac Sınıfı kısmında karakter olamaz veya boş geçilemez.");
            errorProvider1.SetError(txtBakiye, "Bakiye Bilgisi kısmında karakter olamaz veya boş geçilemez.");
            errorProvider1.SetError(txtPlaka, "Plaka boş geçilemez.");


            ValidationManager manager = new ValidationManager();
            RegisterModel register = new RegisterModel();

            register.ad = ad;
            register.soyad = soyad;
            register.tc = tc;
            register.tel = tel;
            register.mail = mail;
            register.hgsetiket = hgs;
            register.aracsınıfı = aracsınıfı;
            register.bakiyebilgisi = bakiyebilgisi;
            register.plaka = plaka;
            #endregion

            bool validationResult = manager.ValidateRegistration(register);

            if (!validationResult)
            {
                MessageBox.Show("Validation Hatası.");
                return;
            }

           

            string connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("insert * into [HGSStaj].[dbo].[Table_2]", connection);

                string add = "INSERT INTO [HGSStaj].[dbo].[Table_2] (ad, soyad, tcno, tel, mail, hgsetiket, aracsınıfı,bakiyebilgisi,plaka) values (@ad," +
               " @soyad, @tcno, @tel, @mail, @hgsetiket, @aracsınıfı, @bakiyebilgisi, @plaka)";

                command = new SqlCommand(add, connection);

                //command.Parameters.AddWithValue("@ad", ad);
                //command.Parameters.AddWithValue("@soyad", soyad);
                //command.Parameters.AddWithValue("@tcno", int.Parse(tc));
                //command.Parameters.AddWithValue("@tel", int.Parse(tel));
                //command.Parameters.AddWithValue("@mail", mail);
                //command.Parameters.AddWithValue("@hgsetiket", hgs);
                //command.Parameters.AddWithValue("@aracsınıfı", aracsınıfı);
                //command.Parameters.AddWithValue("@bakiyebilgisi", bakiyebilgisi);

                try
                {
                    
                    command.Parameters.AddWithValue("@ad", ad);
                    command.Parameters.AddWithValue("@soyad", soyad);
                    command.Parameters.AddWithValue("@tcno", tc);
                    command.Parameters.AddWithValue("@tel", tel);
                    command.Parameters.AddWithValue("@mail", mail);
                    command.Parameters.AddWithValue("@hgsetiket", hgs);
                    command.Parameters.AddWithValue("@aracsınıfı", aracsınıfı);
                    command.Parameters.AddWithValue("@bakiyebilgisi", bakiyebilgisi);
                    command.Parameters.AddWithValue("@plaka", plaka);


                    int effectedRows = command.ExecuteNonQuery();
                    if (effectedRows > 0)
                    {
                        //MessageBox.Show("Kayıt Başarılı");
                        log.Info("İşlem başarılı.");
                    }
                    else
                    {
                        //MessageBox.Show("Kayıt Başarısız");
                        log.Info("İşlem başarısız.");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Hata !!" + ex);
                    log.Error("Hata oluştu.", ex);
                }



            }

            

        }


        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            string row = "";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    connection.Open();



                    SqlCommand command = new SqlCommand("select * from [HGSStaj].[dbo].[Table_2]", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row += reader[i].ToString() + "\t\n";

                        }

                        // listBox1.Items.Add(row);
                        row = string.Empty;
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Bağlantı hatası: " + ex.Message);
                }
            }

        }

        private void dataGridView1_DataMemberChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Changed changed = new Changed();
            bool control = false;
            int rownIndex = e.RowIndex;
            int columnnIndex = e.ColumnIndex;
            if (rownIndex >= 0 && columnnIndex >= 0)
            {

                var cellValue = dataGridView1.Rows[rownIndex].Cells[columnnIndex].Value.ToString();
                var columnName = dataGridView1.Columns[columnnIndex].Name;
                var idValue = dataGridView1.Rows[rownIndex].Cells[dataGridView1.ColumnCount - 1].Value.ToString();
                changed.cellValue = cellValue;
                changed.columnName = columnName;
                changed.idValue = idValue;

                foreach (var item in lst)
                {
                    if (item.cellValue == cellValue && item.columnName == columnName && item.idValue == idValue)
                    {
                        control = true;
                        break;
                    }
                }
                if (!control)
                {
                    lst.Add(changed);
                }


            }
        }
        public class Changed
        {
            public string cellValue { get; set; }
            public string columnName { get; set; }
            public string idValue { get; set; }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }





        //private Form2 form2;

        //private void ShowForm2()
        //{
        //    form2 = new Form2();
        //   // form2.Show();
        //}



        private void btnBakiyeYukle_Click(object sender, EventArgs e)
        {
            // ShowForm2();
            // MessageBox.Show(sss.variable);

            //validasyon

            string connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";

            string plakano = textBox1.Text;
            string bakiye = txtBakiyeYukle.Text;
            string a = sss.variable;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("insert * into [HGSStaj].[dbo].[bakiyeyuklemeislemleri]", connection);
                               

                SqlCommand commandBakiyeGuncelle = new SqlCommand("insert * into [HGSStaj].[dbo].[Table_2]", connection);

                string query = "UPDATE [HGSStaj].[dbo].[Table_2] SET bakiyebilgisi = bakiyebilgisi + '"+bakiye+"' WHERE plaka = '"+plakano+"'";


                SqlCommand commandHgsEtiket = new SqlCommand("INSERT INTO [HGSStaj].[dbo].[bakiyeyuklemeislemleri] hgsetiket values ");

                string addHgsEtiket = "SELECT hgsetiket FROM [HGSStaj].[dbo].[Table_2] WHERE plaka = '"+plakano+"'";
               

                commandHgsEtiket = new SqlCommand(addHgsEtiket, connection);
                commandBakiyeGuncelle = new SqlCommand(query, connection);
               

                var ccc = commandHgsEtiket.ExecuteReader();
                List<string> strings = new List<string>();
                while (ccc.Read())
                {
                    var etkt = ccc["hgsetiket"];
                    strings.Add(etkt.ToString());
                   
                }
                ccc.Close();

                string add = "INSERT INTO [HGSStaj].[dbo].[bakiyeyuklemeislemleri] (balance, plateno, username, hgsetiket) values (@bakiye, @plakano, @a, @etkt )";

                command = new SqlCommand(add, connection);



                try
                {
                    
                    commandBakiyeGuncelle.Parameters.AddWithValue("@bakiye", bakiye);
                    commandBakiyeGuncelle.Parameters.AddWithValue("@plaka", plakano);
                    command.Parameters.AddWithValue("@bakiye", bakiye);
                    command.Parameters.AddWithValue("@a", a);
                    command.Parameters.AddWithValue("@plakano", plakano);
                    command.Parameters.AddWithValue("@etkt", strings.First());

                    int effectedRows = command.ExecuteNonQuery();
                    int effectedRows1 = commandBakiyeGuncelle.ExecuteNonQuery();
                    if (effectedRows > 0 && effectedRows1 > 0)
                    {
                        MessageBox.Show("Kayıt Başarılı");

                    }
                    else
                    {
                        MessageBox.Show("Kayıt Başarısız");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata !!" + ex);
                }



            }



        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
