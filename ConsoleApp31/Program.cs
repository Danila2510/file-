using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp31
{
    internal class Program
    {
        public class Pisatel
        {
            public string Zagolovok { get; set; }
            public string Avtor { get; set; }
            public int God { get; set; }
            public string Tekst { get; set; }
            public string Tema { get; set; }
            public Pisatel() { }
            public Pisatel(string zagolovok, string avtor, int god, string tekst, string tema)
            {
                Zagolovok = zagolovok;
                Avtor = avtor;
                God = god;
                Tekst = tekst;
                Tema = tema;
            }
            public override string ToString() { return $"Title of the verse{Zagolovok}\nName of the author{Avtor}\nYear of writing{God}\nVerse text:{Tekst}\nThe subject of the verse{Tema}"; }
        }
        class Spisok_Pisatelei
        {
            List<Pisatel> pisatel;
            public Spisok_Pisatelei() => pisatel = new List<Pisatel>();
            public void Add(Pisatel Poema)
            {
                pisatel.Add(Poema);
            }
            
            public void Remove(Pisatel Poema)
            {
                pisatel.Remove(Poema);
            }
            public void ChangeTitel(Pisatel Poema, string titel)
            {
                Poema.Zagolovok = titel;
            }
            public void ChangeAuthor(Pisatel Poema, string author)
            {
                Poema.Avtor = author;
            }
            public void ChangeYearWriting(Pisatel Poema, int Vipusk)
            {
                Poema.God = Vipusk;
            }
            public void ChangeTextPoem(Pisatel Poema, string Soderjanie)
            {
                Poema.Tekst = Soderjanie;
            }
            public void ChangeThemePoem(Pisatel Poema, string tema_poema)
            {
                Poema.Tema = tema_poema;
            }
            public List<Pisatel> FindTitel(string Poema_nazvanie)
            {
                return pisatel.Where(p => p.Zagolovok == Poema_nazvanie).ToList();
            }
            public List<Pisatel> FindAuthor(string author)
            {
                return pisatel.Where(p => p.Avtor == author).ToList();
            }
            public List<Pisatel> FindByTextPoem(string Soderjanie)
            {
                return pisatel.Where(p => p.Tekst == Soderjanie).ToList();
            }
            public List<Pisatel> FindByThemePoem(string tema_poema)
            {
                return pisatel.Where(p => p.Tema == tema_poema).ToList();
            }
            public List<Pisatel> FindByYearWriting(int Vipusk)
            {
                return pisatel.Where(p => p.God == Vipusk).ToList();
            }
                public void Save(string imya_file)
            {
                using (StreamWriter sw = new StreamWriter(imya_file))
                {
                    foreach (Pisatel i in pisatel)
                        sw.WriteLine($"{i.ToString()}\n");
                }
            }
            public void File(string imya_file)
            {
                List<Pisatel> writers = new List<Pisatel>();
                using (StreamReader sr = new StreamReader(imya_file))
                {
                    string Zagolovok = sr.ReadLine();
                    string stroka;
                    while ((stroka = sr.ReadLine()) != null)
                    {
                        string[] part = stroka.Split(',');
                        string zagolovok = part[0];
                        string avtor = part[1];
                        int god = int.Parse(part[2]);
                        string tekst = part[3];
                        string tema = part[3];
                        writers.Add(new Pisatel()
                        {
                            Zagolovok = zagolovok,
                            Avtor = avtor,
                            God = god,
                            Tekst = tekst,
                            Tema = tema
                        });
                    }
                }
            }
            public void ReportTitel(string Poema_nazvanie, string imya_file = null)
            {
                List<Pisatel> result = FindTitel(Poema_nazvanie);

                StringBuilder report = new StringBuilder();
                report.AppendLine($"Poetry report with title'{Poema_nazvanie}':");
                report.AppendLine($"{"Title of the verse",-30}{"Avtor",-20}{"Year",-10}{"Theme",-20}");
                foreach (Pisatel Poema in result)
                    report.AppendLine($"{Poema.Zagolovok,-30}{Poema.Avtor,-20}{Poema.God,-10}{Poema.Tema,-20}");
                if (imya_file != null)
                {
                    using (StreamWriter sw = new StreamWriter(imya_file)) { sw.Write(report); }
                    Console.WriteLine($"Report saved to file {imya_file}");
                }
                else
                    Console.WriteLine(report);
            }
            public void GenerateReportByAuthor(string avtor, string imya_file = null)
            {
                List<Pisatel> result = FindAuthor(avtor);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Report{avtor}:");
                sb.AppendLine($"{"Title of the verse",-30}{"Author of the verse",-20}{"Year of the verse",-10}{"Theme of the verse",-20}");
                foreach (Pisatel Poema in result)
                    sb.AppendLine($"{Poema.Zagolovok,-30}{Poema.Avtor,-20}{Poema.God,-10} {Poema.Tema,-20}");
                if (imya_file != null)
                {
                    using (StreamWriter sw = new StreamWriter(imya_file)) { sw.Write(sb); }
                    Console.WriteLine($"Report saved to file {imya_file}");
                }
                else
                    Console.WriteLine(sb);
            }
            public void GenerateReportByThemePoem(string tema_poema, string imya_file = null)
            {
                List<Pisatel> result = FindByThemePoem(tema_poema);
                StringBuilder report = new StringBuilder();
                report.AppendLine($"Report on verses on the topic '{tema_poema}':");
                report.AppendLine($"{"Title of the verse",-30}{"Author",-20}{"Year",-10}{"Theme",-20}");
                foreach (Pisatel Poema in result)
                    report.AppendLine($"{Poema.Zagolovok,-30} {Poema.Avtor,-20} {Poema.God,-10} {Poema.Tema,-20}");
                if (imya_file != null)
                {
                    using (StreamWriter sw = new StreamWriter(imya_file)) { sw.Write(report); }
                    Console.WriteLine($"Report saved to file {imya_file}");
                }
                else
                    Console.WriteLine(report);
            }
            public List<Pisatel> FindByWord(string slovo) { return pisatel.Where(p => p.Tekst.ToLower().Contains(slovo.ToLower())).ToList(); }
            public List<Pisatel> FindByYear(int god) { return pisatel.Where(p => p.God == god).ToList(); }
            public List<Pisatel> FindByLength(int dlina) { return pisatel.Where(p => p.Tekst.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length == dlina).ToList(); }
        }
        static void Main(string[] args)
        {



        }
    }
}
