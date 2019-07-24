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
    /// Logique d'interaction pour Choix_du_Provier.xaml
    /// </summary>
    public partial class CreationProjet : Window
    {
        public MainWindow mw { get; set; }
        public CreationProjet()
        {
            InitializeComponent();
        }

        public CreationProjet(MainWindow m)
        {
            InitializeComponent();
            mw = m;
        }

        private void Bouton_validerProjet_Click(object sender, RoutedEventArgs e)
        {
            Projets P = new Projets();
            switch (Combo_Provider.Text)
            {
                case "Amazon Web Services":
                    P.Name = NomProjet.Text;
                    P.Provider = PROVIDER.AWS;
                    break;
                case "Azure":
                    P.Name = NomProjet.Text;
                    P.Provider = PROVIDER.AZURE;
                    break;
                case "Google Cloud Services":
                    P.Name = NomProjet.Text;
                    P.Provider = PROVIDER.GOOGLE;
                    break;
                default:
                    break;
            }
            if (NomProjet.Text == null || NomProjet.Text == "")
            {
                MessageBox.Show("Vous devez donner un nom au projet", "Nom invalide", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            } else if (Combo_Provider.Text == null || Combo_Provider.Text == "")
            {
                MessageBox.Show("Vous devez séléctionner un provider", "Provider invalide", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
            else
            {
                mw.database.ProjetDatabase.Add(P);
                mw.database.SaveChanges();

                Dashboard D = new Dashboard(mw, P);
                D.Show();
                Close();
            }
            
        }
    }
}
