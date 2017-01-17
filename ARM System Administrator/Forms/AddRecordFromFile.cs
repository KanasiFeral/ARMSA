using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ARM_System_Administrator.Forms
{
    public partial class AddRecordFromFile : Form
    {
        public Classes.ConnectorSQL conMySQL;
        public AddRecordFromFile(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }
        //Выбор открытия файла
        private void comboBoxChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            textFile.Clear();
            if (comboBoxChoice.SelectedIndex == 0)//Если выбраны системные блоки
            {
                panelComp.Visible = true;
                panelNout.Visible = false;
                labelTimer.Text = "";
            }
            else if (comboBoxChoice.SelectedIndex == 1)//Если выбраны ноутбуки
            {
                panelComp.Visible = true;
                panelNout.Visible = true;
                labelTimer.Text = "";              
            }

            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "txt|*.txt|html|*.html|htm|*.htm|all|*";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(OpenFile.FileName, Encoding.Default);
                textFile.Text = sr.ReadToEnd();
                sr.Dispose();
                sr.Close();
                buttonWriteText.Enabled = true;
                buttonClearText.Enabled = true;
                buttonAddRecord.Enabled = true;                
                ClearTextBoxNout();
                ClearTextBoxComp();
            }
            else
            {
                panelComp.Visible = false;
                panelNout.Visible = false;
                buttonWriteText.Enabled = false;
                buttonClearText.Enabled = false;
                buttonAddRecord.Enabled = false;
                labelTimer.Text = "";
                ClearTextBoxNout();
                ClearTextBoxComp();
            }
        }

        public int Time;

        //Анализируем открытый нами текст
        private void buttonWriteText_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = textFile.Lines.Length;
            labelTimer.Text = "";
            if (comboBoxChoice.SelectedIndex == 0)//Если выбраны системные блоки
            {
                string NameOS = "", NameProc = "", NameVideo = "", NamePamyat = "", NameHDD = "", NamePitanie = "", NameMatPlata = "", NameCDRom = "";

                //Начинаем считывание и поиск искомых данных

                string[] WordSystem = {"Операционная система", "Тип ЦП", "Видеоадаптер",
                                  "Системная память", "Дисковый накопитель", "Напряжение батареи", "Системная плата", "Оптический накопитель"};

                try
                {
                    for (int i = 0; i < textFile.Lines.Length; i++)
                    {
                        labelTimer.Text = "Анализ начат, обработано: " + i + " строк.";
                        //Операционная система
                        if (textFile.Lines[i].Contains(WordSystem[0].ToString()))
                        {
                            NameOS = textFile.Lines[i];
                        }
                        //Тип ЦП
                        else if (textFile.Lines[i].Contains(WordSystem[1].ToString()))
                        {
                            NameProc = textFile.Lines[i];
                        }
                        //Видеоадаптер
                        else if (textFile.Lines[i].Contains(WordSystem[2].ToString()))
                        {
                            NameVideo = textFile.Lines[i];
                        }
                        //Системная память
                        else if (textFile.Lines[i].Contains(WordSystem[3].ToString()))
                        {
                            NamePamyat = textFile.Lines[i];
                        }
                        //Дисковый накопитель
                        else if (textFile.Lines[i].Contains(WordSystem[4].ToString()))
                        {
                            NameHDD = textFile.Lines[i];
                        }
                        //Напряжение батареи
                        else if (textFile.Lines[i].Contains(WordSystem[5].ToString()))
                        {
                            NamePitanie = textFile.Lines[i];
                        }
                        //Системная плата
                        else if (textFile.Lines[i].Contains(WordSystem[6].ToString()))
                        {
                            NameMatPlata = textFile.Lines[i];
                        }
                        //Оптический накопитель
                        else if (textFile.Lines[i].Contains(WordSystem[7].ToString()))
                        {
                            NameCDRom = textFile.Lines[i];
                        }

                        progressBar1.Value = i;
                        progressBar1.Refresh();


                        Application.DoEvents();
                    }

                    //Операционная система
                    int posOS = NameOS.IndexOf(WordSystem[0]) + WordSystem[0].Length;
                    if (posOS >= WordSystem[0].Length)
                    {
                        NameOS = NameOS.Substring(posOS);
                        textAddOSComp.Text = NameOS.Trim();
                    }
                    //Тип ЦП
                    int posTSP = NameProc.IndexOf(WordSystem[1]) + WordSystem[1].Length;
                    if (posTSP >= WordSystem[1].Length)
                    {
                        NameProc = NameProc.Substring(posTSP);
                        textAddProcComp.Text = NameProc.Trim();
                    }
                    //Видеоадаптер
                    int posVid = NameVideo.IndexOf(WordSystem[2]) + WordSystem[2].Length;
                    if (posVid >= WordSystem[2].Length)
                    {
                        NameVideo = NameVideo.Substring(posVid);
                        textAddVideoComp.Text = NameVideo.Trim();
                    }
                    //Системная память
                    int posPam = NamePamyat.IndexOf(WordSystem[3]) + WordSystem[3].Length;
                    if (posPam >= WordSystem[3].Length)
                    {
                        NamePamyat = NamePamyat.Substring(posPam);
                        textAddOZUComp.Text = NamePamyat.Trim();
                    }
                    //Дисковый накопитель
                    int posHDD = NameHDD.IndexOf(WordSystem[4]) + WordSystem[4].Length;
                    if (posHDD >= WordSystem[4].Length)
                    {
                        NameHDD = NameHDD.Substring(posHDD);
                        textAddHDDComp.Text = NameHDD.Trim();
                    }
                    //Напряжение батареи
                    int posBat = NamePitanie.IndexOf(WordSystem[5]) + WordSystem[5].Length;
                    if (posBat >= WordSystem[5].Length)
                    {
                        NamePitanie = NamePitanie.Substring(posBat);
                        textAddPitanieComp.Text = NamePitanie.Trim();
                    }
                    //Системная плата
                    int posMatPlata = NameMatPlata.IndexOf(WordSystem[6]) + WordSystem[6].Length;
                    if (posMatPlata >= WordSystem[6].Length)
                    {
                        NameMatPlata = NameMatPlata.Substring(posMatPlata);
                        textAddMatPlataComp.Text = NameMatPlata.Trim();
                    }
                    //Оптический накопитель
                    int posCDRom = NameCDRom.IndexOf(WordSystem[7]) + WordSystem[7].Length;
                    if (posCDRom >= WordSystem[7].Length)
                    {
                        NameCDRom = NameCDRom.Substring(posCDRom);
                        textAddPrivodComp.Text = NameCDRom.Trim();
                    }

                }
                catch { MessageBox.Show("Вы открыли файл созданный не AIDA64", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else//Если выбраны ноутбуки
            {
                string Name = "", NameOS = "", NameProc = "", NameVideo = "", NameDiag = "", NamePamyat = "", NameHDD = "";

                //Начинаем считывание и поиск искомых данных

                string[] WordSystem = {"DMI система", "Операционная система"
                                      , "Тип ЦП", "Видеоадаптер", "Монитор",
                                  "Системная память", "Дисковый накопитель"};

                try
                {
                    for (int i = 0; i < textFile.Lines.Length; i++)
                    {
                        labelTimer.Text = "Анализ начат, обработано: " + i + " строк.";
                        //DMI система
                        if (textFile.Lines[i].Contains(WordSystem[0].ToString()))
                        {
                            Name = textFile.Lines[i];
                        }
                        //Операционная система
                        else if (textFile.Lines[i].Contains(WordSystem[1].ToString()))
                        {
                            NameOS = textFile.Lines[i];
                        }
                        //Тип ЦП
                        else if (textFile.Lines[i].Contains(WordSystem[2].ToString()))
                        {
                            NameProc = textFile.Lines[i];
                        }
                        //Видеоадаптер
                        else if (textFile.Lines[i].Contains(WordSystem[3].ToString()))
                        {
                            NameVideo = textFile.Lines[i];
                        }
                        //Монитор
                        else if (textFile.Lines[i].Contains(WordSystem[4].ToString()))
                        {
                            NameDiag = textFile.Lines[i];                            
                        }
                        //Системная память
                        else if (textFile.Lines[i].Contains(WordSystem[5].ToString()))
                        {
                            NamePamyat = textFile.Lines[i];
                        }
                        //Дисковый накопитель
                        else if (textFile.Lines[i].Contains(WordSystem[6].ToString()))
                        {
                            NameHDD = textFile.Lines[i];
                        }

                        progressBar1.Value = i;
                        progressBar1.Refresh();

                        Application.DoEvents();
                    }

                    //DMI система    
                    int posName = Name.IndexOf(WordSystem[0]) + WordSystem[0].Length;
                    if (posName >= WordSystem[0].Length)
                    {
                        Name = Name.Substring(posName);
                        textAddNazvNout.Text = Name.Trim();
                    }
                    //Операционная система
                    int posOS = NameOS.IndexOf(WordSystem[1]) + WordSystem[1].Length;
                    if (posOS >= WordSystem[1].Length)
                    {
                        NameOS = NameOS.Substring(posOS);
                        textAddOSNout.Text = NameOS.Trim();
                    }
                    //Тип ЦП
                    int posTSP = NameProc.IndexOf(WordSystem[2]) + WordSystem[2].Length;
                    if (posTSP >= WordSystem[2].Length)
                    {
                        NameProc = NameProc.Substring(posTSP);
                        textAddProcNout.Text = NameProc.Trim();
                    }
                    //Видеоадаптер
                    int posVid = NameVideo.IndexOf(WordSystem[3]) + WordSystem[3].Length;
                    if (posVid >= WordSystem[3].Length)
                    {
                        NameVideo = NameVideo.Substring(posVid);
                        textAddVideoNout.Text = NameVideo.Trim();
                    }
                    //Монитор
                    int posMon = NameDiag.IndexOf(WordSystem[4]) + WordSystem[4].Length;
                    if (posMon >= WordSystem[4].Length)
                    {
                        NameDiag = NameDiag.Substring(posMon);
                        textAddDiagNout.Text = NameDiag.Trim();
                        string[] textSplit = textAddDiagNout.Text.Split('[','\"');
                        textAddDiagNout.Text = textSplit[1]; //15.6
                    }
                    //Системная память
                    int posPam = NamePamyat.IndexOf(WordSystem[5]) + WordSystem[5].Length;
                    if (posPam >= WordSystem[5].Length)
                    {
                        NamePamyat = NamePamyat.Substring(posPam);
                        textAddPamyatNout.Text = NamePamyat.Trim();
                    }
                    //Системная память
                    int posHDD = NameHDD.IndexOf(WordSystem[6]) + WordSystem[6].Length;
                    if (posHDD >= WordSystem[6].Length)
                    {
                        NameHDD = NameHDD.Substring(posHDD);
                        textAddHDDNout.Text = NameHDD.Trim();
                    }
                }
                catch { MessageBox.Show("Вы открыли файл созданный не AIDA64", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            labelTimer.Text = "Анализ завершен, обработано: " + textFile.Lines.Length + " строк.";
            progressBar1.Value = 0;

        }

        //Загрузка формы
        private void AddRecordFromFile_Load(object sender, EventArgs e)
        {
            conMySQL.QueryToComboBox("SELECT `Nomer` FROM Auditoria", textAddAuditNout, "Nomer");
            conMySQL.QueryToComboBox("SELECT `Nomer` FROM Auditoria", textAddAuditComp, "Nomer");
            textAddStatusNout.SelectedIndex = 0;
            textAddStatusComp.SelectedIndex = 0;
            labelTimer.Text = "";
        }

        //Процедура очистки полей ввода
        public void ClearTextBoxNout()
        {
            textAddInvNomerNout.Clear();
            //textAddOSNout.Clear();
            textAddNazvNout.Clear();
            textAddProcNout.Clear();
            textAddVideoNout.Clear();
            textAddDiagNout.Clear();
            textAddPamyatNout.Clear();
            textAddHDDNout.Clear();
            //textAddStatusNout.Clear();
            textAddTsenaNout.Clear();
            //textAddTipObNout.Clear();
            textAddDopInfNout.Clear();
            //textAddAuditNout.Clear();
        }

        //Процедура очистки полей ввода
        public void ClearTextBoxComp()
        {
            textAddInvNomerComp.Clear();
            //textAddOSComp.Clear();
            //textAddPrivodComp.Clear();
            textAddMatPlataComp.Clear();
            textAddOZUComp.Clear();
            textAddKuleraComp.Clear();
            textAddVideoComp.Clear();
            textAddHDDComp.Clear();
            textAddProcComp.Clear();
            textAddPitanieComp.Clear();
            //textAddStatusComp.Clear();
            textAddTsenaComp.Clear();
            textAddDopInfComp.Clear();
            //textAddAuditComp.Clear();
        }

        //Кнопка очистки полей ввода
        private void buttonClearText_Click(object sender, EventArgs e)
        {
            if (comboBoxChoice.SelectedIndex == 0)//Если выбраны системные блоки
            {
                ClearTextBoxComp();
            }
            else//Если выбраны ноутбуки
            {
                ClearTextBoxNout();
            }
        }

        public int ID_Param_sis_blokov;
        public int ID_Param_nout;

        //Кнопка добавить запись
        private void buttonAddRecord_Click(object sender, EventArgs e)
        {
            if (comboBoxChoice.SelectedIndex == 0)//Если выбраны системные блоки
            {
                if ((textAddInvNomerComp.Text.Equals("")) || (textAddOSComp.Text.Equals("")) ||
                (textAddPrivodComp.Text.Equals("")) || (textAddMatPlataComp.Text.Equals("")) ||
                (textAddOZUComp.Text.Equals("")) || (textAddKuleraComp.Text.Equals("")) ||
                (textAddVideoComp.Text.Equals("")) || (textAddHDDComp.Text.Equals("")) ||
                (textAddProcComp.Text.Equals("")) || (textAddPitanieComp.Text.Equals("")) ||
                (textAddStatusComp.Text.Equals("")) || (textAddTsenaComp.Text.Equals("")) ||
                (textAddTipObComp.Text.Equals("")) || (textAddAuditComp.Text.Equals("")) ||
                (textAddDataVvodaComp.Text.Equals("")) || (textAddDataSpisaniaComp.Text.Equals("")))
                {
                    MessageBox.Show("Не все поля введены", "Ошибка!");
                }
                else
                {
                    if (conMySQL.QueryToBool("SELECT * FROM `Param_sis_blokov`") == true)
                    {
                        string ID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_sis_blokov`");
                        try
                        {
                            ID_Param_sis_blokov = Convert.ToInt32(ID);
                            ID_Param_sis_blokov++;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        ID_Param_sis_blokov = 1;
                    }

                    string queryString = "INSERT INTO `Param_sis_blokov` VALUES (" + ID_Param_sis_blokov + ",'" + textAddInvNomerComp.Text +
                                        "','" + textAddOSComp.Text + "','" + textAddPrivodComp.Text +
                                        "','" + textAddMatPlataComp.Text + "','" + textAddOZUComp.Text +
                                        "','" + textAddKuleraComp.Text + "','" + textAddVideoComp.Text +
                                        "','" + textAddHDDComp.Text + "','" + textAddProcComp.Text +
                                        "','" + textAddPitanieComp.Text + "','" + textAddStatusComp.Text +
                                        "','" + textAddTsenaComp.Text + "','" + textAddDopInfComp.Text + "','" + textAddDataVvodaComp.Text +
                                        "','" + textAddDataSpisaniaComp.Text + "','" + textAddAuditComp.Text + "')";

                    conMySQL.QueryToBool(queryString);
                    //ClearTextBoxComp();
                }
            }
            else//Если выбраны ноутбуки
            {
                if ((textAddInvNomerNout.Text.Equals("")) ||
                (textAddOSNout.Text.Equals("")) || (textAddNazvNout.Text.Equals("")) ||
                (textAddProcNout.Text.Equals("")) || (textAddVideoNout.Text.Equals("")) ||
                (textAddDiagNout.Text.Equals("")) || (textAddPamyatNout.Text.Equals("")) ||
                (textAddHDDNout.Text.Equals("")) || (textAddStatusNout.Text.Equals("")) ||
                (textAddTsenaNout.Text.Equals("")) || (textAddTipObNout.Text.Equals("")) ||
                ((textAddAuditNout.Text.Equals(""))))
                {
                    MessageBox.Show("Не все поля введены", "Ошибка!");
                }
                else
                {
                    if (conMySQL.QueryToBool("SELECT * FROM `Param_nout`") == true)
                    {
                        string ID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_nout`");
                        try
                        {
                            ID_Param_nout = Convert.ToInt32(ID);
                            ID_Param_nout++;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        ID_Param_nout = 1;
                    }

                    string queryString = "INSERT INTO `Param_nout` VALUES (" + ID_Param_nout + ",'" + textAddInvNomerNout.Text +
                                        "','" + textAddOSNout.Text + "','" + textAddNazvNout.Text +
                                        "','" + textAddProcNout.Text + "','" + textAddVideoNout.Text +
                                        "','" + textAddDiagNout.Text + "','" + textAddPamyatNout.Text +
                                        "','" + textAddHDDNout.Text + "','" + textAddStatusNout.Text +
                                        "','" + textAddTsenaNout.Text + "','" + textAddDopInfNout.Text + "','" + textAddDataVvodaNout.Text +
                                        "','" + textAddDataSpisaniaNout.Text + "','" + textAddAuditNout.Text + "')";

                    conMySQL.QueryToBool(queryString);
                    //ClearTextBoxNout();
                }
            }
        }
    }
}