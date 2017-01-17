using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class Autorizatsia : Form
    {
        public Classes.ConnectorSQL conMySQL;
        public Autorizatsia(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        //Кнопка "Выход из программы", закрытие программы
        private void button2_Click(object sender, EventArgs e)
        {
            conMySQL.CloseConnection();
            Application.Exit(); //Выходим из программы
        }

        //Загрузка формы
        private void Autorizatsia_Load(object sender, EventArgs e)
        {
            PassTextBox.UseSystemPasswordChar = true; //Включаем системное отображение пароля
            comboBox1.SelectedIndex = 0;
            PassTextBox.Focus();
        }

        //Переключатель, нужен для отбражения пароля или скрытия его
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                PassTextBox.UseSystemPasswordChar = false; //Включаем символы
            }
            else
            {
                PassTextBox.UseSystemPasswordChar = true; //Скрываем символы
            }
        }

        //Кнопка "Вход", для входа в программу
        private void button1_Click(object sender, EventArgs e)
        {
            Vhod();
        }

        //Проверка нажатия кнопки Enter
        private void PassTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Vhod();
            }
        }

        //Процедура входа в программу для администратора
        public void EnterToProgrammAdmin(string StatusPol, string password_user, bool ProverkaPasword, Main main)
        {
            if (ProverkaPasword == true)
            {
                if (PassTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Вы не ввели все данные!", "Предупреждение!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    main = new Main(conMySQL);
                    main.admin_status = true;
                    string queryToBool = "select * from `Users` where (`Name_user` = '"
                                + StatusPol + "' and `Pass_user` = '" + password_user + "')";


                    if (conMySQL.QueryToBool(queryToBool) == true)
                    {                        
                        main.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ваш пароль неверен, проверьте корректность данных", "Ошибка");
                    }
                }
            }
            else
            {
                main = new Main(conMySQL);
                main.admin_status = true;
                string queryToBool = "select * from `Users` where (`Name_user` = '"
                            + StatusPol + "' and `Pass_user` = '" + password_user + "')";


                if (conMySQL.QueryToBool(queryToBool) == true)
                {
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ваш пароль неверен, проверьте корректность данных", "Ошибка");
                }
            }
        }

        //Процедура входа в программу для преподавателя
        public void EnterToProgrammUser(string StatusPol, string password_user, bool ProverkaPasword, MenuPrepod menuPrep)
        {
            if (ProverkaPasword == true)
            {
                if (PassTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Вы не ввели все данные!", "Предупреждение!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    menuPrep = new MenuPrepod(conMySQL);
                    menuPrep.admin_status = false;

                    string queryToBool = "select * from `Users` where (`Name_user` = '"
                    + StatusPol + "' and `Pass_user` = '" + password_user + "')";


                    if (conMySQL.QueryToBool(queryToBool) == true)
                    {
                        menuPrep.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ваш пароль неверен, проверьте корректность данных", "Ошибка");
                    }
                }
            }
            else
            {
                menuPrep = new MenuPrepod(conMySQL);
                menuPrep.admin_status = false;

                string queryToBool = "select * from `Users` where (`Name_user` = '"
                + StatusPol + "' and `Pass_user` = '" + password_user + "')";


                if (conMySQL.QueryToBool(queryToBool) == true)
                {
                    menuPrep.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ваш пароль неверен, проверьте корректность данных", "Ошибка");
                }
            }
        }

        //Процедура входа в систему, проверка на пользователя
        public void Vhod()
        {
            string de_admin_Pass = String.Empty;
            string en_admin_Pass = String.Empty;

            string de_user_Pass = String.Empty;
            string en_user_Pass = String.Empty;

            string StatusPol;
            string password_user = PassTextBox.Text; //Записываем в переменную пароль из текст бокса
            int x = comboBox1.SelectedIndex;
            if (x == 0)
            {
                Main main = new Main(conMySQL);
                StatusPol = "admin";
                //main.admin_status = true;

                if ((password_user != String.Empty) || (password_user != ""))
                {
                    if (checkBoxSavePass.Checked == true) //Хотим сохранить пароль в файл
                    {
                        string queryToBool = "select * from `Users` where (`Name_user` = '"
                                + StatusPol + "' and `Pass_user` = '" + password_user + "')";

                        if (conMySQL.QueryToBool(queryToBool) == true)
                        {
                            StreamWriter writer = new StreamWriter(@"Data\Txt\PasswordAdmin.txt");

                            string admin_pass = password_user;

                            string en_admin_pass = Encrypt(admin_pass, "Passpord11", "Password22", "SHA1", 2,
                                                 "16CHARSLONG12345", 256);

                            writer.WriteLine(en_admin_pass);
                            writer.Close();
                            writer.Dispose();

                            EnterToProgrammAdmin(StatusPol, password_user, true, main); //Процедура входа в программу
                        }
                    }
                    else //Галку не ставим, просто хотим зайти в программу
                    {
                        EnterToProgrammAdmin(StatusPol, password_user, true, main);
                    }
                }
                else
                {
                    if (File.Exists(@"Data\Txt\PasswordAdmin.txt"))
                    {
                        StreamReader sreader = new StreamReader(@"Data\Txt\PasswordAdmin.txt");

                        string admin_Pass = sreader.ReadLine(); //читаем шифрованные данные о пароле в переменную 

                        //Дешифрируем строку подключения
                        try
                        {
                            de_admin_Pass = Decrypt(admin_Pass, "Passpord11", "Password22", "SHA1", 2, "16CHARSLONG12345", 256);

                            EnterToProgrammAdmin(StatusPol, de_admin_Pass, false, main); //Процедура входа в программу
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageBox.Show("Файл с сохранненым паролем был поврежден! Файл с сохранненым паролем будет удален!", "Сообщение", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            sreader.Close();
                            sreader.Dispose();

                            File.Delete(@"Data\Txt\PasswordAdmin.txt");
                        }

                        sreader.Close();
                        sreader.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Файла с сохраненными настройками отсутствует! Для создания введите пароль и поставьте галочку на поле\n<<Сохранить пароль при входе>>", 
                            "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            } //Конец выборки


            else //Для преподавателя
            {
                MenuPrepod menuPrep = new MenuPrepod(conMySQL);
                StatusPol = "user";

                if ((password_user != String.Empty) || (password_user != ""))
                {
                    if (checkBoxSavePass.Checked == true) //Хотим сохранить пароль в файл
                    {
                        string queryToBool = "select * from `Users` where (`Name_user` = '"
                                + StatusPol + "' and `Pass_user` = '" + password_user + "')";

                        if (conMySQL.QueryToBool(queryToBool) == true)
                        {
                            StreamWriter writer = new StreamWriter(@"Data\Txt\PasswordUser.txt");

                            string user_pass = password_user;

                            string en_user_pass = Encrypt(user_pass, "Passpord11", "Password22", "SHA1", 2,
                                                 "16CHARSLONG12345", 256);

                            writer.WriteLine(en_user_pass);
                            writer.Close();
                            writer.Dispose();

                            EnterToProgrammUser(StatusPol, password_user, true, menuPrep); //Процедура входа в программу
                        }
                    }
                    else //Галку не ставим, просто хотим зайти в программу
                    {
                        EnterToProgrammUser(StatusPol, password_user, true, menuPrep);
                    }
                }
                else
                {
                    if (File.Exists(@"Data\Txt\PasswordUser.txt"))
                    {
                        StreamReader sreader = new StreamReader(@"Data\Txt\PasswordUser.txt");

                        string user_Pass = sreader.ReadLine(); //читаем шифрованные данные о пароле в переменную 

                        //Дешифрируем строку подключения
                        try
                        {
                            de_user_Pass = Decrypt(user_Pass, "Passpord11", "Password22", "SHA1", 2, "16CHARSLONG12345", 256);

                            EnterToProgrammUser(StatusPol, de_user_Pass, false, menuPrep); //Процедура входа в программу
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageBox.Show("Файл с сохранненым паролем был поврежден! Файл с сохранненым паролем будет удален!", "Сообщение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            sreader.Close();
                            sreader.Dispose();

                            File.Delete(@"Data\Txt\PasswordUser.txt");
                        }

                        sreader.Close();
                        sreader.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Файла с сохраненными настройками отсутствует! Для создания введите пароль и поставьте галочку на поле\n<<Сохранить пароль при входе>>",
                            "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            } //Конец Else
        }

        //Для открытия HTML страницы справки
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            string url = @"Data\Help\Авторизация.html";
            Process.Start(url);
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
    }
}
