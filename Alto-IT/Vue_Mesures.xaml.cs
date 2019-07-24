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
        public Mesures ROOT_Mesures { get; set; }
        public Mesures MesureSelectionnee { get; set; }


        public Vue_Mesures()
        {
            InitializeComponent();
        }

        public Vue_Mesures(Dashboard D)
        {
            InitializeComponent();
            dashb = D;
            dashb.Vue_Mesure = this;
            ROOT_Mesures = new Mesures("Menu");
            treeviewFrame.Items.Add(ROOT_Mesures);
        }

        private void TreeviewFrame_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MesureSelectionnee = (Mesures)treeviewFrame.SelectedItem;
        }

        private void Documentviewer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        public void AfficherTreeViewMesure()
        {
            dashb.mw.database.MesuresDatabase.ToList();

            for (int i = 0; i < dashb.mw.database.MesuresDatabase.ToList().Count(); i++)
            {
                for (int j = 0; j < dashb.mw.database.MesuresDatabase.ToList().Count(); j++)
                {
                    if (dashb.mw.database.MesuresDatabase.ToList()[i].Id == dashb.mw.database.MesuresDatabase.ToList()[j].FK_to_Mesures)
                    {
                        if (!dashb.mw.database.MesuresDatabase.ToList().Contains(dashb.mw.database.MesuresDatabase.ToList()[i]))
                        {
                            dashb.mw.database.MesuresDatabase.ToList()[i].MesuresObservCollec.Add(dashb.mw.database.MesuresDatabase.ToList()[j]);
                        }
                    }
                    else if ((dashb.mw.database.MesuresDatabase.ToList()[i].FK_to_Mesures == 0) && (dashb.ProjetEnCours.Id == dashb.mw.database.MesuresDatabase.ToList()[i].FK_to_Projets))
                    {
                        if (!ROOT_Mesures.MesuresObservCollec.ToList().Contains(dashb.mw.database.MesuresDatabase.ToList()[i]))
                        {
                            ROOT_Mesures.MesuresObservCollec.Add(dashb.mw.database.MesuresDatabase.ToList()[i]);
                        }
                    }

                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AfficherTreeViewMesure();
        }
    }
}
