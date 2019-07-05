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

        private void ValiderNorme_Click(object sender, RoutedEventArgs e)
        {
            


            if (Vue.ItemSelectionne == null)
            {
                CreateTable(Title.Text);
                Exigence ExigenceParent = new Exigence(Title.Text,Content.Text, 0);
                Vue.ROOT.ExigenceObervCollec.Add(ExigenceParent);
                mw.database.ExigenceDatabase.Add(ExigenceParent);
                mw.database.SaveChanges();

            } else
            {
                CreateTable(Title.Text);
                RemplirTable(Vue.ItemSelectionne.Name, Vue.ItemSelectionne.Id);
                Exigence ExigenceEnfant = new Exigence(Title.Text, Content.Text, Vue.ItemSelectionne.Id);
                Vue.ItemSelectionne.ExigenceObervCollec.Add(ExigenceEnfant);
                mw.database.ExigenceDatabase.Add(ExigenceEnfant);
                try
                {
                    mw.database.SaveChanges();
                }
                catch (Exception)
                {
                }

            }
            Close();
        }

        public void CreateTable(string TableName)
        {
            TableName = mw.FormaterToSQLRequest(TableName);

            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                try
                {
                    var x = context.Database.ExecuteSqlCommand("CREATE TABLE " + TableName + " (ID INTEGER IDENTITY(1,1) PRIMARY KEY, ForeignKey INT, Titre VARCHAR(1000), Description VARCHAR(1000))");
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Table non crée", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void RemplirTable(string TableName, int ForeignKey)
        {
            TableName = mw.FormaterToSQLRequest(TableName);

            if (TableName != "Menu")
            {
                try
                {
                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        var x = context.Database.ExecuteSqlCommand("INSERT INTO " + TableName + " (ForeignKey, Titre, Description) VALUES (" + "'" + ForeignKey + "'" + "," + "'" + Title.Text + "'" + "," + "'" + Content.Text + "'" + ")");
                        Close();
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
