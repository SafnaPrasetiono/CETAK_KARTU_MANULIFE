using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CETAK_KARTU_MANULIFE.Moduls
{
    class FormaterModul
    {
        // DATE FORMATER
        public string dateFormaters(string Tanggal)
        {
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
            string Date = dateArray[1] + " " + month + " " + dateArray[2].Substring(0, 4);
            return Date;
        }

        // MULTI LINE TEXT
        public List<string> multilineText(string testLine, int lineWidth, int maxLine)
        {
            List<string> lines = new List<string>();
            lines.Add(testLine);
            int cutoff = 0;
            int numberOfLines = 1;
            while (testLine.Length >= lineWidth && numberOfLines < maxLine)
            {
                cutoff = testLine.LastIndexOf(" ", lineWidth);
                if (cutoff >= 1)
                {
                    lines[numberOfLines - 1] = testLine.Substring(0, cutoff);
                    lines.Add(testLine.Substring(cutoff + 1));
                    testLine = lines[numberOfLines];
                }
                else
                {
                    lines[numberOfLines - 1] = testLine.Substring(0, lineWidth);
                    lines.Add(testLine.Substring(lineWidth));
                    testLine = lines[numberOfLines];
                }
                numberOfLines += 1;
                cutoff = 0;
            }
            return lines;
        }

    }
}
