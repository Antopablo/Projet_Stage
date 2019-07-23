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
            List<string> X = new List<string>();

            foreach (Projets item in mw.database.ProjetDatabase.ToList())
            {
                X.Add(item.Name);
            }

            Combo_Provider.ItemsSource = X;

        }

        private void ValiderChoixProjet_Click(object sender, RoutedEventArgs e)
        {
            // récupéré le projet dans la bdd qui porte le nom selectionné

            Dashboard D = new Dashboard(mw, Combo_Provider.Text);
            D.Show();
            Close();
        }
    }
}
