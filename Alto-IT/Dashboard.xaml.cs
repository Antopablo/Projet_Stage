using System.Collections.Generic;
using System.Linq;
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
            this.Left = SystemParameters.WorkArea.Left;
            this.Top = SystemParameters.WorkArea.Top;
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width;
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
            try
            {
                if (Vue.ItemSelectionne.Name != "Menu")
                {
                    Modifier M = new Modifier(mw, Vue);
                    M.Title.Text = Vue.ItemSelectionne.Name;
                    M.Content.Text = Vue.ItemSelectionne.Description;
                    M.Show();

                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Selectionner une norme à modifier", "error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Supr_norme_Click(object sender, RoutedEventArgs e)
        {
            if (Vue.ItemSelectionne != null && Vue.ItemSelectionne.Name != "Menu")
            {
                if (MessageBox.Show("Voulez-vous supprimer", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    string CurrentItem = mw.FormaterToSQLRequest(Vue.ItemSelectionne.Name);
                    Norme Ntmp = Vue.ItemSelectionne;

                    string NtmpTableName = "";
                    NtmpTableName = mw.FormaterToSQLRequest(Ntmp.Name);

                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        
                        //supprime de Normes
                        var xx = context.Database.ExecuteSqlCommand("DELETE FROM Normes WHERE Id = '" + Ntmp.Id + "'");

                        //Quand suppression d'un parent => supprimer la table nominative des enfants
                        SuppressionTabEntant(CurrentItem);

                        //supprime de la table parent
                        var ParentName = context.Database.SqlQuery<string>("SELECT Name from Normes WHERE Id= " + Ntmp.ForeignKey).FirstOrDefault();

                        var ListeEnfant = context.Database.SqlQuery<string>("SELECT * FROM " + Ntmp);

                        if (ParentName != "Menu" && ParentName != null)
                        {
                            ParentName = mw.FormaterToSQLRequest(ParentName);
                            var zz = context.Database.ExecuteSqlCommand("DELETE FROM " + ParentName + " WHERE Titre = "+"'" + Ntmp.Name +"'");
                        }

                        // supprime la table à son nom
                        var x = context.Database.ExecuteSqlCommand("DROP TABLE " + CurrentItem);

                        
                    }

                    // Supprime de la DbSet, à mettre à la fin, reviens à la position 1
                    mw.database.NormesDatabase.Remove(Ntmp);

                    // remove tous ses enfants de la collection Observable
                    Ntmp.NormeObervCollec.Clear();

                    // remove de la liste général dans le treeview
                    Vue.ROOT.NormeObervCollec.Remove(Ntmp);
                }
            }
            else
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

        public void SuppressionTabEntant(string CurrentItem)
        {
            List<string> ListeGenerale2 = new List<string>();
            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var ListeEnfant = context.Database.SqlQuery<string>("Select Titre from " + CurrentItem).ToList();
                foreach (string item in ListeEnfant)
                {
                    ListeGenerale2.Add(item);
                    SuppressionTabEntant(mw.FormaterToSQLRequest(item));
                }
                foreach (string item2 in ListeGenerale2)
                {
                    var suppenfant = context.Database.ExecuteSqlCommand("DROP TABLE " + mw.FormaterToSQLRequest(item2));
                }
            }
        }
    }
}