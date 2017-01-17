using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace ARM_System_Administrator.Classes
{
    public class Exports
    {
        public void ExportToExcel(DataGridView dGV)
        {
            if (MessageBox.Show("Вы действительно хотите вывести на печать?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;

                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);

                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

                for (int j = 0; j < dGV.ColumnCount; j++)
                {
                    ExcelApp.Cells[1, j + 1] = dGV.Columns[j].HeaderText;
                }

                for (int i = 1; i < dGV.Rows.Count + 1; i++)
                {
                    for (int j = 0; j < dGV.ColumnCount; j++)
                    {
                        ExcelApp.Cells[i + 1, j + 1] = dGV.Rows[i - 1].Cells[j].Value;
                    }
                }

                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
            }
            else
            {
                MessageBox.Show("Отмена печати!", "Отмена!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
