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
            Dash.mw.database.MesuresDatabase.ToList();

            for (int i = 0; i < Dash.mw.database.MesuresDatabase.ToList().Count(); i++)
            {
                for (int j = 0; j < Dash.mw.database.MesuresDatabase.ToList().Count(); j++)
                {
                    if (Dash.mw.database.MesuresDatabase.ToList()[i].Id == Dash.mw.database.MesuresDatabase.ToList()[j].FKToMesure)
                    {
                        if (!Dash.mw.database.MesuresDatabase.ToList().Contains(Dash.mw.database.MesuresDatabase.ToList()[i]))
                        {
                            Dash.mw.database.MesuresDatabase.ToList()[i].MesureObservableCollec.Add(Dash.mw.database.MesuresDatabase.ToList()[j]);
                        }
                    }
                    else if (Dash.mw.database.MesuresDatabase.ToList()[i].FKToMesure == 0 && Dash.ProjetEncours.Id == Dash.mw.database.MesuresDatabase.ToList()[i].FKToProjets)
                    {
                        if (!ROOT_Mesures.MesureObservableCollec.ToList().Contains(Dash.mw.database.MesuresDatabase.ToList()[i]))
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
