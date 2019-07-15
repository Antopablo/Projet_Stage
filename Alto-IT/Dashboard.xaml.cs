using System.Collections.Generic;
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
        public MainWindow mw { get; set; }
        public Vue_Circulaire Vue { get; set; }
        public Norme ROOT_Normes { get; set; }
        public Norme NormeSelectionnee { get; set; }
        public bool FenetreOuverte { get; set; }


        public Dashboard(MainWindow m)
        {
            InitializeComponent();
            mw = m;
            Vue = new Vue_Circulaire(this);

            ROOT_Normes = new Norme("Normes");
            TreeViewNORME.Items.Add(ROOT_Normes);
            AfficherLesNormes();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // FullScreen
            this.Left = SystemParameters.WorkArea.Left;
            this.Top = SystemParameters.WorkArea.Top;
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width;
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
                    if (Vue.ExigenceSelectionnee.Name != "Menu")
                    {
                        Modifier M = new Modifier(mw, Vue);
                        M.Title.Text = Vue.ExigenceSelectionnee.Name;
                        M.Content.Text = Vue.ExigenceSelectionnee.Description;
                        M.Status.Text = Vue.ExigenceSelectionnee.Status.ToString();
                        M.Document.Text = Vue.ExigenceSelectionnee.DocumentName;
                        M.Show();
                        FenetreOuverte = true;
                    }
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Selectionner une norme à modifier", "error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
        }

        private void Supr_exigence_Click(object sender, RoutedEventArgs e)
        {

            if (Vue.ExigenceSelectionnee != null && Vue.ExigenceSelectionnee.Name != "Menu")
            {
                if (MessageBox.Show("Voulez-vous supprimer "+ Vue.ExigenceSelectionnee.Name, "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    string CurrentItem = TableFormater(SimpleQuoteFormater(FormaterToSQLRequest(Vue.ExigenceSelectionnee.Name)));
                    
                    Exigence Ntmp = Vue.ExigenceSelectionnee;


                    // Supprime de la DbSet, à mettre en 1er
                    mw.database.ExigenceDatabase.Remove(Ntmp);
                    mw.database.SaveChanges();


                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        
                        //Quand suppression d'un parent => supprimer la table nominative des enfants
                        SuppressionTabEntant(CurrentItem);

                        //supprime de la table parent
                        var ParentName = context.Database.SqlQuery<string>("SELECT Name from Exigences WHERE Id= " + Ntmp.ForeignKey).FirstOrDefault();

                        var ListeEnfant = context.Database.SqlQuery<string>("SELECT * FROM " + Ntmp);

                        if (ParentName != "Menu" && ParentName != null)
                        {
                            ParentName = TableFormater(FormaterToSQLRequest(ParentName));
                            var zz = context.Database.ExecuteSqlCommand("DELETE FROM " + ParentName + " WHERE Titre = '" + SimpleQuoteFormater(Ntmp.Name) +"'");
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
                    var suppenfantTableExigence = context.Database.ExecuteSqlCommand("DELETE FROM Exigences WHERE Name = '" + SimpleQuoteFormater(item2) + "'");

                    string tmp2 = "";
                    tmp2 = item2;
                    var suppenfant = context.Database.ExecuteSqlCommand("DROP TABLE " + TableFormater(SimpleQuoteFormater(FormaterToSQLRequest(tmp2))));
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
                AF.Show();
                FenetreOuverte = true;
            }
        }

        private void TreeViewNORME_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
           
            if (TreeViewNORME.SelectedItem.ToString() == "Normes")
            {
                GridControle_Norme.Visibility = Visibility.Visible;
                GridControle_exigence.Visibility = Visibility.Collapsed;
                Frame_Vue_Circulaire.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                GridControle_Norme.Visibility = Visibility.Collapsed;
                GridControle_exigence.Visibility = Visibility.Visible;
                Frame_Vue_Circulaire.Visibility = Visibility.Visible;
                Frame_Vue_Circulaire.Content = new Vue_Circulaire(this);
            }

            NormeSelectionnee = (Norme)TreeViewNORME.SelectedItem;
        }

        public void AfficherLesNormes()
        {
            foreach (Norme item in mw.database.NormeDatabase)
            {
                ROOT_Normes.NormeObervCollec.Add(item);
            }
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
            Text = Text.Replace('{', '_');
            Text = Text.Replace('}', '_');
            Text = Text.Replace('^', '_');
            Text = Text.Replace('$', '_');
            Text = Text.Replace('+', '_');
            Text = Text.Replace('-', '_'); 
            Text = Text.Replace('=', '_'); 
            Text = Text.Replace('£', '_');
            Text = Text.Replace('?', '_');
            Text = Text.Replace('!', '_');
            Text = Text.Replace(',', '_');
            Text = Text.Replace('<', '_');
            Text = Text.Replace('>', '_');
            Text = Text.Replace('§', '_');
            Text = Text.Replace('%', '_');
            Text = Text.Replace("\"", "");
            Text = Text.Replace("[", "_");
            Text = Text.Replace("]", "_");
            Text = Text.Replace(".", "_");
            Text = Text.Replace("-", "_");
            Text = Text.Replace("|", "_");
            Text = Text.Replace("&", "_");
            Text = Text.Replace("`", "_");
            Text = Text.Replace("#", "_");
            Text = Text.Replace("°", "_");
            Text.Trim();
            return Text;
        }

        public string TableFormater(string Text)
        {
            StringBuilder builder = new StringBuilder(Text);
            return Text = builder.Insert(0, "_"+NormeSelectionnee.Id).ToString();
        }

        public string SimpleQuoteFormater(string text)
        {
            return text.Replace("'", "''");
        }
    }
}