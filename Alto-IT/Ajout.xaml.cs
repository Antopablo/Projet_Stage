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
            Norme N = new Norme(Title.Text, Content.Text);
            mw.database.NormesDatabase.Add(N);
            mw.database.SaveChanges();

            CreateTable(Title.Text);
            if (Vue.ItemSelectionne != null)
            {
                RemplirTable(Title.Text, Vue.ItemSelectionne.Id);
            } else
            {
                RemplirTable(Title.Text, 0);
            }
        }

        public void CreateTable(string TableName)
        {
            TableName = TableName.Replace(" ", "_");
            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                try
                {
                    var x = context.Database.ExecuteSqlCommand("CREATE TABLE " + TableName + " (ID INTEGER IDENTITY(1,1) PRIMARY KEY, ForeignKey INT, Titre VARCHAR(1000), Description VARCHAR(1000))");
                }
                catch (System.Data.SqlClient.SqlException)
                {
                }
            }
        }

        public void RemplirTable(string TableName, int ForeignKey)
        {
            TableName = TableName.Replace(" ", "_");
                using (ApplicationDatabase context = new ApplicationDatabase())                                                     
                {
                    var x = context.Database.ExecuteSqlCommand("INSERT INTO " + TableName + " (ForeignKey, Titre, Description) VALUES (" + "'" + ForeignKey + "'" +","+ "'" + Title.Text + "'" + "," + "'" + Content.Text + "'" + ")");
                    Close();
                }
        }
    }
}
