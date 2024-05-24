using stajHGSEtiket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;


namespace stajHGSEtiket
{
    public partial class Form2 : Form
    {
      // public string globalusername = string.Empty;

       // public string globalusername;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            #region Params
            
            string username = textBox1.Text;
           // globalusername = username;
            string password = textBox2.Text;
            sss m = new sss();
            sss.variable = username;


            ValidationManager manager = new ValidationManager();
            LoginModel register = new LoginModel();


            register.username = username;
            register.password = password;
            #endregion


            bool validationResult = manager.ValidateLogin(register);

            if (!validationResult)
            {
                MessageBox.Show("Validation Hatası.");
                return;
            }



            //ValidationManager mngr = new ValidationManager();
            //bool valResult = mngr.ValidateLogin();
            //if (!valResult)
            //{
            //    MessageBox.Show("Kullanıcı adı veya parola boş geçilemez.");
            //    return;
            //}
            //if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            //{

            //}


            string connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";

            string userName = textBox1.Text;
            string Password = textBox2.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    string query = "SELECT * FROM [HGSStaj].[dbo].[users] WHERE [username] = @UserName and [password] = @Password ";
                   // string query1 = "SELECT * FROM [HGSStaj].[dbo].[users] WHERE [password] = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@Password", Password); 

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                MessageBox.Show("Girdi veritabanında bulundu.");
                                Form1 frm = new Form1();
                                frm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Girdi veritabanında bulunamadı.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }





        }
    }
}
