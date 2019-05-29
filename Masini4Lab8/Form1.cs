using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Masini4Lab8
{
    public partial class Form1 : Form
    {
        //public List<Proprietari> ProprietariMasina = new List<Proprietari>();
        public List<Masina> MasinaProprietari = new List<Masina>();
        public List<string> Files = new List<string>();


        public void ReadFromFile()
        {
            Files.Clear();
            Files.Add("MasinaProprietari.txt");
            
            foreach(var file in Files)
            {
                string[] lines = File.ReadAllLines(file);
                string[] attributes;
                foreach(string line in lines)
                {
                    attributes = line.Split('|');
                    Masina masinuta = new Masina(
                       attributes[0],
                       attributes[1],
                       attributes[2],
                       attributes[3],
                       attributes[4],
                       attributes[5],
                       attributes[6],
                       Convert.ToDouble(attributes[7]),
                       Convert.ToDouble(attributes[8]),
                       attributes[9],
                       Convert.ToDouble(attributes[10])
                        );
                    MasinaProprietari.Add(masinuta);
                    masinuta = null;

                }
            }
        }


        public Form1()
        {
            InitializeComponent();
            //ReadFromFile();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        
        Masina masina = new Masina();
        Proprietari proprietari = new Proprietari();

        static string[] tip = { "Hibride", "Electrice", "Euro 6", "Euro 5", "Euro 4", "Euro 3", "Euro 2", "Euro 1", "Non-Euro" };
        static string[] ani = { "Nou", "<1 an", "<3 ani", "<5 ani", "<10 ani", "Peste 10 ani" };
        double rezCO2, rezCmc, cotaReducere;

        //double emisie(double n) {
        //    if (n == 0 && (norma_p.SelectedItem == "Hibride") || (norma_p.SelectedItem == "Electrice") || (norma_p.SelectedItem == "Euro 6"))
        //    { rezCO2 = 0; }else
        //    if (n <= 120 && (norma_p.SelectedItem == "Euro 5") || (norma_p.SelectedItem == "Euro 4") || (norma_p.SelectedItem == "Euro 3"))
        //    {
        //        rezCO2 = 0;
        //    }else
        //    if (n >= 121 && n <= 210 && (norma_p.SelectedItem == "Euro 5") || (norma_p.SelectedItem == "Euro 4") || (norma_p.SelectedItem == "Euro 3"))
        //    {
        //        rezCO2 = 1;
        //    }else
        //    if (n >= 211 && n <= 270 && (norma_p.SelectedItem == "Euro 5") || (norma_p.SelectedItem == "Euro 4") || (norma_p.SelectedItem == "Euro 3"))
        //    {
        //        rezCO2 = 4;
        //    }else
        //    if (n >= 271 && (norma_p.SelectedItem == "Euro 5") || (norma_p.SelectedItem == "Euro 4") || (norma_p.SelectedItem == "Euro 3"))
        //    {
        //        rezCO2 = 8;
        //    }else
        //    if (n == 0 && (norma_p.SelectedItem == "Euro 2") || (norma_p.SelectedItem == "Euro 1") || (norma_p.SelectedItem == "Non-Euro"))
        //    { rezCO2 = 16; }
        //        return rezCO2; 
            
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            proprietari.Nume = nume.Text;
            proprietari.Prenume = prenume.Text;
            proprietari.nrTelefon =nrTel.Text;
            proprietari.CNP =cnp.Text;
            proprietari.Adresa = adresa.Text;
            proprietari.mMasina = marca.Text;
            masina.norma = norma_p.Text;
            masina.capacitate = Convert.ToDouble(cmc.Text);
            masina.emisie = Convert.ToDouble(emisieCO2.Text);
            masina.vechime = vechime.Text;
            masina.total = Convert.ToDouble(rez.Text);

            proprietari.WriteToFile();
            masina.WriteToFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReadFromFile();
           // lvDetalii.Items.Clear();
            foreach (var propri in MasinaProprietari)
            {
                lvDetalii.Items.Add( new ListViewItem(new[]
                   {
                    propri.Nume,
                    propri.Prenume,
                    propri.nrTelefon,
                    propri.CNP,
                    propri.mMasina,
                    propri.norma,
                    propri.emisie.ToString(),
                    propri.capacitate.ToString(),
                    propri.vechime,
                    propri.total.ToString()
                    })
                );
            }
        }

        double capacitate(double n)
        {

            if (n == 0 && (norma_p.SelectedItem == "Hibride") || (norma_p.SelectedItem == "Electrice") || (norma_p.SelectedItem == "Euro 6")) //trebuie pusa conditia ca masina sa fie hibrid, electrice sau euro 6
            { rezCmc = 0; }else
            if(n<=2000 && norma_p.SelectedItem=="Euro 5")
            {
                rezCmc = 1.3;
            }else
            if(n>=2001 && norma_p.SelectedItem == "Euro 5")
            {
                rezCmc = 0.39;
            }else
            if (n <= 2000 && norma_p.SelectedItem == "Euro 4")
            {
                rezCmc = 0.13;
            }else
            if (n >= 2001 && norma_p.SelectedItem == "Euro 4")
            {
                rezCmc = 3;
            }else
            if((norma_p.SelectedItem == "Euro 3") ||(norma_p.SelectedItem == "Euro 2") || (norma_p.SelectedItem == "Euro 1") || (norma_p.SelectedItem == "Non-Euro"))
            {
                if (n <= 2000)
                {
                    rezCmc = 9;
                }
                else { rezCmc = 16; }
            }

            return rezCmc; // il pot baga in if-uri
        }

        private void button_Click(object sender, EventArgs e)
        {
            norma_p.Items.AddRange(tip);
            vechime.Items.AddRange(ani);
            double eCO2, capCil, raspuns;
            try
            {
                eCO2 = Convert.ToDouble(emisieCO2.Text);
                capCil = Convert.ToDouble(cmc.Text);
                cotaReducere = 0;
                if (vechime.SelectedItem == "Nou")
                {
                    cotaReducere = 0;
                }else
                if (vechime.SelectedItem == "<1 an")
                {
                    cotaReducere = 10;
                }else
                if (vechime.SelectedItem == "<3 ani")
                {
                    cotaReducere = 30;
                }else
                if (vechime.SelectedItem == "<5 ani")
                {
                    cotaReducere = 40;
                }else
                if (vechime.SelectedItem == "<10 ani")
                {
                    cotaReducere = 60;
                }else
                if (vechime.SelectedItem == "Peste 10 ani")
                {
                    cotaReducere = 80;
                }
                raspuns = ((eCO2 * masina.emisieCO2(eCO2) * 30 / 100) + (capCil * capacitate(capCil) * 70 / 100) * (100 - cotaReducere) / 100) / 100;
                String text = " ";
                text += raspuns;
                rez.Text = text;
            }
            catch (Exception)
            {
                MessageBox.Show("Ati introdus o valoare invalida");
            }
        }
    }
}
