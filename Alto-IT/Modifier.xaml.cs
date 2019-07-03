using System.Linq;
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
            if (Vue.ItemSelectionne != null)
            {
                string CurrentItem = Vue.ItemSelectionne.Name.Replace(' ', '_');


                using (ApplicationDatabase context = new ApplicationDatabase())
                {
                    string newTableName = Title.Text.Replace(' ', '_');
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
