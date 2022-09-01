using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CETAK_KARTU_MANULIFE.Moduls
{
    class ExcelModul
    {
        public DataTable ReadExcel(string filename, string sheetname)
        {
            string strExcel = "";
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


        public DataTable read_excel(string fullname)
        {
            DataTable SheetData = new DataTable();
            List<string> listSheet = new List<string>();
            string strExcel;
            FileInfo fi = new FileInfo(fullname);
            string extn = fi.Extension;
            if (extn.Equals(".xlsx"))
            {
                strExcel = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={ fullname }; Extended Properties=Excel 12.0;";
            }
            else
            {
                strExcel = $"Provider=Microsoft.JET.OLEDB.4.0; data source={fullname}; Extended Properties=Excel 8.0;";
            }
            OleDbConnection connection = new OleDbConnection(strExcel);
            connection.Open();
            DataTable dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            foreach (DataRow drSheet in dtSheet.Rows)
            {
                //checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                {
                    listSheet.Add(drSheet["TABLE_NAME"].ToString().Replace("'", ""));
                }
                else
                {
                    listSheet.Add(drSheet["TABLE_NAME"].ToString().Replace("'", "") + "$");
                }
            }
            try
            {
                OleDbCommand oleDbCmd = new OleDbCommand($"select * from [{ listSheet[1] }]", connection);
                var adap = new OleDbDataAdapter(oleDbCmd);
                adap.Fill(SheetData);
            }
            catch (Exception msg)
            {
                MessageBox.Show("Error Baca sheet Excel :" + msg.ToString());
            }
            connection.Close();
            return SheetData;
        }

        public DataTable read_excel_secondary(string fullname)
        {
            DataTable SheetData = new DataTable();
            List<string> listSheet = new List<string>();
            string strExcel;
            FileInfo fi = new FileInfo(fullname);
            string extn = fi.Extension;
            if (extn.Equals(".xlsx"))
            {
                strExcel = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={ fullname }; Extended Properties=Excel 12.0;";
            }
            else
            {
                strExcel = $"Provider=Microsoft.JET.OLEDB.4.0; data source={fullname}; Extended Properties=Excel 8.0;";
            }
            OleDbConnection connection = new OleDbConnection(strExcel);
            connection.Open();
            DataTable dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            foreach (DataRow drSheet in dtSheet.Rows)
            {
                //checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                {
                    listSheet.Add(drSheet["TABLE_NAME"].ToString().Replace("'", ""));
                }
                else
                {
                    listSheet.Add(drSheet["TABLE_NAME"].ToString().Replace("'", "") + "$");
                }
            }
            try
            {
                OleDbCommand oleDbCmd = new OleDbCommand($"select * from [{ listSheet[0] }]", connection);
                var adap = new OleDbDataAdapter(oleDbCmd);
                adap.Fill(SheetData);
            }
            catch (Exception msg)
            {
                MessageBox.Show("Error Baca sheet Excel :" + msg.ToString());
            }
            connection.Close();
            return SheetData;
        }
    }
}
