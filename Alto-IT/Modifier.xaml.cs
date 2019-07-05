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
            if (Vue.ItemSelectionne != null && Vue.ItemSelectionne.Name != "Menu")
            {
                string CurrentItem = mw.FormaterToSQLRequest(Vue.ItemSelectionne.Name);               


                using (ApplicationDatabase context = new ApplicationDatabase())
                {
                    string newTableName = mw.FormaterToSQLRequest(Title.Text);
                                      

                    //renomme la table
                    var w = context.Database.ExecuteSqlCommand("EXEC sp_rename '" + CurrentItem + "', '" + newTableName + "'");


                    // modif dans sa table
                    var xx = context.Database.ExecuteSqlCommand("UPDATE " + newTableName + " SET Description = '" + Content.Text + "' WHERE Titre = " + "'" + newTableName + "'" + " ");
                    var x = context.Database.ExecuteSqlCommand("UPDATE " + newTableName + " SET Titre = '" + Title.Text + "' WHERE Titre = " + "'" + newTableName + "'" + " ");

                    //modif dans la table Normes
                    var yy = context.Database.ExecuteSqlCommand("UPDATE Normes" + " SET Description = '" + Content.Text + "' WHERE Name = " + "'" + newTableName + "'" + " ");
                    var y = context.Database.ExecuteSqlCommand("UPDATE Normes" + " SET Name = '" + Title.Text + "' WHERE Name = " + "'" + newTableName + "'" + " ");

                    //modif dans table parents
                    var ParentName = context.Database.SqlQuery<string>("SELECT Name from Normes WHERE Id= " + Vue.ItemSelectionne.ForeignKey).FirstOrDefault();
                    if (ParentName != "Menu" && ParentName != null)
                    {
                        ParentName = mw.FormaterToSQLRequest(ParentName);
                        var zz = context.Database.ExecuteSqlCommand("UPDATE " + ParentName + " SET Description = '" + Content.Text + "' WHERE Titre = " + "'" + Vue.ItemSelectionne.Name + "'" + " ");
                        var z = context.Database.ExecuteSqlCommand("UPDATE " + ParentName + " SET Titre = '" + Title.Text + "' WHERE Titre = " + "'" + Vue.ItemSelectionne.Name + "'" + " ");
                    }


                    //actualise l'itemSleceted
                    Vue.ItemSelectionne.Name = Title.Text;
                    Vue.ItemSelectionne.Description = Content.Text;

                    //MajCollection obesrvable
                    // Réaliser avec INotifyPropertyChanges

                    //sauvegarde
                    //mw.database.SaveChanges();
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
