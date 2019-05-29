using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masini4Lab8
{
    public class Proprietari
    {
        public String Nume { get; set; }
        public String Prenume { get; set; }
        public String Adresa { get; set; }
        public String nrTelefon { get; set; }
        public String mMasina { get; set; }
        public String CNP { get; set; }
        public Proprietari(String nume, String prenume, String adresa, String nrTelefon, String mMasina, String CNP)
        {
            this.Nume = nume;
            this.Prenume = prenume;
            this.Adresa = adresa;
            this.nrTelefon = nrTelefon;
            this.mMasina = mMasina;
            this.CNP = CNP;
        }
        public Proprietari() { }
        public void WriteToFile()
        {
            string line = this.Nume + '|' +
                          this.Prenume + '|' +
                          this.CNP + '|' +
                          this.Adresa + '|' +
                          this.nrTelefon + '|' +
                          this.mMasina + '|';
            System.IO.File.AppendAllText("MasinaProprietari.txt", line);
        }
    }
}
