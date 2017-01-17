using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; //Ссылка на Word компоненты
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ARM_System_Administrator.Forms;
using MySql.Data.MySqlClient; //Ссылка на MySQL компоненты

namespace ARM_System_Administrator.Forms
{
    public partial class Query : Form
    {
        public Classes.Exports ExportsTo = new Classes.Exports();
        public Classes.ConnectorSQL conMySQL;
        public Query(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        public BindingSource binSourceMySQL = new BindingSource();
        public BindingSource binSourceColumn = new BindingSource();

        //Процедура настройки дата грида
        public void dataGridSettings(DataGridView dataGV)
        {
            dataGV.ReadOnly = true;
            dataGV.AllowUserToAddRows = false;
            dataGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGV.MultiSelect = false;
            comboBox1.SelectedItem = 0;
        }

        //Загрузка формы
        private void Query_Load(object sender, EventArgs e)
        {
            dataGridSettings(dataGridVColumn);
            dataGridSettings(dataGridVMySQL);
        }

        //При выборе таблицы прогружает список столбцов
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)//Таблица Users
            {
                conMySQL.LoadTable("Users", "SHOW COLUMNS FROM `Users`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 1)//Таблица Prepod
            {
                conMySQL.LoadTable("Prepod", "SHOW COLUMNS FROM `Prepod`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 2)//Таблица Auditoria
            {
                conMySQL.LoadTable("Auditoria", "SHOW COLUMNS FROM `Auditoria`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 4)//Таблица Zayavka
            {
                conMySQL.LoadTable("Zayavka", "SHOW COLUMNS FROM `Zayavka`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 5)//Таблица Param_print
            {
                conMySQL.LoadTable("Param_print", "SHOW COLUMNS FROM `Param_print`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 6)//Таблица Param_monitor
            {
                conMySQL.LoadTable("Param_monitor", "SHOW COLUMNS FROM `Param_monitor`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 7)//Таблица Param_mishek
            {
                conMySQL.LoadTable("Param_mishek", "SHOW COLUMNS FROM `Param_mishek`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 8)//Таблица Param_klava
            {
                conMySQL.LoadTable("Param_klava", "SHOW COLUMNS FROM `Param_klava`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 9)//Таблица Param_sis_blokov
            {
                conMySQL.LoadTable("Param_sis_blokov", "SHOW COLUMNS FROM `Param_sis_blokov`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 10)//Таблица Param_nout
            {
                conMySQL.LoadTable("Param_nout", "SHOW COLUMNS FROM `Param_nout`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Prochee
            {
                conMySQL.LoadTable("Prochee", "SHOW COLUMNS FROM `Prochee`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Sotrudnik
            {
                conMySQL.LoadTable("Sotrudnik", "SHOW COLUMNS FROM `Sotrudnik`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
            else if (comboBox1.SelectedIndex == 3)//Таблица Program_Obespech
            {
                conMySQL.LoadTable("Program_Obespech", "SHOW COLUMNS FROM `Program_Obespech`", binSourceColumn, dataGridVColumn, NavigatorMySQL);
            }
        }

        //Кнопка очистки поля запроса
        private void buttonClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        //Добавление строки запроса на все данные
        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)//Таблица Users
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Users` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 1)//Таблица Prepod
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Prepod` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 2)//Таблица Auditoria
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Auditoria` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 4)//Таблица Zayavka
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Zayavka` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 5)//Таблица Param_print
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Param_print` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 6)//Таблица Param_monitor
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Param_monitor` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 7)//Таблица Param_mishek
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Param_mishek` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 8)//Таблица Param_klava
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Param_klava` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 9)//Таблица Param_sis_blokov
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Param_sis_blokov` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 10)//Таблица Param_nout
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Param_nout` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Prochee
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Prochee` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Sotrudnik
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Sotrudnik` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 3)//Таблица Program_Obespech
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT * FROM `Program_Obespech` WHERE 1";
            }
        }

        //Добавление строки запроса на все данные, с названием каждого столбца
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)//Таблица Users
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Name_user`, `Pass_user` FROM `Users` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 1)//Таблица Prepod
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Name_prep`, `Fam_prep`, `Otch_prep`, `Distsiplina` FROM `Prepod` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 2)//Таблица Auditoria
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `Nomer`, `Otvetstvenii` FROM `Auditoria` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 4)//Таблица Zayavka
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `FIO_Prepod`, `id_User`, `Type_Oborud`, `Auditoria`, `Inv_nomer`, `Tsel`, `Data_zayavki`, `Data_sdachi`, `Status_zayavki` FROM `Zayavka` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 5)//Таблица Param_print
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Inv_nomer`, `Nazvanie`, `Max_raz`, `Razmeri`, `Pamyat`, `Skor_pechati`, `Max_format`, `Kol_str`, `Status`, `Tsena`, `Dop_Inf`, `Data_vvoda`, `Data_spisania`, `Auditoria` FROM `Param_print` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 6)//Таблица Param_monitor
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Inv_nomer`, `Nazvanie`, `Max_raz`, `Razmeri`, `Pamyat`, `Skor_pechati`, `Max_format`, `Kol_str`, `Status`, `Tsena`, `Tip_oboridovania`, `Dop_Inf`, `Auditoria` FROM `Param_monitor` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 7)//Таблица Param_mishek
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Inv_nomer`, `Tip_podkl`, `Status`, `Tsena`, `Tip_oboridovania`, `Dop_Inf`, `Auditoria` FROM `Param_mishek` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 8)//Таблица Param_klava
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Inv_nomer`, `Tip_podkl`, `Kol_klavish`, `Status`, `Tsena`, `Tip_oboridovania`, `Dop_Inf`, `Auditoria` FROM `Param_klava` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 9)//Таблица Param_sis_blokov
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Inv_nomer`, `OS`, `Privod`, `Mat_plata`, `Pzu`, `Kulera`, `Video`, `HDD`, `Protsessor`, `Blok_pitania`, `Status`, `Tsena`, `Tip_oboridovania`, `Dop_Inf`, `Auditoria` FROM `Param_sis_blokov` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 10)//Таблица Param_nout
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Inv_nomer`, `OS`, `Nazvanie`, `Protsessor`, `Video`, `Diag`, `Pamyat`, `HDD`, `Status`, `Tsena`, `Tip_oboridovania`, `Dop_Inf`, `Auditoria` FROM `Param_nout` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Prochee
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Inv_nomer`, `Nazvanie`, `Status`, `Tsena`, `Dop_Inf`, `Data_vvoda`, `Data_spisania`, `Auditoria` FROM `Prochee` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Sotrudnik
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Fam_sotr`, `Name_sotr`, `Otch_sotr`, `Dolgnost`, `Auditoria` FROM `Sotrudnik` WHERE 1";
            }
            else if (comboBox1.SelectedIndex == 3)//Таблица Program_Obespech
            {
                richTextBox1.Clear();
                richTextBox1.Text = "SELECT `ID`, `Tip_PO`, `Nazvanie_PO`, `Auditoria` FROM `Program_obespech` WHERE 1";
            }
        }

        //Для подтверждения запроса
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == String.Empty)
            {
                MessageBox.Show("Вы не ввели все данные", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)//Таблица Users
                {
                    conMySQL.LoadTable("Users", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 1)//Таблица Prepod
                {
                    conMySQL.LoadTable("Prepod", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 2)//Таблица Auditoria
                {
                    conMySQL.LoadTable("Auditoria", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 4)//Таблица Zayavka
                {
                    conMySQL.LoadTable("Zayavka", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 5)//Таблица Param_print
                {
                    conMySQL.LoadTable("Param_print", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 6)//Таблица Param_monitor
                {
                    conMySQL.LoadTable("Param_monitor", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 7)//Таблица Param_mishek
                {
                    conMySQL.LoadTable("Param_mishek", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow); ;
                }
                else if (comboBox1.SelectedIndex == 8)//Таблица Param_klava
                {
                    conMySQL.LoadTable("Param_klava", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 9)//Таблица Param_sis_blokov
                {
                    conMySQL.LoadTable("Param_sis_blokov", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 10)//Таблица Param_nout
                {
                    conMySQL.LoadTable("Param_nout", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 11)//Таблица Prochee
                {
                    conMySQL.LoadTable("Prochee", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 12)//Таблица Sotrudnik
                {
                    conMySQL.LoadTable("Sotrudnik", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
                else if (comboBox1.SelectedIndex == 3)//Таблица Program_Obespech
                {
                    conMySQL.LoadTable("Program_Obespech", richTextBox1.Text, binSourceMySQL, dataGridVMySQL, NavigatorShow);
                }
            }
        }

        //Кнопка MAX, для вывода максимального значения
        private void buttonMax_Click(object sender, EventArgs e)
        {
            int x = dataGridVColumn.CurrentRow.Index;
            string Name_Column = Convert.ToString(dataGridVColumn[0, x].Value);

            if (comboBox1.SelectedIndex == 0)//Таблица Users
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Users`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 1)//Таблица Prepod
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Prepod`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 2)//Таблица Auditoria
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Auditoria`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 4)//Таблица Zayavka
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Zayavka`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 5)//Таблица Param_print
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Param_print`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 6)//Таблица Param_monitor
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Param_monitor`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 7)//Таблица Param_mishek
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Param_mishek`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 8)//Таблица Param_klava
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Param_klava`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 9)//Таблица Param_sis_blokov
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Param_sis_blokov`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 10)//Таблица Param_nout
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Param_nout`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Prochee
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Prochee`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 12)//Таблица Sotrudnik
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Sotrudnik`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 3)//Таблица Program_Obespech
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT MAX(`" + Name_Column + "`) FROM `Program_Obespech`";
                richTextBox1.Text = queryStringMax;
            }
        }

        //Кнопка MIN, для вывода минимального значения
        private void buttonMin_Click(object sender, EventArgs e)
        {
            int x = dataGridVColumn.CurrentRow.Index;
            string Name_Column = Convert.ToString(dataGridVColumn[0, x].Value);

            if (comboBox1.SelectedIndex == 0)//Таблица Users
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Users`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 1)//Таблица Prepod
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Prepod`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 2)//Таблица Auditoria
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Auditoria`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 4)//Таблица Zayavka
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Zayavka`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 5)//Таблица Param_print
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Param_print`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 6)//Таблица Param_monitor
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Param_monitor`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 7)//Таблица Param_mishek
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Param_mishek`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 8)//Таблица Param_klava
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Param_klava`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 9)//Таблица Param_sis_blokov
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Param_sis_blokov`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 10)//Таблица Param_nout
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Param_nout`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Prochee
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Prochee`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 12)//Таблица Sotrudnik
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Sotrudnik`";
                richTextBox1.Text = queryStringMin;
            }
            else if (comboBox1.SelectedIndex == 3)//Таблица Program_Obespech
            {
                richTextBox1.Clear();
                string queryStringMin = "SELECT MIN(`" + Name_Column + "`) FROM `Program_Obespech`";
                richTextBox1.Text = queryStringMin;
            }
        }

        //Кнопка AVG, для вывода минимального значения
        private void buttonAvg_Click(object sender, EventArgs e)
        {
            int x = dataGridVColumn.CurrentRow.Index;
            string Name_Column = Convert.ToString(dataGridVColumn[0, x].Value);

            if (comboBox1.SelectedIndex == 0)//Таблица Users
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Users`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 1)//Таблица Prepod
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Prepod`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 2)//Таблица Auditoria
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Auditoria`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 4)//Таблица Zayavka
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Zayavka`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 5)//Таблица Param_print
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Param_print`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 6)//Таблица Param_monitor
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Param_monitor`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 7)//Таблица Param_mishek
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Param_mishek`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 8)//Таблица Param_klava
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Param_klava`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 9)//Таблица Param_sis_blokov
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Param_sis_blokov`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 10)//Таблица Param_nout
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Param_nout`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Prochee
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Prochee`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 12)//Таблица Sotrudnik
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Sotrudnik`";
                richTextBox1.Text = queryStringAVG;
            }
            else if (comboBox1.SelectedIndex == 3)//Таблица Program_Obespech
            {
                richTextBox1.Clear();
                string queryStringAVG = "SELECT AVG(`" + Name_Column + "`) FROM `Program_Obespech`";
                richTextBox1.Text = queryStringAVG;
            }
        }

        //Кнопка SUM, для вывода минимального значения
        private void buttonSum_Click(object sender, EventArgs e)
        {
            int x = dataGridVColumn.CurrentRow.Index;
            string Name_Column = Convert.ToString(dataGridVColumn[0, x].Value);

            if (comboBox1.SelectedIndex == 0)//Таблица Users
            {
                richTextBox1.Clear();
                string queryStringSUM = "SELECT SUM(`" + Name_Column + "`) FROM `Users`";
                richTextBox1.Text = queryStringSUM;
            }
            else if (comboBox1.SelectedIndex == 1)//Таблица Prepod
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT SUM(`" + Name_Column + "`) FROM `Prepod`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 2)//Таблица Auditoria
            {
                richTextBox1.Clear();
                string queryStringSUM = "SELECT SUM`" + Name_Column + "`) FROM `Auditoria`";
                richTextBox1.Text = queryStringSUM;
            }
            else if (comboBox1.SelectedIndex == 4)//Таблица Zayavka
            {
                richTextBox1.Clear();
                string queryStringSUM = "SELECT SUM(`" + Name_Column + "`) FROM `Zayavka`";
                richTextBox1.Text = queryStringSUM;
            }
            else if (comboBox1.SelectedIndex == 5)//Таблица Param_print
            {
                richTextBox1.Clear();
                string queryStringSUM = "SELECT SUM(`" + Name_Column + "`) FROM `Param_print`";
                richTextBox1.Text = queryStringSUM;
            }
            else if (comboBox1.SelectedIndex == 6)//Таблица Param_monitor
            {
                richTextBox1.Clear();
                string queryStringSUM = "SELECT SUM(`" + Name_Column + "`) FROM `Param_monitor`";
                richTextBox1.Text = queryStringSUM;
            }
            else if (comboBox1.SelectedIndex == 7)//Таблица Param_mishek
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT SUM(`" + Name_Column + "`) FROM `Param_mishek`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 8)//Таблица Param_klava
            {
                richTextBox1.Clear();
                string queryStringSUM = "SELECT SUM(`" + Name_Column + "`) FROM `Param_klava`";
                richTextBox1.Text = queryStringSUM;
            }
            else if (comboBox1.SelectedIndex == 9)//Таблица Param_sis_blokov
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT SUM(`" + Name_Column + "`) FROM `Param_sis_blokov`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 10)//Таблица Param_nout
            {
                richTextBox1.Clear();
                string queryStringSUM = "SELECT SUM(`" + Name_Column + "`) FROM `Param_nout`";
                richTextBox1.Text = queryStringSUM;
            }
            else if (comboBox1.SelectedIndex == 11)//Таблица Prochee
            {
                richTextBox1.Clear();
                string queryStringMax = "SELECT SUM(`" + Name_Column + "`) FROM `Prochee`";
                richTextBox1.Text = queryStringMax;
            }
            else if (comboBox1.SelectedIndex == 12)//Таблица Sotrudnik
            {
                richTextBox1.Clear();
                string queryStringSUM = "SELECT SUM(`" + Name_Column + "`) FROM `Sotrudnik`";
                richTextBox1.Text = queryStringSUM;
            }
            else if (comboBox1.SelectedIndex == 3)//Таблица Program_Obespech
            {
                richTextBox1.Clear();
                string queryStringSUM = "SELECT SUM(`" + Name_Column + "`) FROM `Program_Obespech`";
                richTextBox1.Text = queryStringSUM;
            }
        }

        //Печать результатов запроса
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVMySQL);
        }

        public string[] massSQLWord = { "update", "UPDATE", "Update", "delete", "DELETE", "Delete", "show", "SHOW", "Show", "use", "USE", "Use" };

        //Проверка на вредный SQL код
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {            
            string SQLtext = richTextBox1.Text;
            for (int i = 0; i < massSQLWord.Length; i++ )
            {
                if (SQLtext.Contains(massSQLWord[i].ToString()))
                {
                    SQLtext = SQLtext.Replace(massSQLWord[i].ToString(), "");
                    richTextBox1.Text = SQLtext;
                } 
            }         
        }
    }
}