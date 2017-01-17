using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //Ссылка на MySQL компоненты
using Excel = Microsoft.Office.Interop.Excel; //Ссылка на Excel компоненты

namespace ARM_System_Administrator.Forms
{
    public partial class UchetOborudovania : Form
    {
        public BindingSource binSourceUchet = new BindingSource();
        public Classes.Exports ExportsTo = new Classes.Exports();
        public Classes.ConnectorSQL conMySQL;
        public UchetOborudovania(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        private void UchetOborudovania_Load(object sender, EventArgs e)
        {
            conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", comboBoxTypeAudit, "Nomer");

            comboBoxTypeOborud.SelectedIndex = 0;
            comboBoxStatus.SelectedIndex = 0;

            dataGridVUchet.ReadOnly = true;
            dataGridVUchet.AllowUserToAddRows = false;


            dataGridVUchet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridVUchet.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridVUchet.MultiSelect = false;
        }

        //Процедура настройки заголовкой хедеров у dataGridView
        public void HeadersNameDataGrid()
        {
            if (comboBoxTypeOborud.SelectedIndex == 0) //Клавиатуры
            {
                dataGridVUchet.Columns[0].Visible = false;
                dataGridVUchet.Columns[1].HeaderText = "Инвентарный номер";
                dataGridVUchet.Columns[2].HeaderText = "Тип подключения";
                dataGridVUchet.Columns[3].HeaderText = "Количество клавиш";
                dataGridVUchet.Columns[4].HeaderText = "Статус";
                dataGridVUchet.Columns[5].HeaderText = "Цена";
                dataGridVUchet.Columns[6].HeaderText = "Дополнительная информация";
                dataGridVUchet.Columns[7].HeaderText = "Дата ввода в эксплуатацию";
                dataGridVUchet.Columns[8].HeaderText = "Дата списания оборудования";
                dataGridVUchet.Columns[9].HeaderText = "Аудитория";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 1) //Компьютеры
            {
                dataGridVUchet.Columns[0].Visible = false;
                dataGridVUchet.Columns[1].HeaderText = "Инвентарный номер";
                dataGridVUchet.Columns[2].HeaderText = "Операционная система";
                dataGridVUchet.Columns[3].HeaderText = "Привод";
                dataGridVUchet.Columns[4].HeaderText = "Материнская плата";
                dataGridVUchet.Columns[5].HeaderText = "ПЗУ";
                dataGridVUchet.Columns[6].HeaderText = "Кулер";
                dataGridVUchet.Columns[7].HeaderText = "Видеокарта";
                dataGridVUchet.Columns[8].HeaderText = "Жесткий диск";
                dataGridVUchet.Columns[9].HeaderText = "Процессор";
                dataGridVUchet.Columns[10].HeaderText = "Блок питания";
                dataGridVUchet.Columns[11].HeaderText = "Статус";
                dataGridVUchet.Columns[12].HeaderText = "Цена";
                dataGridVUchet.Columns[13].HeaderText = "Дополнительная информация";
                dataGridVUchet.Columns[14].HeaderText = "Дата ввода в эксплуатацию";
                dataGridVUchet.Columns[15].HeaderText = "Дата списания оборудования";
                dataGridVUchet.Columns[16].HeaderText = "Аудитория";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 2) //Мониторы
            {
                dataGridVUchet.Columns[0].Visible = false;
                dataGridVUchet.Columns[1].HeaderText = "Инвентарный номер";
                dataGridVUchet.Columns[2].HeaderText = "Разрешение";
                dataGridVUchet.Columns[3].HeaderText = "Диагональ";
                dataGridVUchet.Columns[4].HeaderText = "Статус";
                dataGridVUchet.Columns[5].HeaderText = "Цена";
                dataGridVUchet.Columns[6].HeaderText = "Дополнительная информация";
                dataGridVUchet.Columns[7].HeaderText = "Дата ввода в эксплуатацию";
                dataGridVUchet.Columns[8].HeaderText = "Дата списания оборудования";
                dataGridVUchet.Columns[9].HeaderText = "Аудитория";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 3) //Мышки
            {
                dataGridVUchet.Columns[0].Visible = false;
                dataGridVUchet.Columns[1].HeaderText = "Инвентарный номер";
                dataGridVUchet.Columns[2].HeaderText = "Тип подключения";
                dataGridVUchet.Columns[3].HeaderText = "Статус";
                dataGridVUchet.Columns[4].HeaderText = "Цена";
                dataGridVUchet.Columns[5].HeaderText = "Дополнительная информация";
                dataGridVUchet.Columns[6].HeaderText = "Дата ввода в эксплуатацию";
                dataGridVUchet.Columns[7].HeaderText = "Дата списания оборудования";
                dataGridVUchet.Columns[8].HeaderText = "Аудитория";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 4) //Ноутбуки
            {
                dataGridVUchet.Columns[0].Visible = false;
                dataGridVUchet.Columns[1].HeaderText = "Инвентарный номер";
                dataGridVUchet.Columns[2].HeaderText = "Операционная система";
                dataGridVUchet.Columns[3].HeaderText = "Название";
                dataGridVUchet.Columns[4].HeaderText = "Процессор";
                dataGridVUchet.Columns[5].HeaderText = "Видео";
                dataGridVUchet.Columns[6].HeaderText = "Диагональ";
                dataGridVUchet.Columns[7].HeaderText = "Память";
                dataGridVUchet.Columns[8].HeaderText = "Жесткий диск";
                dataGridVUchet.Columns[9].HeaderText = "Статус";
                dataGridVUchet.Columns[10].HeaderText = "Цена";
                dataGridVUchet.Columns[11].HeaderText = "Дополнительная информация";
                dataGridVUchet.Columns[12].HeaderText = "Дата ввода в эксплуатацию";
                dataGridVUchet.Columns[13].HeaderText = "Дата списания оборудования";
                dataGridVUchet.Columns[14].HeaderText = "Аудитория";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 5) //Принтеры
            {
                dataGridVUchet.Columns[0].Visible = false;
                dataGridVUchet.Columns[1].HeaderText = "Инвентарный номер";
                dataGridVUchet.Columns[2].HeaderText = "Название";
                dataGridVUchet.Columns[3].HeaderText = "Максимальный размер";
                dataGridVUchet.Columns[4].HeaderText = "Размеры";
                dataGridVUchet.Columns[5].HeaderText = "Память";
                dataGridVUchet.Columns[6].HeaderText = "Скорость печати";
                dataGridVUchet.Columns[7].HeaderText = "Максимальный формат";
                dataGridVUchet.Columns[8].HeaderText = "Количество страниц";
                dataGridVUchet.Columns[9].HeaderText = "Статус";
                dataGridVUchet.Columns[10].HeaderText = "Цена";
                dataGridVUchet.Columns[11].HeaderText = "Дополнительная информация";
                dataGridVUchet.Columns[12].HeaderText = "Дата ввода в эксплуатацию";
                dataGridVUchet.Columns[13].HeaderText = "Дата списания оборудования";
                dataGridVUchet.Columns[14].HeaderText = "Аудитория";
            }
            else //Прочее
            {
                dataGridVUchet.Columns[0].Visible = false;
                dataGridVUchet.Columns[1].HeaderText = "Инвентарный номер";
                dataGridVUchet.Columns[2].HeaderText = "Название";
                dataGridVUchet.Columns[3].HeaderText = "Статус";
                dataGridVUchet.Columns[4].HeaderText = "Цена";
                dataGridVUchet.Columns[5].HeaderText = "Дополнительная информация";
                dataGridVUchet.Columns[6].HeaderText = "Дата ввода в эксплуатацию";
                dataGridVUchet.Columns[7].HeaderText = "Дата списания оборудования";
            }
        }

        //Печать результатов
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVUchet);
        }

