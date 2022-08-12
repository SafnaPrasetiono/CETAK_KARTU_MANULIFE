using System;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace CETAK_KARTU_MANULIFE.Moduls
{
    class ExcelModul
    {
        string strExcel = "";
        public DataTable ReadExcel(string filename, string sheetname)
        {
            FileInfo fi = new FileInfo(filename);
            // Get file extension   
            string extn = fi.Extension;
            //Console.WriteLine("File Extension: {0}", extn);
            if (extn.Equals(".xlsx"))
            {
                 strExcel = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={ filename };Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1'";
            } else
            {
                 strExcel = $"Provider=Microsoft.JET.OLEDB.4.0; data source={filename}; Extended Properties=Excel 8.0;";
            }
            OleDbConnection connection = new OleDbConnection(strExcel);
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
