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
    /// Logique d'interaction pour AffichageDesNormes.xaml
    /// </summary>
    public partial class AffichageDesNormes : Window
    {
        MainWindow mw;
        Dashboard dashb;
        Norme NormeSelectionnee { get; set; }
        public AffichageDesNormes(MainWindow m, Dashboard dash)
        {
            InitializeComponent();
            mw = m;
            dashb = dash;
            ListeViewNormes.ItemsSource = mw.database.NormeDatabase.Local;
            mw.database.NormeDatabase.ToList();
        }

        private void ListeViewNormes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NormeSelectionnee = (Norme)ListeViewNormes.SelectedItem;
            if (NormeSelectionnee != null)
            {
                TitreModify.Text = NormeSelectionnee.Nom_Norme;
                TitreModifyBlock.Text = NormeSelectionnee.Nom_Norme;
            }
        }

        private void BoutonValiderModify_Click(object sender, RoutedEventArgs e)
        {

            if (NormeSelectionnee != null)
            {
                NormeSelectionnee.Nom_Norme = TitreModify.Text;
                mw.database.SaveChanges();
                dashb.ROOT_Normes.NormeObervCollec.Clear();
                dashb.AfficherLesNormes();
                Close();

            } else
            {
                MessageBox.Show("Selectionnez une norme à modifier", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void BoutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (NormeSelectionnee != null)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer la norme " + NormeSelectionnee.Nom_Norme + " ?", "Supprimer", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    int IDSelected = NormeSelectionnee.Id;

                    mw.database.NormeDatabase.Remove(NormeSelectionnee);
                    mw.database.SaveChanges(); 

                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        //var ListeEnfant = context.Database.SqlQuery<string>("SELECT Name FROM Exigences WHERE ForeignKey_TO_Norme = " + NormeSelectionnee.Id).ToList();
                        var ListeEnfant = context.Database.SqlQuery<string>("SELECT Name FROM Exigences WHERE ForeignKey_TO_Norme = " + IDSelected).ToList();
                        foreach (string item in ListeEnfant)
                        {
                            // supprime la table à son nom
                            var x = context.Database.ExecuteSqlCommand("DROP TABLE " + mw.SimpleQuoteFormater(mw.FormaterToSQLRequest(item)));
                            // supprime de Exigences
                            var zz = context.Database.ExecuteSqlCommand("DELETE FROM Exigences WHERE Name = '" + mw.SimpleQuoteFormater(item) + "'");
                        }
                    }

                    dashb.ROOT_Normes.NormeObervCollec.Clear();
                    dashb.AfficherLesNormes();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Selectionnez une norme à supprimer", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            dashb.FenetreOuverte = false;
        }
    }
}
