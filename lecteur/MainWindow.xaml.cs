using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Controls.Primitives;
using System.ComponentModel;


namespace lecteur
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        DispatcherTimer timer;
        OpenFileDialog ofd = new OpenFileDialog();
        ObservableCollection<Playlist> saveplayxml = new ObservableCollection<Playlist>();
        ObservableCollection<Playlist> nameplay = new ObservableCollection<Playlist>();
        ObservableCollection<Song> gsong = new ObservableCollection<Song>();
        ObservableCollection<Song> savexml = new ObservableCollection<Song>();
        double savevolume;
        bool fullscreen = false;
        bool isDragging = false;
        double hauteur;
        double largeur;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            hauteur = mediaElement1.Height;
            largeur = mediaElement1.Width;
            string rep = Environment.CurrentDirectory;
            rep += "\\..\\..\\Icones\\";
            this.Background = new ImageBrush(new BitmapImage(new Uri(rep + "Fond_basic2.jpg")));
            mediaElement1.Source = new Uri(rep + "Note_musique1.jpg");

            //Affichage des playlists
            refreshlist();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += new EventHandler(timer_Tick);

        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
                slider_seek.Value = mediaElement1.Position.TotalSeconds;
        }

        //Bouton play
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.Source != null)
            {
                mediaElement1.Visibility = System.Windows.Visibility.Visible;
                mediaElement1.Play();
            }
            else
            {
                ofd.Filter = "MP4 (*.mp4)|*.mp4|MP3's (*.mp3)|*.mp3|AVI (*.avi)|*.avi|All files (*.*)|*.*";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    mediaElement1.Source = new Uri(ofd.FileName);
                    mediaElement1.LoadedBehavior = MediaState.Manual;
                    mediaElement1.UnloadedBehavior = MediaState.Manual;
                    mediaElement1.Play();
                }
            }
        }

        //Bouton Pause
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.Source != null)
                mediaElement1.Pause();
        }

        //Bouton Stop
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.Source != null)
                mediaElement1.Stop();
        }

        //Volume
        private void slider_vol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Volume = (double)slider_vol.Value;
        }

        private void muet_Click(object sender, RoutedEventArgs e)
        {
            muet.Visibility = System.Windows.Visibility.Hidden;
            resetvol.Visibility = System.Windows.Visibility.Visible;
            mediaElement1.Volume = savevolume;
            slider_vol.Value = savevolume;
        }

        private void resetvol_Click(object sender, RoutedEventArgs e)
        {
            resetvol.Visibility = System.Windows.Visibility.Hidden;
            muet.Visibility = System.Windows.Visibility.Visible;
            savevolume = mediaElement1.Volume;
            mediaElement1.Volume = 0;
            slider_vol.Value = 0;
        }

        private void seekBar_DragStarted(Object sender, DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void seekBar_DragCompleted(Object sender, DragCompletedEventArgs e)
        {
            isDragging = false;
            mediaElement1.Position = TimeSpan.FromSeconds(slider_seek.Value);
        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.NaturalDuration.HasTimeSpan == true)
            {
                TimeSpan ts = mediaElement1.NaturalDuration.TimeSpan;
                slider_seek.Maximum = ts.TotalSeconds;
                slider_seek.SmallChange = 1;
                slider_seek.LargeChange = Math.Min(10, ts.Seconds / 10);
            }
            timer.Start();
        }

        //Ouvrir les fichiers par le répertoire
        private void Ouvrir(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "MP4 (*.mp4)|*.mp4|MP3's (*.mp3)|*.mp3|AVI (*.avi)|*.avi|All files (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mediaElement1.Source = new Uri(ofd.FileName);
                mediaElement1.LoadedBehavior = MediaState.Manual;
                mediaElement1.UnloadedBehavior = MediaState.Manual;
                mediaElement1.Play();
            }
        }

        //FullScreen
        private void mediaElement1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && fullscreen == false)
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
                fullscreen = true;
            }
            else if (e.ClickCount == 2 && fullscreen == true)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                fullscreen = false;
            }
        }

        //Fermeture du programme
        private void Quitter(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        //Affichage des boutons pour la création des playlist
        private void Create_playlist(object sender, RoutedEventArgs e)
        {
            crtext.Visibility = System.Windows.Visibility.Visible;
            Creer.Visibility = System.Windows.Visibility.Visible;
        }

        //Affichage des membres du projets
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 test = new Window1();
            test.Show();
        }

        //Mise a jour des playlists
        public void refreshlist()
        {
            string[] file = Directory.GetFiles("../../", "*.xml");
            Playlist woot;
            foreach (string dir in file)
            {
                string value = System.IO.Path.GetFileNameWithoutExtension(dir);
                woot = new Playlist();
                woot.setname(value);
                nameplay.Add(woot);
            }
        }

        //Biding mylist
        public ObservableCollection<Playlist> Playlists
        {
            get { return nameplay; }
        }

        //Biding songlist
        public ObservableCollection<Song> Songs
        {
            get { return gsong; }
        }

        //Génération de la playlist, affichage son
        private void mylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Réduction du mediaelement
            mediaElement1.Width = 200;
            mediaElement1.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            mediaElement1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            //Récupération de la playlist
            int index = Convert.ToInt32(mylist.SelectedIndex.ToString());
            Playlist value = new Playlist();
            if (index >= 0 && index < mylist.Items.Count)
            {
                value = (Playlist)mylist.Items[index];
                string file = value.name;
                file = "../../" + file + ".xml";

                //Affichage du titre de la playlist en cours et du boutonsave
                titleplay.Content = value.name;
                titleplay.Visibility = System.Windows.Visibility.Visible;
                saveplay.Visibility = System.Windows.Visibility.Visible;
                fondlabel.Visibility = System.Windows.Visibility.Visible;

                ObservableCollection<Playlist> myplay = new ObservableCollection<Playlist>();
                XmlSerializer ps = new XmlSerializer(typeof(ObservableCollection<Playlist>));
                using (StreamReader rd = new StreamReader(file))
                {
                    myplay = ps.Deserialize(rd) as ObservableCollection<Playlist>;
                }
                gsong.Clear();

                //Affichage des sons de la playlist
                int i = 0;
                Song displaysong;
                while (i < myplay[0].LSongs.Count)
                {
                    displaysong = new Song();
                    displaysong.name = myplay[0].LSongs[i].name;
                    displaysong.artist = myplay[0].LSongs[i].artist;
                    displaysong.genre = myplay[0].LSongs[i].genre;
                    displaysong.chemin = myplay[0].LSongs[i].chemin;
                    gsong.Add(displaysong);
                    i++;
                }
            }
            songlist.Visibility = System.Windows.Visibility.Visible;
        }

        //Creation de la playlist
        private void Creer_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("test");
            string value = crtext.Text;
            value = "../../" + value + ".xml";
            Console.WriteLine(value);
            System.IO.FileStream fs = System.IO.File.Create(value);
            fs.Close();

            ObservableCollection<Playlist> otmp = new ObservableCollection<Playlist>();
            Playlist tmp = new Playlist();
            tmp.name = crtext.Text;
            otmp.Add(tmp);

            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Playlist>));
            using (StreamWriter wr = new StreamWriter(value))
            {
                xs.Serialize(wr, otmp);
            }

            crtext.Visibility = System.Windows.Visibility.Hidden;
            Creer.Visibility = System.Windows.Visibility.Hidden;
            nameplay.Clear();
            refreshlist();
            OnPropertyChanged("Playlists");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
        }


        //Lecture des musiques
        private void lectsong(int index, int ajout)
        {
            Song readsong;
            Console.WriteLine(index);

            readsong = new Song();
            if ((index + ajout) >= 0 && (index + ajout) < songlist.Items.Count)
            {
                readsong = (Song)songlist.Items[index + ajout];
                string filename = readsong.chemin;

                Boolean file = System.IO.File.Exists(filename);
                if (file == true)
                {
                    mediaElement1.Source = new Uri(filename);
                    mediaElement1.LoadedBehavior = MediaState.Manual;
                    mediaElement1.UnloadedBehavior = MediaState.Manual;
                    mediaElement1.Volume = (double)slider_vol.Value;
                    mediaElement1.Play();
                }
                else
                {
                    ErreurFile error = new ErreurFile();
                    error.Show();
                }
            }
        }

        private void songlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = Convert.ToInt32(songlist.SelectedIndex.ToString());
            lectsong(index, 0);
            
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32(songlist.SelectedIndex.ToString());
            lectsong(index, -1);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32(songlist.SelectedIndex.ToString());
            lectsong(index, 1);
        }


        //Drop des fichiers dans la playlist
        private void songlist_Drop(object sender, System.Windows.DragEventArgs e)
        {
            string chem = (string)((System.Windows.DataObject)e.Data).GetFileDropList()[0];
            string filename = System.IO.Path.GetFileNameWithoutExtension(chem);
            Song nesong = new Song();
            nesong.name = filename;
            nesong.chemin = chem;
            gsong.Add(nesong);
        }

        //Drop ds le mediaelement
        private void mediaElement1_Drop(object sender, System.Windows.DragEventArgs e)
        {
            string chem = (string)((System.Windows.DataObject)e.Data).GetFileDropList()[0];
            mediaElement1.Source = new Uri(chem);
            mediaElement1.LoadedBehavior = MediaState.Manual;
            mediaElement1.UnloadedBehavior = MediaState.Manual;
            mediaElement1.Volume = (double)slider_vol.Value;
            mediaElement1.Play();
        }

        //Sauvegarde de la playlist
        private void saveplay_Click(object sender, RoutedEventArgs e)
        {
            Song tmp;
            Song savesong;
            Playlist saveplaylist = new Playlist();
            saveplaylist.name = titleplay.Content.ToString();
            Console.WriteLine(saveplaylist.name);
            int i = 0;
            while (i < songlist.Items.Count)
            {
                tmp = new Song();
                savesong = new Song();
                tmp = (Song)songlist.Items[i];
                savesong.name = tmp.name;
                savesong.chemin = tmp.chemin;
                saveplaylist.LSongs.Add(savesong);
                i++;
            }
            saveplayxml.Add(saveplaylist);

            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Playlist>));
            using (StreamWriter wr = new StreamWriter("../../"+saveplaylist.name+".xml"))
            {
                xs.Serialize(wr, saveplayxml);
            }

        }

        private void mediaElement1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            songlist.Visibility = System.Windows.Visibility.Hidden;
            titleplay.Visibility = System.Windows.Visibility.Hidden;
            saveplay.Visibility = System.Windows.Visibility.Hidden;
            fondlabel.Visibility = System.Windows.Visibility.Hidden;

            mediaElement1.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            mediaElement1.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            mediaElement1.Height = hauteur;
            mediaElement1.Width = largeur;
        }

        //Changement des thèmes
        private string initheme()
        {
            Play.Content = null;
            Pause.Content = null;
            Stop.Content = null;
            muet.Content = null;
            resetvol.Content = null;
            Creer.Content = null;
            saveplay.Content = null;
            screenfull.Content = null;
            Prev.Content = null;
            Next.Content = null;
            string rep = Environment.CurrentDirectory;
            rep += "\\..\\..\\Icones\\";
            return rep;
        }

        private void choosetheme(string rep, string nb)
        {
            Play.Background = new ImageBrush(new BitmapImage(new Uri(rep + "play" + nb + ".png")));
            Pause.Background = new ImageBrush(new BitmapImage(new Uri(rep + "pause" + nb + ".png")));
            Stop.Background = new ImageBrush(new BitmapImage(new Uri(rep + "stop" + nb + ".png")));
            muet.Background = new ImageBrush(new BitmapImage(new Uri(rep + "son_off" + nb + ".png")));
            resetvol.Background = new ImageBrush(new BitmapImage(new Uri(rep + "son_on" + nb + ".png")));
            Creer.Background = new ImageBrush(new BitmapImage(new Uri(rep + "creer" + nb + ".png")));
            saveplay.Background = new ImageBrush(new BitmapImage(new Uri(rep + "save" + nb + ".png")));
            screenfull.Background = new ImageBrush(new BitmapImage(new Uri(rep + "fullscreen" + nb + ".png")));
            Prev.Background = new ImageBrush(new BitmapImage(new Uri(rep + "precedent" + nb + ".png")));
            Next.Background = new ImageBrush(new BitmapImage(new Uri(rep + "suivant" + nb + ".png")));
        }
        private void ColorDefault(object sender, RoutedEventArgs e)
        {
            string rep = initheme();
            this.Background = new ImageBrush(new BitmapImage(new Uri(rep+"Fond_basic2.jpg")));
            choosetheme(rep, "5");
        }

        private void ColorOcean(object sender, RoutedEventArgs e)
        {
            string rep = initheme();
            this.Background = new ImageBrush(new BitmapImage(new Uri(rep + "Fond_ocean.jpg")));
            choosetheme(rep, "4");
        }

        private void ColorPigeon(object sender, RoutedEventArgs e)
        {
            string rep = initheme();
            this.Background = new ImageBrush(new BitmapImage(new Uri(rep + "Fond_animaux.jpg")));
            choosetheme(rep, "");
        }

        private void ColorCiel(object sender, RoutedEventArgs e)
        {
            string rep = initheme();
            this.Background = new ImageBrush(new BitmapImage(new Uri(rep + "Fond_basic3.png")));
            choosetheme(rep, "3");
        }

        private void ColorNuit(object sender, RoutedEventArgs e)
        {
            string rep = initheme();
            this.Background = new ImageBrush(new BitmapImage(new Uri(rep + "Fond_basic.jpg")));
            choosetheme(rep, "2");
        }

        private void screenfull_Click(object sender, RoutedEventArgs e)
        {
            if (fullscreen == false)
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
                fullscreen = true;
            }
            else if (fullscreen == true)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                fullscreen = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
