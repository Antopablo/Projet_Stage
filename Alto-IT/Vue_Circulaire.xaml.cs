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
        public Norme ItemSelectionne { get; set; }
       public List<Norme> ListeNormes { get; set; }

        public Norme ROOT { get; set; }

        public Vue_Circulaire()
        {
            InitializeComponent();
        }

        public Vue_Circulaire(Dashboard D)
        {
            InitializeComponent();
            dash = D;

            ROOT = new Norme() { Name = "Menu" }; //modifier le nom entraine un changement dans Ajout.cs(RemplirTable)
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
            dash.mw.database.NormesDatabase.ToList();
            
            for (int i = 0; i < dash.mw.database.NormesDatabase.ToList().Count(); i++)
            {
                for (int j = 0; j < dash.mw.database.NormesDatabase.ToList().Count(); j++)
                {
                    if (dash.mw.database.NormesDatabase.ToList()[i].Id == dash.mw.database.NormesDatabase.ToList()[j].ForeignKey)
                    {
                        if (!dash.mw.database.NormesDatabase.ToList().Contains(dash.mw.database.NormesDatabase.ToList()[i]) )
                        {
                            dash.mw.database.NormesDatabase.ToList()[i].NormeObervCollec.Add(dash.mw.database.NormesDatabase.ToList()[j]);
                        }
                    }
                    else if(dash.mw.database.NormesDatabase.ToList()[i].ForeignKey == 0)
                    {
                        if (!ROOT.NormeObervCollec.ToList().Contains(dash.mw.database.NormesDatabase.ToList()[i]))
                        {
                            ROOT.NormeObervCollec.Add(dash.mw.database.NormesDatabase.ToList()[i]);
                        }
                    }
                    
                }
            }
        }


        private void TreeviewFrame_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ItemSelectionne = (Norme)treeviewFrame.SelectedItem;
        }
    }
}