        //Процедура установки названия таблицы
        public string TableName()
        {
            string typeOborud;

            if (comboBoxTypeOborud.SelectedIndex == 0)
            {
                typeOborud = "Param_klava";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 1)
            {
                typeOborud = "Param_sis_blokov";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 2)
            {
                typeOborud = "Param_monitor";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 3)
            {
                typeOborud = "Param_mishek";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 4)
            {
                typeOborud = "Param_nout";
            }
            else if (comboBoxTypeOborud.SelectedIndex == 5)
            {
                typeOborud = "Param_print";
            }
            else
            {
                typeOborud = "Prochee";
            }

            return typeOborud;
        }

        //Кнопка "Подсчет"
        private void buttonPodshet_Click(object sender, EventArgs e)
        {
            string typeOborud;
            string typeAudit = comboBoxTypeAudit.Text;
            string Status = comboBoxStatus.Text;

            typeOborud = TableName();

            string query = "SELECT COUNT(*) FROM `" + typeOborud + "` WHERE `Status` = '" +
                Status + "' AND `Auditoria` = '" + typeAudit + "'";

            string Result = conMySQL.AgregateQueryToDataGrid(query);
            int x = Convert.ToInt32(Result);

            if (x != 0)
            {                
                string messageBox = "В аудитории " + typeAudit + ", имеются '" + comboBoxTypeOborud.Text + "' в количестве: " + Result + " шт.";
                if (MessageBox.Show(messageBox + "\nХотите просмотреть список?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string queryString = "SELECT * FROM `" + typeOborud + "` WHERE `Auditoria` = '" + typeAudit + "' AND `Status` = '" + Status + "'";
                    conMySQL.LoadTable(typeOborud, queryString, binSourceUchet, dataGridVUchet, NavigatorUchet);
                    HeadersNameDataGrid();                    
                }
            }
            else
            {
                MessageBox.Show("Оборудование не найдено!", "Сообщение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Кнопка "Все оборудование по типу"
        private void buttonAllOborud_Click(object sender, EventArgs e)
        {
            string typeOborud;
            string Status = comboBoxStatus.Text;

            typeOborud = TableName();

            string query = "SELECT COUNT(*) FROM `" + typeOborud + "` WHERE `Status` = '" +
                Status + "'";

            if (conMySQL.QueryToBool(query) == true)
            {
                string Result = conMySQL.AgregateQueryToDataGrid(query);
                string messageBox = "Имеется оборудования типа : '" + comboBoxTypeOborud.Text + "' в количестве: " + Result + " шт.";
                if (MessageBox.Show(messageBox + "\nХотите просмотреть список?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string queryString = "SELECT * FROM `" + typeOborud + "` WHERE `Status` = '" + Status + "'";
                    conMySQL.LoadTable(typeOborud, queryString, binSourceUchet, dataGridVUchet, NavigatorUchet);
                    HeadersNameDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Оборудование не найдено!", "Сообщение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}