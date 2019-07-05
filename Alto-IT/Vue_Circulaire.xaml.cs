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
        public Exigence ItemSelectionne { get; set; }
       public List<Exigence> ListeExigence { get; set; }

        public Exigence ROOT { get; set; }

        public Vue_Circulaire()
        {
            InitializeComponent();
        }

        public Vue_Circulaire(Dashboard D)
        {
            InitializeComponent();
            dash = D;

            ROOT = new Exigence() { Name = "Menu" }; //modifier le nom entraine un changement dans Ajout.cs(RemplirTable)
            treeviewFrame.Items.Add(ROOT);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Height = SystemParameters.WorkArea.Height - 90;
            this.Width = SystemParameters.WorkArea.Width - 190;
            AfficherDatabase();
        }

        public void AfficherDatabase()
        {
            dash.mw.database.ExigenceDatabase.ToList();
            
            for (int i = 0; i < dash.mw.database.ExigenceDatabase.ToList().Count(); i++)
            {
                for (int j = 0; j < dash.mw.database.ExigenceDatabase.ToList().Count(); j++)
                {
                    if (dash.mw.database.ExigenceDatabase.ToList()[i].Id == dash.mw.database.ExigenceDatabase.ToList()[j].ForeignKey)
                    {
                        if (!dash.mw.database.ExigenceDatabase.ToList().Contains(dash.mw.database.ExigenceDatabase.ToList()[i]) )
                        {
                            dash.mw.database.ExigenceDatabase.ToList()[i].ExigenceObervCollec.Add(dash.mw.database.ExigenceDatabase.ToList()[j]);
                        }
                    }
                    else if(dash.mw.database.ExigenceDatabase.ToList()[i].ForeignKey == 0)
                    {
                        if (!ROOT.ExigenceObervCollec.ToList().Contains(dash.mw.database.ExigenceDatabase.ToList()[i]))
                        {
                            ROOT.ExigenceObervCollec.Add(dash.mw.database.ExigenceDatabase.ToList()[i]);
                        }
                    }
                    
                }
            }
        }


        private void TreeviewFrame_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ItemSelectionne = (Exigence)treeviewFrame.SelectedItem;
        }
    }
}
