using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAW_comert
{
    [Serializable]
    public class Desfaceri
    {
        public string numeProdus { get; set; }
        public int cantitate { get; set; }
        public int pret { get; set; }
        public Raioane raion { get; set; }
        public Magazine magazin { get; set; }

        public Desfaceri()
        {
        }

        public Desfaceri(string numeProdus, int cantitate, int pret)
        {
            this.numeProdus = numeProdus;
            this.cantitate = cantitate;
            this.pret = pret;
        }

        public Desfaceri(string numeProdus, int cantitate, int pret, Raioane raioane, Magazine magazine)
        {
            this.numeProdus = numeProdus;
            this.cantitate = cantitate;
            this.pret = pret;
            this.raion = raioane;
            this.magazin = magazine;
        }
    }
}
