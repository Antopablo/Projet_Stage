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
    /// Logique d'interaction pour AjoutNorme.xaml
    /// </summary>
    public partial class AjoutNorme : Window
    {
        MainWindow mw;
        Dashboard dashb;
        Vue_Circulaire Vue;
        public AjoutNorme(MainWindow m, Dashboard dash,Vue_Circulaire v)
        {
            InitializeComponent();
            mw = m;
            dashb = dash;
            Vue = v;
        }

        private void ValiderNorme_Click(object sender, RoutedEventArgs e)
        {
            Norme N = new Norme(Title.Text) ;
            dashb.ROOT_Normes.NormeObervCollec.Add(N);
            mw.database.NormeDatabase.Add(N);
            mw.database.SaveChanges();
        }
    }
}
