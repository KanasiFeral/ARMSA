using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ARM_System_Administrator.Forms
{
    public partial class Settings : Form
    {
        public Classes.ConnectorSQL conMySQL;
        public Settings(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        //Кнопка "Отмена", для отмены действий
        private void button1_Click(object sender, EventArgs e)
        {
            Splashscreen SplashScreen = new Splashscreen(conMySQL);
            SplashScreen.Show();
            this.Close();
        }

        //Процедура шифрования данных
        public string Encrypt(string plainText, string password,
             string salt = "Kosher", string hashAlgorithm = "SHA1",
           int passwordIterations = 2, string initialVector = "OFRna73m*aze01xY",
            int keySize = 256)
        {
            if (string.IsNullOrEmpty(plainText))
                return "";

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            byte[] cipherTextBytes = null;

            using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initialVectorBytes))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmetricKey.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }

        //Кнопка "Создать соеденение", для добавления данных о подключении в кофигурационный файл
        private void button2_Click(object sender, EventArgs e)
        {
            if (DBTextBox.Text.Equals("") || (ServerTextBox.Text.Equals("")) || (UserTextBox.Text.Equals("")))
            {
                MessageBox.Show("Вы не ввели все данные!","Предупреждение!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Шифрование данных
                string ConnectionString;
                ConnectionString = "Database=" + DBTextBox.Text + ";Server=" + ServerTextBox.Text +
                    ";UserId=" + UserTextBox.Text + ";Password=" + PassTextBox.Text + ";Charset = cp1251";

                string EncryptString = Encrypt(ConnectionString, "Passpord11", "Password22", "SHA1", 2,
                                                 "16CHARSLONG12345", 256);

                StreamWriter writer = new StreamWriter(@"Data\\MySQLcon.cfg");
                writer.WriteLine(EncryptString);
                writer.Close();
                writer.Dispose();
                MessageBox.Show("Данные были сохранены!","Созданно!");
                Splashscreen SP = new Splashscreen(conMySQL);
                SP.Show();
                this.Close();
            }
        }

        //Кнопка "Выход в главное меню", чтобы закрыть форму настроек
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Main main = new Main(conMySQL);
            main.admin_status = true;
            main.Show();
        }
    }
}
