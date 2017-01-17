using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ARM_System_Administrator.Classes
{
    public class ConnectorSQL
    {
        public MySqlConnection conn; //Строка подключения

        public bool CheckException;

        public DataTable dataTable;
        public MySqlDataAdapter adap;
        public DataSet ds;
        public MySqlCommandBuilder commBuild;

        //public BindingSource binSource;//= new BindingSource();//Для связки с таблицей

        public BindingSource binSourceAgregate = new BindingSource();
             
        public BindingSource binSourceZayavka = new BindingSource();

        //Процедура коннекта к базе
        public bool Connection(string Connection)
        {        
            try
            {
                conn = new MySqlConnection(Connection);
                conn.Open();                
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Процедура закрытия коннекта
        public bool CloseConnection()
        {
            conn.Close();
            return true;
        }

        //Вернет true, если есть записи
        public bool QueryToBool(string queryString)
        {
            dataTable = new DataTable();
            MySqlCommand com;
            MySqlDataReader dataReader;
            com = new MySqlCommand(queryString, conn);
            try
            {
                dataReader = com.ExecuteReader();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                CheckException = true;
                return false;
            }
            if (dataReader.HasRows)
            {
                dataTable.Load(dataReader);

                dataReader.Close();
                com.Dispose();
                return true;
            }
            dataReader.Close();
            com.Dispose();
            CheckException = false;
            return false;
        }

        //Процедура для агрегатных запросов
        public string AgregateQueryToDataGrid(string queryString)
        {
            string resultQuery = "";
            MySqlCommand com;
            MySqlDataReader dataReader;
            com = new MySqlCommand(queryString, conn);
            try
            {
                dataReader = com.ExecuteReader();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return resultQuery;
            }

            dataReader.Read();
            resultQuery = dataReader.GetString(0); //Invalid attempt to access a field before calling Read()

            dataReader.Close();
            com.Dispose();
            return resultQuery;
        }

        //Превращение ячейки таблицы в выпадающий список
        public void LoadDataGridComboBox(string queryString, DataGridViewComboBoxColumn column, string Name_Column, string Name_Table)
        {
            try
            {
                adap = new MySqlDataAdapter(queryString, conn);
                ds = new DataSet();
                adap.Fill(ds, Name_Table);
                column.DataSource = ds.Tables[0];
                column.DisplayMember = Name_Column;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Процедура вывода в комбо бокса столбца таблицы
        public bool QueryToComboBox(string queryString, ComboBox comboBox, string Name_Column)
        {
            dataTable = new DataTable();
            MySqlCommand com;
            MySqlDataReader dataReader;
            com = new MySqlCommand(queryString, conn);
            try
            {
                dataReader = com.ExecuteReader();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
            if (dataReader.HasRows)
            {
                dataTable.Load(dataReader);
                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = Name_Column;

                dataReader.Close();
                com.Dispose();
                return true;
            }
            dataReader.Close();
            com.Dispose();
            return false;
        }
        
        //Процедура подтверждения изменений в таблице
        public void SaveTable(string Name_Table, DataGridView dataGrid)
        {
            if (MessageBox.Show("Вы действительно хотите сохранить?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    commBuild = new MySqlCommandBuilder(adap);
                    //dataGrid.CurrentCell = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0];
                    //adap.Update(ds, Name_Table);
                    adap.Update(ds);

                    MessageBox.Show("Данные сохранены!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Процедура загрузки таблицы
        public void LoadTable(string Name_Table, string queryString, BindingSource binSource, DataGridView dataGrid, BindingNavigator Navigator)
        {
            try
            {
                adap = new MySqlDataAdapter(queryString, conn);
                ds = new DataSet();
                adap.Fill(ds, Name_Table);
                binSource.DataSource = ds.Tables[0];
                Navigator.BindingSource = binSource;
                dataGrid.DataSource = binSource;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Процедура загрузки таблицы
        public bool LoadTableToDataGridView(string queryString, BindingSource binSource, DataGridView dataGrid, BindingNavigator Navigator)
        {
            MySqlCommand myCommand;
            MySqlDataReader dataReader;
            myCommand = new MySqlCommand(queryString, conn);
            try
            {
                dataReader = myCommand.ExecuteReader();
            }
            catch (Exception)
            {
                return false;
            }
            dataTable = new DataTable();
            dataTable.Load(dataReader);
            dataGrid.AutoGenerateColumns = true;            
            dataGrid.Refresh();
            binSource.DataSource = dataTable;
            Navigator.BindingSource = binSource;
            dataGrid.DataSource = dataTable;
            dataReader.Close();
            myCommand.Dispose();

            return true;
        }
    }
}