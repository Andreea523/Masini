using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masini4Lab8
{
     public class Masina:Proprietari
    {
        public String norma { get; set; }
        public double emisie { get; set; }
        public double capacitate { get; set; }
        public String vechime { get; set; }
        public double total { get; set; }
        public Masina(String nume, String prenume, String adresa, String nrTelefon, String mMasina, String CNP, String norma, double emisie, double capacitate, String vechimie, double total):base( nume,  prenume,  adresa,  nrTelefon,  mMasina, CNP)
        {
            this.norma = norma;
            this.emisie = emisie;
            this.capacitate = capacitate;
            this.vechime = vechime;
            this.total = total;
        }
        public Masina() { }

        static string[] tip = { "Hibride", "Electrice", "Euro 6", "Euro 5", "Euro 4", "Euro 3", "Euro 2", "Euro 1", "Non-Euro" };
        static string[] ani = { "Nou", "<1 an", "<3 ani", "<5 ani", "<10 ani", "Peste 10 ani" };
        double rezCO2, rezCmc;

        public double emisieCO2(double n)
        {
            if (n == 0 && (norma == "Hibride") || (norma == "Electrice") || (norma == "Euro 6"))
            { rezCO2 = 0; }
            else
            if (n <= 120 && (norma == "Euro 5") || (norma == "Euro 4") || (norma == "Euro 3"))
            {
                rezCO2 = 0;
            }
            else
            if (n >= 121 && n <= 210 && (norma == "Euro 5") || (norma == "Euro 4") || (norma == "Euro 3"))
            {
                rezCO2 = 1;
            }
            else
            if (n >= 211 && n <= 270 && (norma == "Euro 5") || (norma == "Euro 4") || (norma == "Euro 3"))
            {
                rezCO2 = 4;
            }
            else
            if (n >= 271 && (norma == "Euro 5") || (norma == "Euro 4") || (norma == "Euro 3"))
            {
                rezCO2 = 8;
            }
            else
            if (n == 0 && (norma == "Euro 2") || (norma == "Euro 1") || (norma == "Non-Euro"))
            { rezCO2 = 16; }
            return rezCO2;
        }
        public double capacitateCilindrica(double n)
        {

            if (n == 0 && (norma == "Hibride") || (norma == "Electrice") || (norma == "Euro 6")) //trebuie pusa conditia ca masina sa fie hibrid, electrice sau euro 6
            { rezCmc = 0; }
            else
            if (n <= 2000 && norma == "Euro 5")
            {
                rezCmc = 1.3;
            }
            else
            if (n >= 2001 && norma == "Euro 5")
            {
                rezCmc = 0.39;
            }
            else
            if (n <= 2000 && norma == "Euro 4")
            {
                rezCmc = 0.13;
            }
            else
            if (n >= 2001 && norma == "Euro 4")
            {
                rezCmc = 3;
            }
            else
            if ((norma == "Euro 3") || (norma == "Euro 2") || (norma == "Euro 1") || (norma == "Non-Euro"))
            {
                if (n <= 2000)
                {
                    rezCmc = 9;
                }
                else { rezCmc = 16; }
            }

            return rezCmc; // il pot baga in if-uri
        }
        public double vechimeMasina(double cotaReducere)
        {
            if (vechime == "Nou")
            {
                cotaReducere = 0;
            }
            else
                  if (vechime == "<1 an")
            {
                cotaReducere = 10;
            }
            else
                  if (vechime == "<3 ani")
            {
                cotaReducere = 30;
            }
            else
                  if (vechime == "<5 ani")
            {
                cotaReducere = 40;
            }
            else
                  if (vechime == "<10 ani")
            {
                cotaReducere = 60;
            }
            else
                  if (vechime == "Peste 10 ani")
            {
                cotaReducere = 80;
            }
            return cotaReducere;

        }

        public void WriteToFile()
        {
            string line = this.norma + '|' +
                          this.emisie + '|' +
                          this.capacitate + '|' +
                          this.vechime + '|' +
                          this.total + "\n";
            System.IO.File.AppendAllText("MasinaProprietari.txt", line);
        }
    }
}

