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
    /// Logique d'interaction pour Projet.xaml
    /// </summary>
    public partial class Projet : Window
    {
        public MainWindow mw { get; set; }
        public Projet(MainWindow m)
        {
            InitializeComponent();
            mw = m;
            afficherProjets();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CreationProjet P = new CreationProjet(mw);
            P.Show();
            Close();
        }

        public void afficherProjets()
        {
            foreach (Projets item in mw.database.ProjetsDatabase.ToList())
            {
                listeProjet.Items.Add(item);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listeProjet.Text == "")
            {
                MessageBox.Show("Veuillez choisir un projet", "erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Dashboard D = new Dashboard(mw, (Projets)listeProjet.SelectedValue);
                D.Show();
                Close();
            }
            
        }
    }
}
