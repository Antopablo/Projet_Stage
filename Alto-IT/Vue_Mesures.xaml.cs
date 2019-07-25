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
        public Mesures MesureSelectionne { get; set; }
        public Dashboard Dash { get; set; }

        public Mesures ROOT_Mesures { get; set; }
        public Vue_Mesures()
        {
            InitializeComponent();

        }

        public Vue_Mesures(Dashboard D)
        {
            InitializeComponent();
            Dash = D;
            ROOT_Mesures = new Mesures("Menu");
            Dash.Vue_Mesure = this;
            treeviewFrame.Items.Add(ROOT_Mesures);

        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TreeviewFrame_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MesureSelectionne = (Mesures)treeviewFrame.SelectedItem; 
        }



        public void AfficherTreeViewMesures()
        {
            Mesures[] Li = Dash.mw.database.MesuresDatabase.ToArray();
            Mesures[] Lj = Li;
            int[] Ls = new int[Lj.Length];
            int[] lar = new int[Lj.Length];
            for (int i = 0; i < Lj.Length; i++)
            {
                Ls[i] = Lj[i].Id;
            }
            for (int i = 0; i < Li.Length; i++)
            {
                int M = Li[i].Id;
                if ((Li[i].Id == Lj[i].FKToMesure) && (Array.BinarySearch(Ls, M) < 0))
                {
                    lar[i] = M;
                    lock (ROOT_Mesures.MesureObservableCollec)
                    {
                        Dash.mw.database.MesuresDatabase.ToList()[i].MesureObservableCollec.Add(Dash.mw.database.MesuresDatabase.ToList()[i]);
                    }
                }
                else if ((Li[i].FKToMesure == 0) && (Dash.ProjetEncours.Id == Li[i].FKToProjets))
                {
                    int MM = Li[i].Id;
                    if (Array.BinarySearch(lar, MM) < 0)
                    {
                        lar[i] = MM;
                        lock (ROOT_Mesures.MesureObservableCollec)
                        {
                            ROOT_Mesures.MesureObservableCollec.Add(Dash.mw.database.MesuresDatabase.ToList()[i]);
                        }
                    }
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AfficherTreeViewMesures();
        }
    }
}
