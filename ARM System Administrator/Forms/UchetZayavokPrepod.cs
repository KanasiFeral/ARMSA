using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ARM_System_Administrator.Forms
{
    public partial class UchetZayavokPrepod : Form
    {
        public Classes.ConnectorSQL conMySQL;
        public UchetZayavokPrepod(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        public BindingSource binSourcePrep = new BindingSource();
        public BindingSource binSourceZayavka = new BindingSource();
        
        //Кнопка "Проверить", для проверки статуса заявки
        private void buttonChangeStatus_Click(object sender, EventArgs e)
        {
            string FIO = textFIO.Text;

            string queryStringCheck = "SELECT * FROM `Zayavka` WHERE `FIO_Prepod` = '" + textFIO.Text + "'";
            
            if (conMySQL.QueryToBool(queryStringCheck) == false)
            {
                //Работа с лейблами
                labelStatus.Visible = true;
                labelFIO.Text = textFIO.Text;
                labelSorry.Visible = true;
                labelFIO.Visible = true;

                string queryString = "SELECT * FROM `Zayavka` WHERE `FIO_Prepod` = '" + textFIO.Text + "'";
                conMySQL.LoadTable("Zayavka", queryString, binSourceZayavka, dataGridVUchetPrepod, NavigatorUchetPrepod);

                dataGridVUchetPrepod.Columns[0].Visible = false;
                dataGridVUchetPrepod.Columns[1].Visible = false;
                dataGridVUchetPrepod.Columns[2].Visible = false;
                dataGridVUchetPrepod.Columns[3].Visible = false;

                dataGridVUchetPrepod.Columns[4].HeaderText = "Номер аудитория";
                dataGridVUchetPrepod.Columns[5].HeaderText = "Инвентарный номер";
                dataGridVUchetPrepod.Columns[6].HeaderText = "Цель заявки";
                dataGridVUchetPrepod.Columns[7].HeaderText = "Дата заявки";
                dataGridVUchetPrepod.Columns[8].HeaderText = "Дата возврата";
                dataGridVUchetPrepod.Columns[9].HeaderText = "Статус заявки";

                dataGridVUchetPrepod.Enabled = false;
            }
            else
            {
                string queryString = "SELECT * FROM `Zayavka` WHERE `FIO_Prepod` = '" + textFIO.Text + "'";
                conMySQL.LoadTable("Zayavka", queryString, binSourceZayavka, dataGridVUchetPrepod, NavigatorUchetPrepod);

                dataGridVUchetPrepod.Columns[0].Visible = false;
                dataGridVUchetPrepod.Columns[1].Visible = false;
                dataGridVUchetPrepod.Columns[2].Visible = false;
                dataGridVUchetPrepod.Columns[3].Visible = false;

                dataGridVUchetPrepod.Columns[4].HeaderText = "Номер аудитория";
                dataGridVUchetPrepod.Columns[5].HeaderText = "Инвентарный номер";
                dataGridVUchetPrepod.Columns[6].HeaderText = "Цель заявки";
                dataGridVUchetPrepod.Columns[7].HeaderText = "Дата заявки";
                dataGridVUchetPrepod.Columns[8].HeaderText = "Дата возврата";
                dataGridVUchetPrepod.Columns[9].HeaderText = "Статус заявки";

                dataGridVUchetPrepod.Enabled = true;

                labelStatus.Visible = false;
                labelFIO.Text = "";
                labelSorry.Visible = false;
                labelFIO.Visible = false;
            }
        }

        //Загрузка формы
        private void UchetZayavokPrepod_Load(object sender, EventArgs e)
        {
            dataGridVUchetPrepod.ReadOnly = true;
            dataGridVUchetPrepod.AllowUserToAddRows = false;
            dataGridVUchetPrepod.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridVUchetPrepod.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridVUchetPrepod.MultiSelect = false;

            conMySQL.QueryToComboBox("SELECT CONCAT_WS(' ', `Fam_prep`, `Name_prep`, `Otch_prep`) FIO FROM `Prepod`", textFIO, "FIO");

        }

        //Обработка закрытия формы, а именно после ее закрытия
        private void UchetZayavokPrepod_FormClosing(object sender, FormClosingEventArgs e)
        {
            MenuPrepod menuPrep = new MenuPrepod(conMySQL);
            menuPrep.Show();
        }        
    }
}
