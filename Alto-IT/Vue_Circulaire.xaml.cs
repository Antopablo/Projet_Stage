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
    /// Logique d'interaction pour Vue_Circulaire.xaml
    /// </summary>
    public partial class Vue_Circulaire : Page
    {
        public Dashboard dash { get; set; }
        public Norme ItemSelectionne { get; set; }

        public Vue_Circulaire()
        {
            InitializeComponent();
        }

        public Vue_Circulaire(Dashboard D)
        {
            InitializeComponent();
            dash = D;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AfficherDatabase();
        }

        private void AfficherDatabase()
        {
            treeviewFrame.ItemsSource = dash.mw.database.NormesDatabase.Local ;
            dash.mw.database.NormesDatabase.ToList();
        }

        private void TreeviewFrame_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void TreeviewFrame_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ItemSelectionne = (Norme)treeviewFrame.SelectedItem;
        }
    }
}
