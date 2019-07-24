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

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour AjoutNorme.xaml
    /// </summary>
    public partial class AjoutNorme : Window
    {
        MainWindow mw;
        Dashboard dashb;
        Vue_Circulaire Vue;
        public AjoutNorme(MainWindow m, Dashboard dash,Vue_Circulaire v)
        {
            InitializeComponent();
            mw = m;
            dashb = dash;
            Vue = v;
        }

        private void ValiderNorme_Click(object sender, RoutedEventArgs e)
        {
            Norme N = new Norme(Title.Text,dashb.ProjetEncours.Id) ;
            List<string> Controle = new List<string>();
            if (Title.Text == "" || Title.Text == null)
            {
                MessageBox.Show("Veuillez saisir le nom de la Norme", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                foreach (Norme item in dashb.ROOT_Normes.NormeObervCollec)
                {
                    Controle.Add(item.Nom_Norme);
                }
                if (!Controle.Contains(Title.Text))
                {
                    dashb.ROOT_Normes.NormeObervCollec.Add(N);
                    mw.database.NormeDatabase.Add(N);
                    mw.database.SaveChanges();
                    Close();
                }
                else
                {
                    MessageBox.Show("Cette Norme éxiste déja", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
            
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            dashb.FenetreOuverte = false;
        }
    }
}
