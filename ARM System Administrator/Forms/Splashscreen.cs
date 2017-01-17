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
    public partial class Splashscreen : Form
    {
        public Classes.ConnectorSQL conMySQL;
        public Splashscreen(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        public string con_splash_screen;
        
        private void Splashscreen_Load(object sender, EventArgs e)
        {
            ConLabel.Text = "Проверка подключения...";
            timer1.Interval = 1000;
            timer1.Start();
        }

        //Дешифрирование
        public string Decrypt(string cipherText, string password,
           string salt = "Kosher", string hashAlgorithm = "SHA1",
           int passwordIterations = 2, string initialVector = "OFRna73m*aze01xY",
            int keySize = 256)
        {
            if (string.IsNullOrEmpty(cipherText))
                return "";

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int byteCount = 0;

            using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initialVectorBytes))
            {
                using (MemoryStream memStream = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmetricKey.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }
        

        //Проверка соеденения
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Если существует файл настроек подключения
            if (File.Exists(@"Data\MySQLcon.cfg"))
            {
                Main main = new Main(conMySQL);
                StreamReader sreader = new StreamReader(@"Data\MySQLcon.cfg");

                string ConnectionString = sreader.ReadLine(); //читаем шифрованные данные о подключении в переменную 

                //Дешифрируем строку подключения
                con_splash_screen = Decrypt(ConnectionString, "Passpord11", "Password22", "SHA1", 2,
                                                 "16CHARSLONG12345", 256); 

                sreader.Close();
                sreader.Dispose();
                if (conMySQL.Connection(con_splash_screen) == true)
                {
                    timer1.Stop();
                    Autorizatsia Aut = new Autorizatsia(conMySQL);
                    Aut.Show();
                    this.Hide();
                }
                else
                {
                    ConLabel.Text = "Ошибка подключения...";
                    timer1.Stop();
                    if (MessageBox.Show("Не удалось подключиться к базе данных, Web-сервер выключен или неправильно указаны данные соединения.\nНастроить подключение?",
                        "Ошибка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Settings Stngs = new Settings(conMySQL);
                        Stngs.Show();
                        Stngs.button3.Enabled = false;
                        Stngs.button1.Enabled = true;
                        this.Hide();
                    }
                    else
                    {
                        Application.Exit();
                        conMySQL.CloseConnection();
                    }
                }
            }
            else
            {
                StreamWriter wreader = new StreamWriter(@"Data\MySQLcon.cfg");
                wreader.Close();
            }
        }
    }
}