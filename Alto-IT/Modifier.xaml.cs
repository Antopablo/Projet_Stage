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
            string CurrentItem = Vue.ItemSelectionne.Name.Replace(' ', '_');
            

            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var x = context.Database.ExecuteSqlCommand("UPDATE " + CurrentItem + " SET Titre = '" + Title.Text + "' WHERE Titre = " + "'" + CurrentItem + "'" + " ");
                var xx = context.Database.ExecuteSqlCommand("UPDATE " + CurrentItem + " SET Description = '" + Content.Text + "' WHERE Titre = " + "'" + CurrentItem + "'" + " ");
                var y = context.Database.ExecuteSqlCommand("UPDATE Normes" + " SET Name = '" + Title.Text + "' WHERE Name = " + "'" + CurrentItem + "'" + " ");
                var yy = context.Database.ExecuteSqlCommand("UPDATE Normes" + " SET Description = '" + Content.Text + "' WHERE Name = " + "'" + CurrentItem + "'" + " ");
                var z = context.Database.ExecuteSqlCommand("EXEC sp_rename '" + CurrentItem + "', '" + Title.Text + "'");
                Close();
            }
        }
    }
}
