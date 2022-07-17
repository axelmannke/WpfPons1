using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class PonsTranslation
    {
        public string lang { get; set; }
        public List<Hits> hits { get; set; } = new List<Hits>();
    }

    public class Hits
    {
        public string type { get; set; }
        public string opendict { get; set; }

        public List<Roms> roms { get; set; } = new List<Roms>();
    }

    public class Roms
    {
        public string headword { get; set; }
        public string headword_full { get; set; }
        public string wordclass { get; set; }
        public List<Arabs> arabs { get; set; } = new List<Arabs>();
    }

    //ToDo: Klasse Arabs und Translations implementieren
    public class Arabs
    {
       
    }
    public class Translations
    {
        
    }
}
