using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace KenoGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GridFeltoltes();
        }

        private void GridFeltoltes()
        {
            for (int i = 1; i <= 80; i++)
            {
                Button btn = new Button
                {
                    Content = i.ToString(),
                    Background = Brushes.LightGreen,
                    FontWeight = FontWeights.Bold,
                };

                grSzelveny.Children.Add(btn);
            }
        }

        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                string[] beolvas = File.ReadAllLines(ofd.FileName);

                foreach (string sor in beolvas)
                {
                    lbSzelvenyek.Items.Add(sor);
                }
            }
        }

        private void lbSzelvenyek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Button btn in grSzelveny.Children)
            {
                btn.Background = Brushes.LightGreen;
                btn.Foreground = Brushes.Black;
            }

            if (lbSzelvenyek.SelectedItem == null)
            {
                return;
            }

            string szelveny = lbSzelvenyek.SelectedItem.ToString();

            string[] reszek = szelveny.Split('!');

            string szamok = reszek[1];

            string[] szamokTomb = szamok.Split(',');

            foreach (string szam in szamokTomb)
            {
                if (int.TryParse(szam, out int szamInt) && szamInt >= 1 && szamInt <= 80)
                {
                    Button btn = grSzelveny.Children[szamInt - 1] as Button;
                    if (btn != null)
                    {
                        btn.Background = Brushes.Yellow;
                        btn.Foreground = Brushes.Black;
                    }
                }
            }

            lblSzorzo.Content = $"{reszek[0].Split(",")[1]}";

            int nyeremeny = Szorzo(randomszamok,szamokTomb
                .Select(x => int.Parse(x)).ToList()) * 200 * int.Parse(reszek[0]
                .Split(",")[1]);

            lblNyeremeny.Content = $"{nyeremeny}";
        }

        Random rnd = new Random();
        List<int> randomszamok = new List<int>();
        private void btnSorsolas_Click(object sender, RoutedEventArgs e)
        {
            lbSorsolt.Items.Clear();
            
            while (randomszamok.Count < 20)
            {
                int szam = rnd.Next(1, 81);
                if (!randomszamok.Contains(szam))
                    randomszamok.Add(szam);
            }
            randomszamok.OrderBy(x => x).ToList().ForEach(x => lbSorsolt.Items.Add(x));

        }

        private int Szorzo(List<int> kenoSzamai, List<int> tippek)
        {
            Dictionary<String, int> nyeroParok = new Dictionary<string, int>(){
                {"10-10",1000000}, {"10-9",8000}, {"10-8",350}, {"10-7",30}, {"10-6",3}, {"10-5",1}, {"10-0",2},
                {"9-9",100000}, {"9-8",1200}, {"9-7",100}, {"9-6",12}, {"9-5",3}, {"9-0",1},
                {"8-8",20000}, {"8-7",350}, {"8-6",25}, {"8-5",5}, {"8-0",1},
                {"7-7",5000}, {"7-6",60}, {"7-5",6}, {"7-4",1}, {"7-0",1},
                {"6-6",500}, {"6-5",20}, {"6-4",3}, {"6-0",1},
                {"5-5",200}, {"5-4",10}, {"5-3",2},
                {"4-4",100}, {"4-3",2},
                {"3-3",15}, {"3-2",1},
                {"2-2",6},
                {"1-1",2}
            };
            int jatekTipus = tippek.Count;
            int talalatokSzama = 0;
            foreach (int tipp in tippek)
            {
                if (kenoSzamai.Contains(tipp))
                {
                    talalatokSzama++;
                }
            }
            string kulcs = jatekTipus + "-" + talalatokSzama;

            if (nyeroParok.Keys.Contains(kulcs))
                return nyeroParok[kulcs];
            else
                return 0;
        }
    }
}