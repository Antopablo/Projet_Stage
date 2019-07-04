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
                string CurrentItem = Vue.ItemSelectionne.Name;
                CurrentItem = CurrentItem.Replace(' ', '_');
                CurrentItem = CurrentItem.Replace(' ', '_');
                CurrentItem = CurrentItem.Replace('/', '_');
                CurrentItem = CurrentItem.Replace("'", "");
                CurrentItem = CurrentItem.Replace("[", "_");
                CurrentItem = CurrentItem.Replace("]", "_");
                CurrentItem = CurrentItem.Replace(".", "_");


                using (ApplicationDatabase context = new ApplicationDatabase())
                {
                    string newTableName = Title.Text;
                    StringBuilder builder = new StringBuilder(newTableName);
                    newTableName = builder.Insert(0, "_").ToString();
                    newTableName = newTableName.Replace(' ', '_');
                    newTableName = newTableName.Replace('/', '_');
                    newTableName = newTableName.Replace("'", "");
                    newTableName = newTableName.Replace("[", "_");
                    newTableName = newTableName.Replace("]", "_");
                    newTableName = newTableName.Replace(".", "_");
                    newTableName.Trim();

                    //renomme la table
                    var z = context.Database.ExecuteSqlCommand("EXEC sp_rename '" + CurrentItem + "', '" + newTableName + "'");


                    // modif dans sa table
                    var xx = context.Database.ExecuteSqlCommand("UPDATE " + newTableName + " SET Description = '" + Content.Text + "' WHERE Titre = " + "'" + newTableName + "'" + " ");
                    var x = context.Database.ExecuteSqlCommand("UPDATE " + newTableName + " SET Titre = '" + Title.Text + "' WHERE Titre = " + "'" + newTableName + "'" + " ");

                    //modif dans la table Normes
                    var yy = context.Database.ExecuteSqlCommand("UPDATE Normes" + " SET Description = '" + Content.Text + "' WHERE Name = " + "'" + newTableName + "'" + " ");
                    var y = context.Database.ExecuteSqlCommand("UPDATE Normes" + " SET Name = '" + Title.Text + "' WHERE Name = " + "'" + newTableName + "'" + " ");


                    //actualise l'itemSleceted
                    Vue.ItemSelectionne.Name = Title.Text;
                    Vue.ItemSelectionne.Description = Content.Text;

                    //MajCollection obesrvable
                    // Réaliser avec INotifyPropertyChanges

                    //sauvegarde et modifie la vue dans le treeView
                    mw.database.SaveChanges();
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
