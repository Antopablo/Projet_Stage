using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
                MessageBox.Show("Selectionnez une norme", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void BoutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (NormeSelectionnee != null)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer la norme " + NormeSelectionnee.Nom_Norme + " ?", "Supprimer", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    int IdSelected = NormeSelectionnee.Id;
                    
                    mw.database.NormeDatabase.Remove(NormeSelectionnee);
                    mw.database.SaveChanges();
                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        var Listeenfants = context.Database.SqlQuery<string>("SELECT Name from Exigences WHERE ForeignKey_TO_Norme= " + IdSelected).ToList();
                        foreach (string item in Listeenfants)
                        {
                            var x = context.Database.ExecuteSqlCommand("DROP TABLE " +dashb.SimpleCotFormater(dashb.FormaterToSQLRequest("_" + IdSelected + item)));
                            var zz = context.Database.ExecuteSqlCommand("DELETE FROM Exigences WHERE Name = " + "'" + dashb.SimpleCotFormater(item) + "'");
                        }
                    }
                    
                    dashb.ROOT_Normes.NormeObervCollec.Clear();
                    dashb.AfficherLesNormes();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Selectionnez une norme", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            dashb.FenetreOuverte = false;
        }

        private void AjoutDocument_Click(object sender, RoutedEventArgs e)
        {
            if (NormeSelectionnee != null)
            {
                OpenFileDialog open = new OpenFileDialog();
                open.ShowDialog();
                string filename = "(" + NormeSelectionnee.Id + ")" + open.SafeFileName;
                string targetPath = @"C:\Users\stagiaire\Desktop\Alto\Projet_Stage\Alto-IT\bin\Debug\Files\" + filename;
                try
                {

                    File.Copy(open.FileName, targetPath);
                    NormeSelectionnee.DocumentPath = targetPath;
                    NormeSelectionnee.DocumentName = filename;
                }
                catch (System.Exception)
                {
                    if (MessageBox.Show("Ce fichier éxiste déja voulez vous le Remplacer ?", "Erreur", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            File.Delete(targetPath);
                            File.Copy(open.FileName, targetPath);
                            NormeSelectionnee.DocumentPath = targetPath;
                            NormeSelectionnee.DocumentName = filename;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Impossible de supprimer");
                        }
                    }
                }
                MessageBox.Show("Document bien enregistré");
            }
            
        }
    }
}
