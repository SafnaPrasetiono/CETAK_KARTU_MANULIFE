using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using CETAK_KARTU_MANULIFE.Moduls;

namespace CETAK_KARTU_MANULIFE
{
    public partial class Main : Form
    {
        // CALL METHOD CLASES FROM MODULS
        FormaterModul formated = new FormaterModul();
        ExcelModul mod = new ExcelModul();

        // MAKE METHOD PUBLIC
        string dir_input = "";
        string tglcycle = "";
        int jmldok = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Apakah Anda Ingin Keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //Form form = (Form)this.MdiParent;
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
                string[] excel = Directory.GetFiles(dir_input, "*.xls*");
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
            try
            {
                // DATA LOADED FROM MODUL
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                dt = mod.read_excel(TextData.Text);
                dt2 = mod.read_excel_secondary(TextData.Text);

                // Make methode directory for file csv and put the field first to csv file
                string pathcsv = Directory.GetCurrentDirectory() + @"\SOFTCOPY\" + tglcycle;
                createfolder(pathcsv);
                string pathfilecsv = pathcsv + @"\Softcopy-"+ tglcycle +".csv"; // make csv file
                string fhd = "NO;NAMA_PRUSAHAAN;NO_INDUK_KARYAWAN(NIK);NAMA_PESERTA;NO_PESERTA(PEMBERI_KERJA);NO_PESERTA(PESERTA);KODE_PELANGGAN(PEMBERI_KERJA);KODE_PELANGGAN(PESERTA);JUM_HAL";
                using (StreamWriter fs = new StreamWriter(pathfilecsv, false))
                {
                    fs.WriteLine(fhd);
                    fs.Close();
                }

                // MAKE METHOD TO FONT
                BaseFont AR   = BaseFont.CreateFont(Directory.GetCurrentDirectory() + @"\FONTS\arial.ttf", BaseFont.WINANSI, true);
                BaseFont ARB  = BaseFont.CreateFont(Directory.GetCurrentDirectory() + @"\FONTS\arialbd.ttf", BaseFont.WINANSI, true);
                BaseFont ARN  = BaseFont.CreateFont(Directory.GetCurrentDirectory() + @"\FONTS\ARIALN.TTF", BaseFont.WINANSI, true);
                BaseFont ARNB = BaseFont.CreateFont(Directory.GetCurrentDirectory() + @"\FONTS\ARIALNB.TTF", BaseFont.WINANSI, true);
                BaseFont TM   = BaseFont.CreateFont(Directory.GetCurrentDirectory() + @"\FONTS\times.ttf", BaseFont.WINANSI, true);
                
                string pathout = Directory.GetCurrentDirectory() + @"\OUTPUT\" + tglcycle;
                Paragraph header1 = new Paragraph();
                jmldok = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    backgroundWorker.ReportProgress(i + 1);

                    string kdcyc = TextData.Text;
                    string NamaPrusahaan = dt.Rows[i][1].ToString().Trim('"');
                    string NamaPeserta = dt.Rows[i][4].ToString().Trim('"');
                    string Nik = dt.Rows[i][5].ToString().Trim('"');
                    string Tanggal = dt.Rows[i][7].ToString().Trim('"');
                    string NoPesertaPeserta = "";
                    string KodePelangganPeserta = "";
                    string NoPesertaKerja = dt.Rows[i][2].ToString().Trim('"');
                    string KodePelangganKerja = dt.Rows[i][3].ToString().Trim('"');

                    for (int y=0; y < dt2.Rows.Count; y++)
                    {
                        if (Nik == dt2.Rows[y][5].ToString().Trim('"'))
                        {
                            NoPesertaPeserta = dt2.Rows[y][2].ToString().Trim('"');
                            KodePelangganPeserta = dt2.Rows[y][3].ToString().Trim('"');
                        }
                    }

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
                    pcb.AddTemplate(plogo, 0, 0); //scalling

                    pcb.BeginText();
                    int pos_y = 138;
                    int pos_x = 37;
                    int fontSize = 8;

                    pcb.SetFontAndSize(ARN, fontSize);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Nama Peserta", pos_x, pos_y, 0);
                    // MAKE NAME TO MULTILINE TEXT
                    List<string> texts = formated.multilineText(NamaPeserta, 30, 999);
                    for (int t = 0; t < texts.Count; t++)
                    {
                        pos_y = pos_y - 10;
                        pcb.SetFontAndSize(ARB, fontSize);
                        pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, texts[t], pos_x, pos_y, 0);
                    }
                    //pos_y = pos_y - 10;
                    //pcb.SetFontAndSize(ARNB, fontSize);
                    //pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, NamaPeserta, pos_x, pos_y, 0);

                    pos_y = pos_y - 14;
                    pcb.SetFontAndSize(TM, fontSize);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "NIK :", pos_x, pos_y, 0);
                    pos_y = pos_y - 10;
                    pcb.SetFontAndSize(ARB, fontSize);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Nik, pos_x, pos_y, 0);
                    pos_y = pos_y - 14;
                    pcb.SetFontAndSize(ARN, fontSize);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tanggal Kepesertaan", pos_x, pos_y, 0);
                    pos_y = pos_y - 10;
                    pcb.SetFontAndSize(ARB, fontSize);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Tanggal.ToUpper(), pos_x, pos_y, 0);
                    pos_y = pos_y - 14;
                    pcb.SetFontAndSize(TM, fontSize);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "No. Peserta ( Pemberi Kerja )", pos_x, pos_y, 0);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "No. Peserta ( Peserta )", pos_x + 118, pos_y, 0);
                    pos_y = pos_y - 10;
                    pcb.SetFontAndSize(ARB, fontSize);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, NoPesertaKerja, pos_x, pos_y, 0);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, NoPesertaPeserta, pos_x + 118, pos_y, 0);
                    pos_y = pos_y - 14;
                    pcb.SetFontAndSize(ARN, fontSize);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Kode Pelanggan ( Pemberi Kerja )", pos_x, pos_y, 0);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Kode Pelanggan ( Peserta )", pos_x + 118, pos_y, 0);
                    pos_y = pos_y - 10;
                    pcb.SetFontAndSize(ARB, fontSize);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, KodePelangganKerja, pos_x, pos_y, 0);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, KodePelangganPeserta, pos_x + 118, pos_y, 0);
                   
                    pcb.EndText();
                    doc.Close();

                    // PUT DATA TO SOFTCOPY
                    string inputDataCsv = (i + 1) + ";" + NamaPrusahaan + ";" + Nik + ";" + NamaPeserta + ";" + NoPesertaKerja + ";" + NoPesertaPeserta + ";" + KodePelangganKerja + ";" + KodePelangganPeserta + ";1";
                    using (System.IO.StreamWriter fs = new System.IO.StreamWriter(pathfilecsv, true))
                    {
                        fs.WriteLine(inputDataCsv);
                        fs.Close();
                    }

                }

                // SHOW MESSAGE FINISHED
                MessageBox.Show("Data Sudah Selesai Diproses", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "WARNING!!!");
            }
            finally
            {
                backgroundWorker.ReportProgress(0);
            }
        }

    }
}
