using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.ObjectModel;

namespace lecteur
{
    public class Playlist
    {
        public string name;
        public ObservableCollection<Song> LSongs = new ObservableCollection<Song>();

        public string getname
        {
            get { return name; }
        }
        public void setname(string newname)
        {
            this.name = newname;
        }

        public void addsong(Song songs)
        {
            LSongs.Add(songs);
        }

    }
}
