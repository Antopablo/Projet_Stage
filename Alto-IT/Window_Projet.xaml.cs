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
        public Projet()
        {
            InitializeComponent();
        }

        public Projet(MainWindow m)
        {
            InitializeComponent();
            mw = m;
            AfficherProjet();
        }


        private void Add_Projet_Click(object sender, RoutedEventArgs e)
        {
            CreationProjet CP = new CreationProjet(mw);
            CP.Show();
            Close();

        }

        public void AfficherProjet ()
        {
            foreach (Projets item in mw.database.ProjetDatabase.ToList())
            {
                Combo_Provider.Items.Add(item);
            }
        }

        private void ValiderChoixProjet_Click(object sender, RoutedEventArgs e)
        {
            if (Combo_Provider.Text == null || Combo_Provider.Text == "")
            {
                MessageBox.Show("Vous devez choisir un projet ou en créer un nouveau", "Choix du projet", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                Dashboard D = new Dashboard(mw, (Projets)Combo_Provider.SelectedValue);
                D.Show();
                Close();
            }

        }
    }
}
