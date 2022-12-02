using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mikro.Entities;
using System.IO;

namespace mikro
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        Random rnd = new Random(1234);
        public Form1()
        {
            InitializeComponent();
            Population = pop(@"C:\Temp\nép.csv");
            BirthProbabilities = birth(@"C:\Temp\születés.csv");
            DeathProbabilities = death(@"C:\Temp\halál.csv");
            
        }
        void Simulation()
        {
            for (int i = 2005; i < 2024; i++)
            {
                for (int j = 0; j < Population.Count; j++)
                {
                    SimStep(i, Population[j]);
                }
                int ferfi = (from x in Population where x.Gender == Gender.Male && x.IsAlive == true select x).ToList().Count;
                int no = (from x in Population where x.Gender == Gender.Female && x.IsAlive == true select x).ToList().Count;
            }
        }
        void SimStep(int year,Person p)
        {
            if (p.IsAlive == false)
            {
                return;
            }
            int age = year - p.BirthYear;
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == p.Gender && x.Age == age
                             select x.Probability).FirstOrDefault();
            if (rnd.NextDouble() <= pDeath)
                p.IsAlive = false;

            if (p.IsAlive && p.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.Probability).FirstOrDefault();
                //Születik gyermek?
                if (rnd.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rnd.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }
        List<Person> pop(string file)
        {
            List<Person> population = new List<Person>();
            StreamReader olvas = new StreamReader(file);
            while (!olvas.EndOfStream)
            {
                string line = olvas.ReadLine();
                string[] szavak = line.Split(';');
                population.Add(new Person()
                {
                    BirthYear = int.Parse(szavak[0]),
                    Gender = (Gender)Enum.Parse(typeof(Gender), szavak[1]),
                    NbrOfChildren = int.Parse(szavak[2])
                });
                
            }
            return population;
        }
        List<BirthProbability> birth(string file)
        {
            List<BirthProbability> szul = new List<BirthProbability>();
            StreamReader olvas = new StreamReader(file);
            while (!olvas.EndOfStream)
            {
                string line = olvas.ReadLine();
                string[] szavak = line.Split(';');
                szul.Add(new BirthProbability()
                {
                    Age = int.Parse(szavak[0]),
                    Children = int.Parse(szavak[1]),
                    Probability = double.Parse(szavak[2])
                });

            }
            return szul;
        }
        List<DeathProbability> death(string file)
        {
            List<DeathProbability> hal = new List<DeathProbability>();
            StreamReader olvas = new StreamReader(file);
            while (!olvas.EndOfStream)
            {
                string line = olvas.ReadLine();
                string[] szavak = line.Split(';');
                hal.Add(new DeathProbability()
                {
                    Gender = (Gender)Enum.Parse(typeof(Gender), szavak[0]),
                    Age = int.Parse(szavak[1]),
                    Probability = double.Parse(szavak[2])
                });

            }
            return hal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Simulation();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }
    }
}
