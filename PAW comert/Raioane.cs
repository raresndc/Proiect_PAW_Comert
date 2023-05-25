using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAW_comert
{

    public enum tip
    {
        Articole_bebelusi,
        Articole_de_imbracaminte,
        Auto_Moto_Brico,
        Carti_papetarie_educatie,
        Casa_gradina_petshop,
        Cosmetice_ingrijire_personala,
        Electrocasnice_climatizare,
        Jucarii_jocuri_copii,
        Mancare,
        Sport_timp_liber,
        TV_Audio_Home_cinema,
        Altceva,
        Na
    };
    [Serializable]

    public class Raioane
    {
        public tip tip { get; set; }
        public Raioane()
        {
            tip = 0;
        }
        public Raioane(tip tip_)
        {
            tip = tip_;
        }

    }
}
