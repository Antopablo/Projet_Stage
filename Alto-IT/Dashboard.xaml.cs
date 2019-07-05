﻿using System.Collections.Generic;
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

        private void Ajout_exigence_Click(object sender, RoutedEventArgs e)
        {
            Ajout A = new Ajout(mw, this, (Vue_Circulaire)Frame_Vue_Circulaire.Content);
            A.Show();
        }

        private void Modif_exigence_Click(object sender, RoutedEventArgs e)
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

        private void Supr_exigence_Click(object sender, RoutedEventArgs e)
        {
            if (Vue.ItemSelectionne != null && Vue.ItemSelectionne.Name != "Menu")
            {
                if (MessageBox.Show("Voulez-vous supprimer "+ Vue.ItemSelectionne.Name, "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    string CurrentItem = mw.FormaterToSQLRequest(Vue.ItemSelectionne.Name);
                    Exigence Ntmp = Vue.ItemSelectionne;

                    string NtmpTableName = "";
                    NtmpTableName = mw.FormaterToSQLRequest(Ntmp.Name);

                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        
                        //supprime de la table Exigence son nom
                        var xx = context.Database.ExecuteSqlCommand("DELETE FROM Exigences WHERE Id = '" + Ntmp.Id + "'");

                        
                        //Quand suppression d'un parent => supprimer la table nominative des enfants
                        SuppressionTabEntant(CurrentItem);

                        //supprime de la table parent
                        var ParentName = context.Database.SqlQuery<string>("SELECT Name from Exigences WHERE Id= " + Ntmp.ForeignKey).FirstOrDefault();

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
                    mw.database.ExigenceDatabase.Remove(Ntmp);

                    // remove tous ses enfants de la collection Observable
                    Ntmp.ExigenceObervCollec.Clear();

                    // remove de la liste général dans le treeview
                    Vue.ROOT.ExigenceObervCollec.Remove(Ntmp);
                }
            }
            else
            {
                MessageBox.Show("Selectionner une ligne", "error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Cloud19714_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GridControle_Norme.Visibility = Visibility.Collapsed;


            GridControle_exigence.Visibility = Visibility.Visible;
            Frame_Vue_Circulaire.Visibility = Visibility.Visible;
            Frame_Vue_Circulaire.Content = Vue;
        }

        private void GestionNorme_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GridControle_exigence.Visibility = Visibility.Collapsed;
            Frame_Vue_Circulaire.Visibility = Visibility.Collapsed;

            GridControle_Norme.Visibility = Visibility.Visible;
        }

        public void SuppressionTabEntant(string CurrentItem)
        {
            List<string> ListeGenerale = new List<string>();
            List<string> ListeEnfant = new List<string>();
            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var RequestListEnfant = context.Database.SqlQuery<string>("Select Titre from " + CurrentItem).ToList();
                ListeEnfant = RequestListEnfant;
                foreach (string item in ListeEnfant)
                {
                    ListeGenerale.Add(item);
                    SuppressionTabEntant(mw.FormaterToSQLRequest(item));
                }
                foreach (string item2 in ListeGenerale)
                {
                    var suppenfant = context.Database.ExecuteSqlCommand("DROP TABLE " + mw.FormaterToSQLRequest(item2));
                    var suppenfantTableExigence = context.Database.ExecuteSqlCommand("DELETE FROM Exigences WHERE Name = '" + item2 + "'");
                }
                RequestListEnfant.Clear();
            }
            ListeGenerale.Clear();
            ListeEnfant.Clear();
        }

        private void Ajout_Norme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Modif_Norme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Supr_Norme_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}