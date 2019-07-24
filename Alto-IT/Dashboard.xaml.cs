using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Projets ProjetEncours { get; set; }
        public MainWindow mw { get; set; }
        public Vue_Circulaire Vue { get; set; }
        public Norme ROOT_Normes { get; set; }

        public Vue_Mesures Vue_Mesure { get; set; }

        public Mesures ROOT_Mesure { get; set; }
        public Norme NormeSelectionnee { get; set; }

        

        public bool FenetreOuverte { get; set; }


        public Dashboard(MainWindow m, Projets Pj)
        {
            InitializeComponent();
            mw = m;
            ProjetEncours = Pj;
            //Vue = new Vue_Circulaire(this);

            
            ROOT_Normes = new Norme("Normes",ProjetEncours.Id);
            ROOT_Mesure = new Mesures("Mesures");
            TreeViewNORME.Items.Add(ROOT_Normes);
            TreeViewMesures.Items.Add(ROOT_Mesure);

            AfficherLesNormes();
            AfficherLesMesures();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // FullScreen
            this.Left = SystemParameters.WorkArea.Left;
            this.Top = SystemParameters.WorkArea.Top;
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width;

            Title = "Dashboard - Projet : "+ ProjetEncours.Nom;

        }

        private void Ajout_exigence_Click(object sender, RoutedEventArgs e)
        {
            if (FenetreOuverte == false)
            {
                Ajout A = new Ajout(mw, this, (Vue_Circulaire)Frame_Vue_Circulaire.Content);
                A.Show();
                FenetreOuverte = true;
            }
            
        }

        private void Modif_exigence_Click(object sender, RoutedEventArgs e)
        {
            if (FenetreOuverte == false)
            {
                try
                {
                    if (Vue.ExigenceSelectionne.Name != "Menu")
                    {
                        Modifier M = new Modifier(mw, Vue);
                        M.Title.Text = Vue.ExigenceSelectionne.Name;
                        M.Content.Text = Vue.ExigenceSelectionne.Description;
                        M.statut.Text = Vue.ExigenceSelectionne.Status.ToString();
                        M.DocName.Text = Vue.ExigenceSelectionne.DocumentName;
                        M.Show();
                        FenetreOuverte = true;
                    }
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Selectionner une éxigence à modifier", "error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
        }

        private void Supr_exigence_Click(object sender, RoutedEventArgs e)
        {
            if (Vue.ExigenceSelectionne != null && Vue.ExigenceSelectionne.Name != "Menu")
            {
                if (MessageBox.Show("Voulez-vous supprimer "+ Vue.ExigenceSelectionne.Name, "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    string CurrentItem = TableFormater(SimpleCotFormater(FormaterToSQLRequest(Vue.ExigenceSelectionne.Name)));
                    
                    Exigence Ntmp = Vue.ExigenceSelectionne;


                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        var pathFile = context.Database.SqlQuery<string>(
                                           "SELECT DocumentPath FROM Exigences WHERE Id= " + Ntmp.Id ).FirstOrDefault();

                        if (pathFile != null)
                        {
                            File.Delete(pathFile);
                        }
                    }

                    //string NtmpTableName = "";
                    //NtmpTableName = mw.SimpleCotFormater(mw.FormaterToSQLRequest(Ntmp.Name));
                    //StringBuilder builder2 = new StringBuilder(NtmpTableName);
                    //NtmpTableName = builder.Insert(1, NormeSelectionnee.Id).ToString();

                    // Supprime de la DbSet, à mettre à la fin, reviens à la position 1
                    mw.database.ExigenceDatabase.Remove(Ntmp);
                    mw.database.SaveChanges();


                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        
                        //supprime de la table Exigence son nom
                        //var xx = context.Database.ExecuteSqlCommand("DELETE FROM Exigences WHERE Id = '" + Ntmp.Id + "'");

                        
                        //Quand suppression d'un parent => supprimer la table nominative des enfants
                        SuppressionTabEntant(CurrentItem);

                        //supprime de la table parent
                        var ParentName = context.Database.SqlQuery<string>("SELECT Name from Exigences WHERE Id= " + Ntmp.ForeignKey).FirstOrDefault();

                        var ListeEnfant = context.Database.SqlQuery<string>("SELECT * FROM " + Ntmp);

                        if (ParentName != "Menu" && ParentName != null)
                        {
                            ParentName = TableFormater(FormaterToSQLRequest(ParentName));
                            var zz = context.Database.ExecuteSqlCommand("DELETE FROM " + ParentName + " WHERE Titre = "+"'" + SimpleCotFormater(Ntmp.Name) +"'");
                        }

                        // supprime la table à son nom
                        var x = context.Database.ExecuteSqlCommand("DROP TABLE " + CurrentItem);

                    }

                    
                    // remove tous ses enfants de la collection Observable
                    Ntmp.ExigenceObervCollec.Clear();

                    // remove de la liste général dans le treeview
                    Vue.ROOT_Exigences.ExigenceObervCollec.Remove(Ntmp);
                }
            }
            else
            {
                MessageBox.Show("Selectionner une ligne", "error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            mw.database.SaveChanges();
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
                    string tmp = "";
                    ListeGenerale.Add(item);
                    tmp = item;
                    SuppressionTabEntant(TableFormater(FormaterToSQLRequest(tmp)));
                }
                foreach (string item2 in ListeGenerale)
                {
                    var pathFile = context.Database.SqlQuery<string>(
                                           "SELECT DocumentPath FROM Exigences WHERE Name = '" + SimpleCotFormater(item2) + "'").FirstOrDefault();
                    if (pathFile != null)
                    {
                        File.Delete(pathFile);
                    }
                    

                    var suppenfantTableExigence = context.Database.ExecuteSqlCommand("DELETE FROM Exigences WHERE Name = '" + SimpleCotFormater(item2) + "'");

                    string tmp2 = "";
                    tmp2 = item2;
                    var suppenfant = context.Database.ExecuteSqlCommand("DROP TABLE " + TableFormater(SimpleCotFormater(FormaterToSQLRequest(tmp2))));
                }
                RequestListEnfant.Clear();
            }
            ListeGenerale.Clear();
            ListeEnfant.Clear();
        }

        private void Ajout_Norme_Click(object sender, RoutedEventArgs e)
        {
            if (FenetreOuverte == false)
            {
                AjoutNorme AJ = new AjoutNorme(mw, this, Vue);
                AJ.Show();
                FenetreOuverte = true;
            }
            
        }

        private void Modif_Norme_Click(object sender, RoutedEventArgs e)
        {
            if (FenetreOuverte == false)
            {
                AffichageDesNormes AF = new AffichageDesNormes(mw, this);
                AF.BoutonValiderModify.Visibility = Visibility.Visible;
                AF.BoutonSupprimer.Visibility = Visibility.Hidden;
                AF.TitreModify.Visibility = Visibility.Visible;
                AF.TitreModifyBlock.Visibility = Visibility.Hidden;
                AF.AjoutDocument.Visibility = Visibility.Visible;
                AF.Show();
                FenetreOuverte = true;
            }
            
        }

        private void Supr_Norme_Click(object sender, RoutedEventArgs e)
        {
            if (FenetreOuverte == false)
            {
                AffichageDesNormes AF = new AffichageDesNormes(mw, this);
                AF.BoutonValiderModify.Visibility = Visibility.Hidden;
                AF.BoutonSupprimer.Visibility = Visibility.Visible;
                AF.TitreModify.Visibility = Visibility.Hidden;
                AF.TitreModifyBlock.Visibility = Visibility.Visible;
                AF.AjoutDocument.Visibility = Visibility.Hidden;
                AF.Show();
                FenetreOuverte = true;
            }
            
        }

        private void TreeViewNORME_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Frame_VueDocuments.Visibility = Visibility.Collapsed;
            ////  cacher la vue circulaire du la page mesure.
            if (TreeViewNORME.SelectedItem.ToString() == "Normes")
            {
                
                GridControle_Norme.Visibility = Visibility.Visible;
                GridControle_exigence.Visibility = Visibility.Collapsed;
                Frame_Vue_Circulaire.Visibility = Visibility.Collapsed;
            } else
            {
                GridControle_Norme.Visibility = Visibility.Collapsed;
                GridControle_exigence.Visibility = Visibility.Visible;
                Frame_Vue_Circulaire.Visibility = Visibility.Visible;
                Frame_Vue_Circulaire.Content = new Vue_Circulaire(this);
                //Vue = (Vue_Circulaire)Frame_Vue_Circulaire.Content;
            }

            

        }

        public void AfficherLesNormes()
        {
            foreach (Norme item in mw.database.NormeDatabase)
            {
                if (item.FKToProjet == ProjetEncours.Id)
                {
                    ROOT_Normes.NormeObervCollec.Add(item);
                }

            }
        }

        public void AfficherLesMesures()
        {
            foreach (Mesures item in mw.database.MesuresDatabase)
            {
                if (item.Nom == "Mesure")
                {
                    ROOT_Mesure.MesureObservableCollec.Add(item);
                }
                
            }
        }

        public string TableFormater(string text)
        {
            StringBuilder builder = new StringBuilder(text);
            return text = builder.Insert(0, "_" + NormeSelectionnee.Id) .ToString();
        }

        public string TableFormaterMesures(string text)
        {
            StringBuilder builder = new StringBuilder(text);
            return text = builder.Insert(0, "_" + ProjetEncours.Id).ToString();
        }

        public string FormaterToSQLRequest(string Text)
        {
            Text = Text.Replace(' ', '_');
            Text = Text.Replace("'", "");
            Text = Text.Replace('/', '_');
            Text = Text.Replace('\\', '_');
            Text = Text.Replace('*', '_');
            Text = Text.Replace(';', '_');
            Text = Text.Replace(':', '_');
            Text = Text.Replace(',', '_');
            Text = Text.Replace('|', '_');
            Text = Text.Replace('&', '_');
            Text = Text.Replace('`', '_');
            Text = Text.Replace('(', '_');
            Text = Text.Replace(')', '$');
            Text = Text.Replace('#', '_');
            Text = Text.Replace('{', '$');
            Text = Text.Replace('}', '_');
            Text = Text.Replace('^', '_');
            Text = Text.Replace('+', '_');
            Text = Text.Replace('=', '$');
            Text = Text.Replace('-', '_');
            Text = Text.Replace('£', '_');
            Text = Text.Replace('?', '_');
            Text = Text.Replace('!', '_');
            Text = Text.Replace('<', '_');
            Text = Text.Replace('>', '_');
            Text = Text.Replace('§', '_');
            Text = Text.Replace('%', '_');
            Text = Text.Replace("\"", "");
            Text = Text.Replace("[", "_");
            Text = Text.Replace("]", "$");
            Text = Text.Replace(".", "");
            Text = Text.Replace("-", "_");
            Text.Trim();
            return Text;
        }

        public string SimpleCotFormater(string text)
        {
            return text.Replace("'", "''");
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                String filename = NormeSelectionnee.DocumentPath;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = filename;
                process.Start();
            }
            catch (NullReferenceException)
            {

                MessageBox.Show("Veuillez sélectionner une éxigence");
            }
            catch (InvalidOperationException)
            {

                MessageBox.Show("Aucun document associé");
            }
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Vue_Document VueDoc = new Vue_Document(this);
            Frame_Vue_Circulaire.Visibility = Visibility.Collapsed;
            Frame_VueDocuments.Visibility = Visibility.Visible;
            Frame_VueMesures.Visibility = Visibility.Collapsed;
            Frame_VueDocuments.Content = VueDoc;

            GridControle_exigence.Visibility = Visibility.Collapsed;
            GridControle_Mesures.Visibility = Visibility.Collapsed;
            GridControle_Norme.Visibility = Visibility.Collapsed;
        }


        private void TreeViewNORME_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                NormeSelectionnee = (Norme)TreeViewNORME.SelectedItem;
                if (NormeSelectionnee.Nom_Norme == "Normes")
                {
                    GridControle_exigence.Visibility = Visibility.Collapsed;
                    GridControle_Norme.Visibility = Visibility.Visible;
                    GridControle_Mesures.Visibility = Visibility.Collapsed;


                    Frame_VueDocuments.Visibility = Visibility.Collapsed;
                    Frame_VueMesures.Visibility = Visibility.Collapsed;
                    Frame_Vue_Circulaire.Visibility = Visibility.Collapsed;
                }
                else
                {
                    GridControle_exigence.Visibility = Visibility.Visible;
                    GridControle_Norme.Visibility = Visibility.Collapsed;
                    GridControle_Mesures.Visibility = Visibility.Collapsed;

                    Frame_VueDocuments.Visibility = Visibility.Collapsed;
                    Frame_VueMesures.Visibility = Visibility.Collapsed;
                    Frame_Vue_Circulaire.Content = new Vue_Circulaire(this);
                    Frame_Vue_Circulaire.Visibility = Visibility.Visible;

                }
            }
            catch (Exception)
            {
                
            }
            
        }
        private void TreeViewMesures_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Vue_Mesure.MesureSelectionne = (Mesures)TreeViewMesures.SelectedItem;
            GridControle_exigence.Visibility = Visibility.Collapsed;
            GridControle_Norme.Visibility = Visibility.Collapsed;
            GridControle_Mesures.Visibility = Visibility.Visible;


            Frame_Vue_Circulaire.Visibility = Visibility.Collapsed;
            Frame_VueDocuments.Visibility = Visibility.Collapsed;
            Frame_VueMesures.Content = new Vue_Mesures(this);
            Frame_VueMesures.Visibility = Visibility.Visible;
            
        }

        private void Ajout_Mesures_Click(object sender, RoutedEventArgs e)
        {
            AjoutMesures AJ = new AjoutMesures(mw,this);
            AJ.Show();
            FenetreOuverte = true;
        }

        private void Modif_Mesures_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Supr_Mesures_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}