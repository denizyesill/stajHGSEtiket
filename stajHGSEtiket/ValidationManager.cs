using stajHGSEtiket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace stajHGSEtiket
{
    public class ValidationManager
    {
        public bool ValidateRegistration(RegisterModel model)
        {
            bool retVal = true;
            if (string.IsNullOrEmpty(model.ad) || string.IsNullOrEmpty(model.soyad) || string.IsNullOrEmpty(model.tc) || string.IsNullOrEmpty(model.tel) || string.IsNullOrEmpty(model.mail) || string.IsNullOrEmpty(model.hgsetiket) || string.IsNullOrEmpty(model.aracsınıfı) || string.IsNullOrEmpty(model.bakiyebilgisi) || string.IsNullOrEmpty(model.plaka))
            {
                return false;
            }
            else if (!Regex.IsMatch(model.ad, "^[a-zA-Z]+$") || string.IsNullOrEmpty(model.ad))//AD Soyad Sayı içermez
            {
                MessageBox.Show("Ad kısmında numeric karakter olamaz veya boş geçilemez.");
                   
            }
            else if (!Regex.IsMatch(model.soyad, "^[a-zA-Z]+$") || string.IsNullOrEmpty(model.soyad))
            {
                MessageBox.Show("Soyad kısmında numeric karakter olamaz veya boş geçilemez.");
            }
            else if (!Regex.IsMatch(model.tc, @"^\d{11}$") || string.IsNullOrEmpty(model.tc))
            {
                MessageBox.Show("TC  kısmında karakter olamaz veya boş geçilemez.");
            }
            else if (!Regex.IsMatch(model.tel, @"^[0-9]+$") || string.IsNullOrEmpty(model.tel))
            {
                MessageBox.Show("Telefon kısmında karakter olamaz veya boş geçilemez.");
            }
            else if (!Regex.IsMatch(model.mail, "[a-zA-Z]") || string.IsNullOrEmpty(model.mail) || !Regex.IsMatch(model.mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Mail boş geçilemez veya geçersiz mail adresi.");
            }
            else if (!Regex.IsMatch(model.hgsetiket, @"^[0-9]+$") || string.IsNullOrEmpty(model.hgsetiket))
            {
                MessageBox.Show("HGS Etiket kısmında karakter olamaz veya boş geçilemez.");
            }
            else if (!Regex.IsMatch(model.aracsınıfı, "^[1-6]$") || string.IsNullOrEmpty(model.aracsınıfı))
            {
                MessageBox.Show("Arac Sınıfı kısmında karakter olamaz veya boş geçilemez.");
            }
            else if (!Regex.IsMatch(model.bakiyebilgisi, @"^[0-9]+$") || string.IsNullOrEmpty(model.bakiyebilgisi))
            {
                MessageBox.Show("Bakiye Bilgisi kısmında karakter olamaz veya boş geçilemez.");
            }
            else if (!Regex.IsMatch(model.plaka, "[a-zA-Z]") || string.IsNullOrEmpty(model.plaka) || !Regex.IsMatch(model.plaka, "[0-9]"))
            {
                MessageBox.Show("Plaka boş geçilemez.");
            }
            else
                return true;


            return retVal;
        }

        public bool ValidateLogin(LoginModel model)
        {
            bool retVal = true;
            //if (string.IsNullOrEmpty(model.username) || string.IsNullOrEmpty(model.password))
            //{
            //    return false;
            //}
             if (!Regex.IsMatch(model.username, "^[a-zA-Z]+$") || string.IsNullOrEmpty(model.username))
            {
                MessageBox.Show("Username kısmında numeric karakter olamaz veya boş geçilemez.");
                return false;
            }
            else if (!Regex.IsMatch(model.password, @"^[0-9]+$") || string.IsNullOrEmpty(model.password))
            {
                MessageBox.Show("password kısmında karakter olamaz veya boş geçilemez.");
                return false;
            }
            else
                return true;
          //  return retVal;
        }



       



    }
}
