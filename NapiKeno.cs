using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENO
{
    internal class NapiKeno
    {
        public int ev;
        public int het;
        public int nap;
        public string huzasDatum;
        public List<int> huzottSzamok;

        public NapiKeno(string[] sor)
        {
            this.ev = int.Parse(sor[0]);
            this.het = int.Parse(sor[1]);
            this.nap = int.Parse(sor[2]);
            this.huzasDatum = sor[3];
            huzottSzamok = new List<int>();
            for (int i = 4; i < sor.Length; i++)
            {
                this.huzottSzamok.Add(int.Parse(sor[i]));
            }

        }
        
        public int TalalatSzam(List<int> tippek)
        {
            int talalatokSzama = 0;
            for (int i = 0; i < tippek.Count; i++)
            {
                if (this.huzottSzamok.Contains(tippek[i]))
                    talalatokSzama++;
            }
            return talalatokSzama;
        }

        public bool Helyes()
        {
            if(huzottSzamok.Count() != 20 || huzottSzamok.Distinct().Count() != 20)
                return false;
            return true;
        }
    }
}
