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
    /// Logique d'interaction pour Modifier.xaml
    /// </summary>
    public partial class Modifier : Window
    {
        public MainWindow mw { get; set; }
        public Vue_Circulaire Vue { get; set; }
        public Modifier()
        {
            InitializeComponent();
        }

        public Modifier(MainWindow m, Vue_Circulaire v)
        {
            InitializeComponent();
            mw = m;
            Vue = v;
        }

        private void ModifierNorme_Click(object sender, RoutedEventArgs e)
        {
            String name = Vue.ItemSelectionne.Name.Replace(' ', '_');
            MessageBox.Show(name);

            using ( ApplicationDatabase context = new ApplicationDatabase())
            {
                var x = context.Database.ExecuteSqlCommand("UPDATE " + name + " SET Titre = '" + Title.Text + "' WHERE Titre = "+"'"+name+"'"+" ");
            }
        }
    }
}
