using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagyarOAdatBazisLetrehozas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Ekezetlenites("ÁrvíztűrőTükörFúróGép"));
            EgyedisegElemzes("helyforr.dat");
            EgyedisegElemzes("koordinatak.csv");
            EgyedisegElemzes("iranyitoszamok.csv");
            Console.ReadKey();
        }
        static string Ekezetlenites(string ekezetes)
        {
            string ekezetlen = "";
            char[] atalakitando = { 'á', 'Á', 'é', 'É', 'ú', 'Ú', 'ő', 'Ő', 'ű', 'Ű', 'ó', 'Ó', 'ü', 'Ü', 'ö', 'Ö', 'í', 'Í' };
            char[] atalakitott = { 'a', 'A', 'e', 'E', 'u', 'U', 'o', 'O', 'u', 'U', 'o', 'O', 'u', 'U', 'o', 'O', 'i', 'I' };
            foreach (char c in ekezetes ) 
            {
                bool van = false;
                int i = 0;
                for (; i < atalakitott.Length; i++ )
                {
                    if(c == atalakitando[i]) 
                    {
                        van = true; 
                        break; 
                    }
                }
                if( van )
                {
                    ekezetlen += atalakitott[i];
                }
                else
                {
                    ekezetlen += c;
                }
            }

            return ekezetlen;
        }
        static void EgyedisegElemzes(string file)
        {
            int pozicio = -1;
            char szeparator='\0';
            int kezdo = -1;
            switch (file)
            {
                case "helyforr.dat":
                    pozicio = 3;
                    szeparator = ' ';
                    kezdo = 0;
                    break;
                case "iranyitoszamok.csv":
                    pozicio = 1;
                    szeparator = ';';
                    kezdo = 1;
                    break;
                case "koordinatak.csv":
                    pozicio = 0;
                    szeparator = ';';
                    kezdo = 1;
                    break;
                default:
                    return;
            }

            StreamReader streamReader = new StreamReader(file);
            StreamReader olvaso = streamReader;
            Dictionary<string,int> szotar = new Dictionary<string,int>();
            //koordinatak.csv:0
            //iranyitoszamok.csv:1
            //helyforr.dat:3
            int db = - 1;
            while (!olvaso.EndOfStream)
            {
                string sor = olvaso.ReadLine();
                db++;
                if (db >= kezdo)
                {
                    string[] reszek = sor.Split(szeparator);
                    try
                    {
                        szotar[Ekezetlenites(reszek[pozicio])]++;
                    }
                    catch 
                    {
                        szotar[Ekezetlenites(reszek[pozicio])]=1;
                    }
                }

            }
            olvaso.Close();
            var ismetlodo = from telepules in szotar where telepules.Value >1 select telepules.Key;
        }
        static void URLEllenorzes(string url)
        {
            WebRequest keres=WebRequest.Create(url);
        }
        
    }
}
