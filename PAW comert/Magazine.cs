using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAW_comert
{

    public enum NumeMagazine
    {
        Auchan,
        Carrefour,
        Cora,
        Kaufland,
        Lidl,
        Mega_Image,
        Metro,
        Penny,
        Profi,
        Selgros,
        Altul,
        Na
    }
    [Serializable]
    public class Magazine
    {
        public NumeMagazine nume { get; set; }

        public Magazine()
        {
        }

        public Magazine(NumeMagazine nume)
        {
            this.nume = nume;
        }


    }
}
