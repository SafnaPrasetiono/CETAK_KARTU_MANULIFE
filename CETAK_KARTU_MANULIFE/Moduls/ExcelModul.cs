using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace CETAK_KARTU_MANULIFE.Moduls
{
    class ExcelModul
    {
        public DataTable ReadExcel(string filename, string sheetname)
        {
            OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={ filename };Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1'");
            OleDbCommand oleDbCmd = new OleDbCommand($"select * from [{ sheetname }$]", connection);
            DataTable data = new DataTable();
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(oleDbCmd);
            da.Fill(data);
            connection.Close();
            return data;
        }
    }
}
