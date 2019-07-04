using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public MainWindow mw { get; set; }
        public Vue_Circulaire Vue { get; set; }

        public Dashboard(MainWindow m)
        {
            InitializeComponent();
            mw = m;
            Vue = new Vue_Circulaire(this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // FullScreen
            //this.Left = SystemParameters.WorkArea.Left;
            //this.Top = SystemParameters.WorkArea.Top;
            //this.Height = SystemParameters.WorkArea.Height;
            //this.Width = SystemParameters.WorkArea.Width;
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


        }

        private void AjoutNorme_Click(object sender, RoutedEventArgs e)
        {
            Ajout A = new Ajout(mw, this, (Vue_Circulaire)Frame_Vue_Circulaire.Content);
            A.Show();
        }

        private void Modif_norme_Click(object sender, RoutedEventArgs e)
        {
            Modifier M = new Modifier(mw, Vue);
            try
            {
                M.Title.Text = Vue.ItemSelectionne.Name;
                M.Content.Text = Vue.ItemSelectionne.Description;
                M.Show();
            }
            catch (System.Exception)
            {
            }
        }

        private void Supr_norme_Click(object sender, RoutedEventArgs e)
        {
            if (Vue.ItemSelectionne != null && Vue.ItemSelectionne.Name != "Menu")
            {
                if (MessageBox.Show("Voulez-vous supprimer", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    string CurrentItem = Vue.ItemSelectionne.Name.Replace(' ', '_');
                    CurrentItem = Vue.ItemSelectionne.Name.Replace("'", "");

                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        var x = context.Database.ExecuteSqlCommand("DROP TABLE " + CurrentItem); // supprime la table à son nom
                        var xx = context.Database.ExecuteSqlCommand("DELETE FROM Normes WHERE Id = '" + Vue.ItemSelectionne.Id + "'"); 
                        mw.database.NormesDatabase.Remove(Vue.ItemSelectionne); // Supprime de la DbSet, à mettre à la fin, reviens à la position 1
                    }

                    Vue.ItemSelectionne.NormeObervCollec.Clear(); // remove tous ses enfants
                    Vue.ROOT.NormeObervCollec.Remove(Vue.ItemSelectionne); // le remove de la liste général dans le treeview
                }
            } else
            {
                MessageBox.Show("Selectionner une ligne", "error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Cloud19714_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GridControle.Visibility = Visibility.Visible;
            Frame_Vue_Circulaire.Visibility = Visibility.Visible;
            Frame_Vue_Circulaire.Content = Vue;
        }
    }
}