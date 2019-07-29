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
    /// Logique d'interaction pour Ajout.xaml
    /// </summary>
    public partial class Ajout : Window
    {
        MainWindow mw;
        Dashboard dashb;
        Vue_Circulaire Vue;
        public Ajout(MainWindow m, Dashboard d, Vue_Circulaire vc)
        {
            InitializeComponent();
            mw = m;
            dashb = d;
            Vue = vc;
        }

        private void ValiderExigence_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "" || Title.Text == null)
            {
                MessageBox.Show("Veuillez saisir le nom de L'exigence", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (Title.Text == "Menu")
                {
                    MessageBox.Show("Vous ne pouvez pas appeler une norme ainsi", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (Vue.ExigenceSelectionne == null || Vue.ExigenceSelectionne.Name == "Menu")
                    {
                        try
                        {
                            CreateTable(Title.Text);
                            Exigence ExigenceParent = new Exigence(Title.Text, Content.Text, 0, dashb.NormeSelectionnee.Id, dashb.ProjetEncours.Id);
                            Vue.ROOT_Exigences.ExigenceObervCollec.Add(ExigenceParent);
                            mw.database.ExigenceDatabase.Add(ExigenceParent);
                            RemplirDico(ExigenceParent);
                            mw.database.SaveChanges();
                            Close();
                        }
                        catch (System.Data.SqlClient.SqlException)
                        {
                            MessageBox.Show("Une éxigence porte déja ce nom", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        try
                        {
                            CreateTable(Title.Text);
                            try
                            {
                                RemplirTable(Vue.ExigenceSelectionne.Name, Vue.ExigenceSelectionne.Id);
                                Exigence ExigenceEnfant = new Exigence(Title.Text, Content.Text, Vue.ExigenceSelectionne.Id, dashb.NormeSelectionnee.Id, dashb.ProjetEncours.Id);
                                Vue.ExigenceSelectionne.ExigenceObervCollec.Add(ExigenceEnfant);
                                mw.database.ExigenceDatabase.Add(ExigenceEnfant);
                                RemplirDico(ExigenceEnfant);
                            }
                            catch (Exception)
                            {
                                SupprimerTable(Title.Text);
                                MessageBox.Show("Impossible de remplir la Table Parent", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            Close();
                        }
                        catch (System.Data.SqlClient.SqlException)
                        {
                            MessageBox.Show("Une éxigence porte déja ce nom", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        

                        mw.database.SaveChanges();

                    }
                }
            }
            
            
        }

        public void RemplirDico(Exigence E)
        {
            foreach (Mesures item in mw.database.MesuresDatabase)
            {
                item.Dico_ExigenceCheck.Add(E,false);
            }
        }

        public void CreateTable(string TableName)
        {
            TableName = dashb.TableFormater(dashb.SimpleCotFormater(dashb.FormaterToSQLRequest(TableName)));

            using (ApplicationDatabase context = new ApplicationDatabase())
            {

                var x = context.Database.ExecuteSqlCommand("CREATE TABLE " + TableName + " (ID INTEGER IDENTITY(1,1) PRIMARY KEY, ForeignKey INT, Titre VARCHAR(MAX), Description VARCHAR(MAX))");

            }
        }

        public void RemplirTable(string TableName, int ForeignKey)
        {
            TableName = dashb.TableFormater(dashb.SimpleCotFormater(dashb.FormaterToSQLRequest(TableName)));

            if (TableName != "_Menu")
            {

                using (ApplicationDatabase context = new ApplicationDatabase())
                {
                    var x = context.Database.ExecuteSqlCommand("INSERT INTO " + TableName + " (ForeignKey, Titre, Description) VALUES (" + "'" + ForeignKey + "'" + "," + "'" + dashb.SimpleCotFormater(Title.Text) + "'" + "," + "'" + dashb.SimpleCotFormater(Content.Text) + "'" + ")");
                    Close();
                }

            }
        }

        public void SupprimerTable(string TabName)
        {
            TabName = dashb.TableFormater(dashb.SimpleCotFormater(dashb.FormaterToSQLRequest(TabName)));

            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var x = context.Database.ExecuteSqlCommand("DROP TABLE " + TabName);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            dashb.FenetreOuverte = false;
            Vue.remplirtab();
        }
    }
}
