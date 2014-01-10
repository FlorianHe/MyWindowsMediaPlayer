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

namespace lecteur
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        OpenFileDialog ofd = new OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();
           
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);

        }

        void timer_Tick(object sender, EventArgs e)
        {
            slider_seek.Value = mediaElement1.Position.TotalSeconds;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.Source != null)
            {
                mediaElement1.Visibility = System.Windows.Visibility.Visible;
                mediaElement1.Play();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.Source != null)
            mediaElement1.Pause();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.Source != null)
            mediaElement1.Stop();
        }

        private void slider_vol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Volume = (double)slider_vol.Value;
        }

        private void slider_seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Position = TimeSpan.FromSeconds(slider_seek.Value);
        }

        private void Window_Drop(object sender, System.Windows.DragEventArgs e)
        {
            string filename = (string)((System.Windows.DataObject)e.Data).GetFileDropList()[0];
            mediaElement1.Source = new Uri(filename);

            mediaElement1.LoadedBehavior = MediaState.Manual;
            mediaElement1.UnloadedBehavior = MediaState.Manual;
            mediaElement1.Volume = (double)slider_vol.Value;
            mediaElement1.Play();

        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.NaturalDuration.HasTimeSpan == true)
            {
                TimeSpan ts = mediaElement1.NaturalDuration.TimeSpan;
                slider_seek.Maximum = ts.TotalSeconds;
                timer.Start();
            }

        }

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

        private void Quitter(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 test = new Window1();
            test.Show();
        }


        private void test_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mediaElement1.Width = 200;
            mediaElement1.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            mediaElement1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        }

        private void createpl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Song w = new Song("chut", "marcha", "rap", "test");
            Song x = new Song("chutt", "marchaa", "raap", "teest");
            Console.WriteLine(w.name);
            Playlist p = new Playlist();
            p.setname("wlala");
            p.addsong(w);
            p.addsong(x);

            System.IO.FileStream fs = System.IO.File.Create("../../test.xml");
            fs.Close();

            XmlSerializer xs = new XmlSerializer(typeof(Playlist));
            using (StreamWriter wr = new StreamWriter("../../test.xml"))
            {
                xs.Serialize(wr, p);
            }
            
            Playlist g = new Playlist();
            XmlSerializer ps = new XmlSerializer(typeof(Playlist));
            using (StreamReader rd = new StreamReader("../../test.xml"))
            {
                g = ps.Deserialize(rd) as Playlist;
                Console.WriteLine(g.name);
            }
        }
    }
}
