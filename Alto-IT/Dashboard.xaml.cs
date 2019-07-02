using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public MainWindow mw { get; set; }
        public Norme ItemSelectionne { get; set; }
        public Dashboard(MainWindow m)
        {
            InitializeComponent();
            ItemSelectionne = new Norme();
            mw = m;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // FullScreen
            //this.Left = SystemParameters.WorkArea.Left;
            //this.Top = SystemParameters.WorkArea.Top;
            //this.Height = SystemParameters.WorkArea.Height;
            //this.Width = SystemParameters.WorkArea.Width;
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void AjoutNorme_Click(object sender, RoutedEventArgs e)
        {
            Ajout A = new Ajout(mw, this);
            A.Show();
        }

        private void Modif_norme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Supr_norme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cloud19714_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GridControle.Visibility = Visibility.Visible;
            Frame_Vue_Circulaire.Visibility = Visibility.Visible;

            treeview.ItemContainerGenerator.ContainerFromItem(treeview.SelectedItem);
        }
    }
}
