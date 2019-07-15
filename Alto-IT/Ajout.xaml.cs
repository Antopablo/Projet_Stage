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
            if (Title.Text == "Menu")
            {
                MessageBox.Show("Vous ne pouvez pas appeler une exigence ainsi", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            } else
            {
                if (Vue.ExigenceSelectionnee == null || Vue.ExigenceSelectionnee.Name == "Menu")
                {
                    CreateTable(Title.Text);
                    Exigence ExigenceParent = new Exigence(Title.Text, Content.Text, 0, dashb.NormeSelectionnee.Id);
                    Vue.ROOT_Exigences.ExigenceObervCollec.Add(ExigenceParent);
                    mw.database.ExigenceDatabase.Add(ExigenceParent);
                    mw.database.SaveChanges();
                }
                else
                {
                    CreateTable(Title.Text);
                    RemplirTable(Vue.ExigenceSelectionnee.Name, Vue.ExigenceSelectionnee.Id);
                    Exigence ExigenceEnfant = new Exigence(Title.Text, Content.Text, Vue.ExigenceSelectionnee.Id, dashb.NormeSelectionnee.Id);
                    Vue.ExigenceSelectionnee.ExigenceObervCollec.Add(ExigenceEnfant);
                    mw.database.ExigenceDatabase.Add(ExigenceEnfant);
                    try
                    {
                        mw.database.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sauvegarde BDD impossible", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            dashb.FenetreOuverte = false;
            Close();
        }

        public void CreateTable(string TableName)
        {
            TableName = dashb.TableFormater(dashb.SimpleQuoteFormater(dashb.FormaterToSQLRequest(TableName)));
            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                try
                {
                    var x = context.Database.ExecuteSqlCommand("CREATE TABLE " + TableName + " (ID INTEGER IDENTITY(1,1) PRIMARY KEY, ForeignKey INT, Titre VARCHAR(1000), Description VARCHAR(1000))");
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Impossible de créer la table dans la BDD", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void RemplirTable(string TableName, int ForeignKey)
        {
            TableName = dashb.TableFormater(dashb.SimpleQuoteFormater(dashb.FormaterToSQLRequest(TableName)));

            if (TableName != "_Menu")
            {
                try
                {
                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        var x = context.Database.ExecuteSqlCommand("INSERT INTO " + TableName + " (ForeignKey, Titre, Description) VALUES (" + "'" + ForeignKey + "'" + "," + "'" + dashb.SimpleQuoteFormater(Title.Text) + "'" + "," + "'" + dashb.SimpleQuoteFormater(Content.Text) + "'" + ")");
                        Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Impossible d'ajouter à la table parent", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            dashb.FenetreOuverte = false;
        }
    }
}
