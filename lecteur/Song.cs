using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecteur
{
    public class Song
    {
        public string name;
        public string artist;
        public string genre;
        public string chemin;

        public Song()
        {

        }

        public Song(string nname, string nartist, string ngenre, string nchemin)
        {
            name = nname;
            artist = nartist;
            genre = ngenre;
            chemin = nchemin;
        }

        public string getnam
        {
            get { return name; }
        }

        public void setname(string nname)
        {
            name = nname;
        }

        public void setartist(string nartist)
        {
            artist = nartist;
        }

        public void setgenre(string ngenre)
        {
            genre = ngenre;
        }

        public void setchemin(string nchemin)
        {
            chemin = nchemin;
        }
    }
}
