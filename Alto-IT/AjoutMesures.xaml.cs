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
    /// Logique d'interaction pour AjoutMesures.xaml
    /// </summary>
    public partial class AjoutMesures : Window
    {
        public MainWindow mw { get; set; }

        public Dashboard Dash { get; set; }
        public AjoutMesures(MainWindow m, Dashboard D)
        {
            InitializeComponent();
            mw = m;
            Dash = D;
        }

        private void ValiderMesure_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Dash.ProjetEncours.Id+"");
            if (MesureName.Text == "Menu")
            {
                MessageBox.Show("Vous ne pouvez pas appeler une Mesure ainsi", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (Dash.Vue_Mesure.MesureSelectionne == null || Dash.Vue_Mesure.MesureSelectionne.Nom == "Menu")
                {
                    try
                    {
                        CreateTable(MesureName.Text);
                        Mesures MesureParent = new Mesures(MesureName.Text, MesureDescription.Text, 0, Dash.ProjetEncours.Id);
                        Dash.Vue_Mesure.ROOT_Mesures.MesureObservableCollec.Add(MesureParent);
                        mw.database.MesuresDatabase.Add(MesureParent);
                        mw.database.SaveChanges();
                        Close();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {

                        MessageBox.Show("Une mesure porte déja ce nom", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                else
                {
                    try
                    {
                        CreateTable(MesureName.Text);
                        try
                        {
                            RemplirTable(Dash.Vue_Mesure.MesureSelectionne.Nom, Dash.Vue_Mesure.MesureSelectionne.Id);
                            Mesures MesureEnfant = new Mesures(MesureName.Text, MesureDescription.Text, Dash.Vue_Mesure.MesureSelectionne.Id, Dash.ProjetEncours.Id);
                            Dash.Vue_Mesure.MesureSelectionne.MesureObservableCollec.Add(MesureEnfant);
                            mw.database.MesuresDatabase.Add(MesureEnfant);
                        }
                        catch (Exception)
                        {

                            SupprimerTable(MesureName.Text);
                            MessageBox.Show("Impossible de remplir la Table Parent", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        Close();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {

                        MessageBox.Show("Une mesure porte déja ce nom", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    mw.database.SaveChanges();
                }
            }
        }



        public void CreateTable(string TableName)
        {
            TableName = Dash.TableFormaterMesures(Dash.SimpleCotFormater(Dash.FormaterToSQLRequest(TableName)));

            using (ApplicationDatabase context = new ApplicationDatabase())
            {

                var x = context.Database.ExecuteSqlCommand("CREATE TABLE " + TableName + " (ID INTEGER IDENTITY(1,1) PRIMARY KEY, ForeignKey INT, Titre VARCHAR(MAX), Description VARCHAR(MAX))");

            }
        }


        public void RemplirTable(string TableName, int ForeignKey)
        {
            TableName = Dash.TableFormaterMesures(Dash.SimpleCotFormater(Dash.FormaterToSQLRequest(TableName)));

            if (TableName != "_Menu")
            {

                using (ApplicationDatabase context = new ApplicationDatabase())
                {
                    var x = context.Database.ExecuteSqlCommand("INSERT INTO " + TableName + " (ForeignKey, Titre, Description) VALUES (" + "'" + ForeignKey + "'" + "," + "'" + Dash.SimpleCotFormater(MesureName.Text) + "'" + "," + "'" + Dash.SimpleCotFormater(MesureDescription.Text) + "'" + ")");
                    Close();
                }

            }
        }

        public void SupprimerTable(string TabName)
        {
            TabName = Dash.TableFormaterMesures(Dash.SimpleCotFormater(Dash.FormaterToSQLRequest(TabName)));

            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var x = context.Database.ExecuteSqlCommand("DROP TABLE " + TabName);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Dash.FenetreOuverte = false;
        }
    }
}
