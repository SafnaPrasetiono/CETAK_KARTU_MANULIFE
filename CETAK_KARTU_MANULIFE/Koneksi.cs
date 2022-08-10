using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CETAK_KARTU_MANULIFE
{

    class koneksi
    {
        SqlConnection con;
        public SqlCommand cmd;
        SqlDataAdapter adapter;
        //static SqlTransaction tran;
        //string vNoman = "";
        static string constr = @"Data Source=DESKTOP-1C0D04E; Initial Catalog=DB_BNILIFE; Integrated Security=true"; //User ID=Ivan.Witono;Password=Ivan1234!";
                                                                                                                     //static string constr = "Data Source=192.168.10.13; Initial Catalog=HANWA_LIFE; User ID=Ivan.Witono; Password=Ivan1234!";

        //static string constr = @"Data Source=ITDEV9\SQLEXPRESS;Initial Catalog=AIATest;persist security info=True;Integrated Security=SSPI;";
        // providerName="System.Data.SqlClient";
        DataTable dt;

        public void connection()
        {
            con = new SqlConnection(constr);

            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
        public void VerifyDir(string pathdir)
        {
            if (!Directory.Exists(pathdir))
            {
                Directory.CreateDirectory(pathdir);
            }
        }

        public DataTable openTable(string query)
        {
            //select
            con = new SqlConnection(constr);
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }

            con.Open();

            dt = new DataTable();
            adapter = new SqlDataAdapter(query, constr);
            adapter.Fill(dt);

            con.Close();
            return dt;
        }

        public void executeQuery(string query)
        {
            con = new SqlConnection(constr);
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }

            con.Open();

            cmd = con.CreateCommand();
            cmd.CommandText = query;
            //reader = cmd.ExecuteReader();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}