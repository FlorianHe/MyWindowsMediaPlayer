using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lecteur
{
    public class Playlist
    {
        public string name;
        public List<Song> lsong = new List<Song>();
        
        public void setname(string newname)
        {
            name = newname;
        }

        public void addsong(Song newsong)
        {
            lsong.Add(newsong);
        }
    }
}
