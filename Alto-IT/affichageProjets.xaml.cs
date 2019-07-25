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
using System.Windows.Shapes;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour affichageProjets.xaml
    /// </summary>
    public partial class affichageProjets : Window
    {
        public Projets ProjetSelectionne { get; set; }

        public MainWindow mw { get; set; }
        public affichageProjets(MainWindow m, Projets P)
        {
            InitializeComponent();
            mw = m;
            ProjetSelectionne = P;
            projetName.Text = ProjetSelectionne.Nom;
        }

        private void ValiderModif_Click(object sender, RoutedEventArgs e)
        {
            if (projetName.Text != "")
            {
                ProjetSelectionne.Nom = projetName.Text;

                switch (comboboxProvider.Text)
                {
                    case "Azure":
                        ProjetSelectionne.CloudProvider = PROVIDER.AZURE; 
                        break;
                    case "Amazon Web Service":
                        ProjetSelectionne.CloudProvider = PROVIDER.AWS;
                        break;
                    case  "Google Cloud Service":
                        ProjetSelectionne.CloudProvider = PROVIDER.GOOGLE;
                        break;
                    default:
                        break;
                }

                mw.database.SaveChanges();
            }
            else
            {
                MessageBox.Show("Champ manquant", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            MessageBox.Show("Modification effectuée avec succès", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }
    }
}
