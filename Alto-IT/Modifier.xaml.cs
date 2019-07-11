using System.Linq;
using System.Text;
using System.Windows;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour Modifier.xaml
    /// </summary>
    public partial class Modifier : Window
    {
        public MainWindow mw { get; set; }
        public Vue_Circulaire Vue { get; set; }

        public Modifier()
        {
            InitializeComponent();
        }

        public Modifier(MainWindow m, Vue_Circulaire v)
        {
            InitializeComponent();
            mw = m;
            Vue = v;
        }

        private void ModifierNorme_Click(object sender, RoutedEventArgs e)
        {
            if (Vue.ExigenceSelectionnee != null && Vue.ExigenceSelectionnee.Name != "Menu")
            {
                string CurrentItem = mw.FormaterToSQLRequest(Vue.ExigenceSelectionnee.Name);               


                using (ApplicationDatabase context = new ApplicationDatabase())
                {
                    string newTableName = mw.FormaterToSQLRequest(Title.Text);
                                      

                    //renomme la table
                    var w = context.Database.ExecuteSqlCommand("EXEC sp_rename '" + CurrentItem + "', '" + newTableName + "'");


                    //modif dans la table Exigence

                    var yy = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Description = ' " + mw.SimpleQuoteFormater(Content.Text) + " ' WHERE Id = " + "'" + Vue.ExigenceSelectionnee.Id + "'");
                    var y = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Name = ' " + mw.SimpleQuoteFormater(Title.Text) + " ' WHERE Id = " + "'" + Vue.ExigenceSelectionnee.Id + "'");

                    //modif dans table parents
                    var ParentName = context.Database.SqlQuery<string>("SELECT Name from Exigences WHERE Id= " + Vue.ExigenceSelectionnee.ForeignKey).FirstOrDefault();
                    if (ParentName != "Menu" && ParentName != null)
                    {
                        ParentName = mw.FormaterToSQLRequest(ParentName);
                        var zz = context.Database.ExecuteSqlCommand("UPDATE " + ParentName + " SET Description = '" + mw.SimpleQuoteFormater(Content.Text) + "' WHERE ForeignKey = " + "'" + Vue.ExigenceSelectionnee.Id + "'");
                        var z = context.Database.ExecuteSqlCommand("UPDATE " + ParentName + " SET Titre = '" + mw.SimpleQuoteFormater(Title.Text) + "' WHERE ForeignKey = " + "'" + Vue.ExigenceSelectionnee.Id + "'");
                    }


                    //actualise l'itemSleceted
                    Vue.ExigenceSelectionnee.Name = Title.Text;
                    Vue.ExigenceSelectionnee.Description = Content.Text;

                    //MajCollection obesrvable
                    // Réaliser avec INotifyPropertyChanges

                    Vue.AfficherDatabase();
                    Close();

                }
            } else
            {
                MessageBox.Show("Selectionner une ligne", "error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
