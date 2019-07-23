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
        }


        private void Add_Projet_Click(object sender, RoutedEventArgs e)
        {
            CreationProjet CP = new CreationProjet(mw);
            CP.Show();
            Close();

        }
    }
}
