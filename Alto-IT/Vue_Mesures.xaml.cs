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
    /// Logique d'interaction pour Vue_Mesures.xaml
    /// </summary>
    public partial class Vue_Mesures : Page
    {
        public Dashboard dashb { get; set; }
        public Mesures MyProperty { get; set; }
        public Vue_Mesures()
        {
            InitializeComponent();
        }

        public Vue_Mesures(Dashboard D)
        {
            InitializeComponent();
            dashb = D;
        }

        private void TreeviewFrame_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void Documentviewer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
