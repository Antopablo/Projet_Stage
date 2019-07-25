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

        private void Modifierprojet_Click(object sender, RoutedEventArgs e)
        {
            if (listeProjet.SelectedItem != null)
            {
                affichageProjets Af = new affichageProjets(mw,(Projets)listeProjet.SelectedValue);
                Af.Show();
            }
            else
            {
                MessageBox.Show("Veuillez saisir un projet à modifier", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void Supprimerprojet_Click(object sender, RoutedEventArgs e)
        {
            if (listeProjet.Text != null || listeProjet.Text != "")
            {
                Projets projetsel = (Projets)listeProjet.SelectedValue;


                if (MessageBox.Show("Voulez vous vraiment supprimer le projet :" + projetsel.Nom + " ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    mw.database.ProjetsDatabase.Remove(projetsel);
                    mw.database.SaveChanges();
                    MessageBox.Show("Projet supprimé", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                    listeProjet.Items.Clear();
                    afficherProjets();
                }
            }
            
        }
    }
}
