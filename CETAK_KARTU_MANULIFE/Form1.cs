using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using CETAK_KARTU_MANULIFE.Moduls;

namespace CETAK_KARTU_MANULIFE
{
    public partial class Form1 : Form
    {
        string dir_input = "";
        string tglcycle = "";
        int jmldok = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Apakah Anda Ingin Keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Form form = (Form)this.MdiParent;
                this.Close();
            }
        }

        private void BtnProses_Click(object sender, EventArgs e)
        {
            if (TextData.Text == "")
            {
                MessageBox.Show("Data belum dipilih!", "Warning");
            }
            else
            {
                if (!this.backgroundWorker.IsBusy)
                {
                    this.backgroundWorker.RunWorkerAsync();
                }
            }
        }

        void createfolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
       
        private void dateTimePicker_CloseUp(object sender, EventArgs e)
        {
            TextData.Text = dateTimePicker.Value.ToString("yyyyMMdd");
            string tcycle = dateTimePicker.Value.ToString("yyyyMMdd");
            TextCycle.Text = tcycle;
            tglcycle = tcycle;
            dir_input = Directory.GetCurrentDirectory() + @"\DATA\" + tcycle + @"\";
            if (!Directory.Exists(dir_input))
            {
                MessageBox.Show("Tanggal Yang Anda Pilih Salah", "Info", MessageBoxButtons.OK);
                TextData.Text = "";
                return;
            }
            else
            {
                string[] excel = Directory.GetFiles(dir_input, "*.xlsx*");
                TextData.Text = excel[0];
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                LblPersen.Text = "0%";
                int persen = (e.ProgressPercentage * 100) / jmldok;
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Maximum = Convert.ToInt32(jmldok);
                progressBar.Value = e.ProgressPercentage;
                LblPersen.Text = Convert.ToString(persen) + "%";
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Memanggil method moduld
            ExcelModul mod = new ExcelModul();
            try
            {
                string pathout = Directory.GetCurrentDirectory() + @"\OUTPUT\" + tglcycle;
                DataTable dt = new DataTable();
                dt = mod.ReadExcel(TextData.Text, tglcycle + " Data Peserta Aktif ER");

                // Make methode directory for file csv and put the field first to csv file
                string pathcsv = Directory.GetCurrentDirectory() + @"\SOFTCOPY\" + tglcycle;
                createfolder(pathcsv);
                string pathfilecsv = pathcsv + @"\DATA.CSV"; // make csv file
                string fhd = "NO;NAMA_PRUSAHAAN;NO_INDUK_KARYAWAN(NIK);NAMA_PESERTA;NO_PESERTA(PEMBERI_KERJA);NO_PESERTA(PESERTA);KODE_PELANGGAN(PEMBERI_KERJA);KODE_PELANGGAN(PESERTA);JUM_HAL";
                using (System.IO.StreamWriter fs = new System.IO.StreamWriter(pathfilecsv, false))
                {
                    fs.WriteLine(fhd);
                    fs.Close();
                }

                //BaseFont bf = BaseFont.CreateFont(Directory.GetCurrentDirectory() + @"\FONTS\arialbd.ttf", BaseFont.WINANSI, true);
                BaseFont bfbold = BaseFont.CreateFont(Directory.GetCurrentDirectory() + @"\FONTS\arialbd.ttf", BaseFont.WINANSI, true);
                //BaseFont bfbarcode = BaseFont.CreateFont(Directory.GetCurrentDirectory() + @"\FONTS\C39P36DlTt.TTF", BaseFont.WINANSI, true);
                Paragraph header1 = new Paragraph();

                jmldok = dt.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    backgroundWorker.ReportProgress(i + 1);
                    string kdcyc = TextData.Text;
                    //int j = ((i - 1) % dt.Rows.Count) + 1;
                    string NamaPrusahaan = dt.Rows[i][0].ToString().Trim('"');
                    string NamaPeserta = dt.Rows[i][5].ToString().Trim('"');
                    string Nik = dt.Rows[i][6].ToString().Trim('"');
                    string Tanggal = dt.Rows[i][7].ToString().Trim('"');
                    string NoPesertaKerja = dt.Rows[i][1].ToString().Trim('"');
                    string NoPesertaPeserta = dt.Rows[i][2].ToString().Trim('"');
                    string KodePelangganKerja = dt.Rows[i][3].ToString().Trim('"');
                    string KodePelangganPeserta = dt.Rows[i][4].ToString().Trim('"');

                    // Memasukan data ke softcopy
                    string inputDataCsv = (i + 1) + ";" + NamaPrusahaan + ";" + Nik + ";" + NamaPeserta + ";" + NoPesertaKerja + ";" + NoPesertaPeserta + ";" + KodePelangganKerja + ";" + KodePelangganPeserta + ";1";
                    using (System.IO.StreamWriter fs = new System.IO.StreamWriter(pathfilecsv, true))
                    {
                        fs.WriteLine(inputDataCsv);
                        fs.Close();
                    }

                    string[] dateArray = Tanggal.Split('/');
                    string month = "Januari";
                    if (dateArray[0] == "2")
                    {
                        month = "Februari";
                    }
                    else if (dateArray[0] == "3")
                    {
                        month = "Maret";
                    }
                    else if (dateArray[0] == "4")
                    {
                        month = "April";
                    }
                    else if (dateArray[0] == "5")
                    {
                        month = "Mei";
                    }
                    else if (dateArray[0] == "6")
                    {
                        month = "Juni";
                    }
                    else if (dateArray[0] == "7")
                    {
                        month = "Juli";
                    }
                    else if (dateArray[0] == "8")
                    {
                        month = "Agustus";
                    }
                    else if (dateArray[0] == "9")
                    {
                        month = "September";
                    }
                    else if (dateArray[0] == "10")
                    {
                        month = "Oktober";
                    }
                    else if (dateArray[0] == "11")
                    {
                        month = "November";
                    }
                    else if (dateArray[0] == "12")
                    {
                        month = "Desember";
                    }
                    string Date = dateArray[1] + " " + month.ToUpper() + " " + dateArray[2].Substring(0, 4);


                    //Make to file name
                    createfolder(pathout);
                    string pathcetak = pathout + "\\" + Nik + "_" + NamaPeserta + ".pdf";

                    // Template PDF
                    PdfReader rlogo = new PdfReader("TEMPLATE/kartu.pdf");

                    //var doc = new Document(new iTextSharp.text.Rectangle(640, 210));    //ini untuk custom ukuran lembaran output nya
                    var doc = new Document(rlogo.GetPageSize(1));                         //ini untuk custom ukuran sama seperti ukuran templatenya
                    var writeKartu = File.OpenWrite(pathcetak);
                    var instanceKartu = PdfWriter.GetInstance(doc, writeKartu);

                    doc.Open();
                    PdfContentByte pcb = instanceKartu.DirectContent;

                    doc.NewPage();
                    PdfImportedPage plogo = instanceKartu.GetImportedPage(rlogo, 1);
                    pcb.AddTemplate(plogo, 0, 0);//scalling

                    pcb.BeginText();
                    int pos_y = 122;
                    int pos_x = 37;
                    //int pos_x2 = 270;
                    int fontSize = 8;
                    pcb.SetFontAndSize(bfbold, fontSize);

                    if (NamaPeserta != "")
                    {
                        pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, NamaPeserta, pos_x, pos_y, 0);
                    }
                    if (Nik != "")
                    {
                        pos_y = pos_y - 24;
                        pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Nik, pos_x, pos_y, 0);
                    }
                    if (Tanggal != "")
                    {
                        pos_y = pos_y - 23;
                        pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Date, pos_x, pos_y, 0);
                    }
                    if (NoPesertaKerja != "")
                    {
                        pos_y = pos_y - 25;
                        pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, NoPesertaKerja, pos_x, pos_y, 0);
                        pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, NoPesertaPeserta, pos_x + 118, pos_y, 0);
                    }
                    if (KodePelangganKerja != "")
                    {
                        pos_y = pos_y - 22;
                        pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, KodePelangganKerja, pos_x, pos_y, 0);
                        pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, KodePelangganPeserta, pos_x + 118, pos_y, 0);
                    }
                    pcb.EndText();
                    doc.Close();


                }

                //sampai disini akhir koding
                MessageBox.Show("Data Sudah Selesai Diproses", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "WARNING!!!");
            } finally
            {
                backgroundWorker.ReportProgress(0);
            }
        }

    }
}
