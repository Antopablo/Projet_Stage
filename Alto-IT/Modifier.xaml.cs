using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour Modifier.xaml
    /// </summary>
    public partial class Modifier : Window
    {
        public MainWindow mw { get; set; }
        public Vue_Circulaire Vue { get; set; }

        public List<string> ListedeMesuresChecked { get; set; }

        public Modifier()
        {
            InitializeComponent();
            ListedeMesuresChecked = new List<string>();
        }

        public Modifier(MainWindow m, Vue_Circulaire v)
        {
            InitializeComponent();
            mw = m;
            Vue = v;
            listeboxMesures.ItemsSource = mw.database.MesuresDatabase.Local;
            mw.database.MesuresDatabase.ToList();

            ListedeMesuresChecked = new List<string>();

        }
        
        private void ModifierExigence_Click(object sender, RoutedEventArgs e)
        {
            if (Vue.ExigenceSelectionne != null && Vue.ExigenceSelectionne.Name != "Menu")
            {
                string CurrentItem = Vue.dash.FormaterToSQLRequest("_"+Vue.dash.NormeSelectionnee.Id+Vue.ExigenceSelectionne.Name);
                string CurrentTitle = Vue.dash.SimpleCotFormater(Vue.ExigenceSelectionne.Name);
                string CurrentDesc = Vue.dash.SimpleCotFormater(Vue.ExigenceSelectionne.Description);
                


                using (ApplicationDatabase context = new ApplicationDatabase())
                {
                    string newTableName = Vue.dash.TableFormater(Vue.dash.SimpleCotFormater(Vue.dash.FormaterToSQLRequest(Title.Text)));

                    try
                    {
                        //renomme la table
                        var w = context.Database.ExecuteSqlCommand("EXEC sp_rename '" + CurrentItem + "', '" + newTableName + "'");

                        //modif dans la table Exigence

                        var yy = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Description = '" + Vue.dash.SimpleCotFormater(Content.Text) + "' WHERE Id = " + "'" + Vue.ExigenceSelectionne.Id + "'" + " ");
                        var y = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Name = '" + Vue.dash.SimpleCotFormater(Title.Text) + "' WHERE Id = " + "'" + Vue.ExigenceSelectionne.Id + "'" + " ");
                        try
                        {
                            //modif dans table parents
                            var ParentName = context.Database.SqlQuery<string>("SELECT Name from Exigences WHERE Id= " + Vue.ExigenceSelectionne.ForeignKey).FirstOrDefault();
                            if (ParentName != "Menu" && ParentName != null)
                            {
                                ParentName = Vue.dash.TableFormater(Vue.dash.SimpleCotFormater(Vue.dash.FormaterToSQLRequest(ParentName)));
                                var zz = context.Database.ExecuteSqlCommand("UPDATE " + ParentName + " SET Description = '" + Vue.dash.SimpleCotFormater(Content.Text) + "' WHERE Titre = '" + Vue.ExigenceSelectionne.Name + "'");
                                var z = context.Database.ExecuteSqlCommand("UPDATE " + ParentName + " SET Titre = '" + Vue.dash.SimpleCotFormater(Title.Text) + "' WHERE Titre = '" + Vue.ExigenceSelectionne.Name + "'");
                                
                            }
                            Vue.ExigenceSelectionne.Name = Title.Text;
                            Vue.ExigenceSelectionne.Description = Content.Text;
                        }
                        catch (Exception)
                        {
                            var ww = context.Database.ExecuteSqlCommand("EXEC sp_rename '" + newTableName + "', '" + CurrentItem + "'");

                            var yty = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Description = '" + CurrentDesc + "' WHERE Id = " + "'" + Vue.ExigenceSelectionne.Id + "'" + " ");
                            var yt = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Name = '" + CurrentTitle + "' WHERE Id = " + "'" + Vue.ExigenceSelectionne.Id + "'" + " ");
                            MessageBox.Show("Impossible d ajouter à la table Parent", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        

                        

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Impossible de Modifier la table", "erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                    }


                    mw.database.SaveChanges();
                    Vue.AfficherTreeViewExigence();
                    Close();

                }

                ////////   Traitement des relations entre Exigences et Mesures
                


               
            }
            else
            {
                MessageBox.Show("Selectionner une ligne", "error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Vue.dash.FenetreOuverte = false;
        }

        private void AjoutDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.ShowDialog();
            string filename = "(" + Vue.ExigenceSelectionne.Id + ")" + open.SafeFileName;
            string targetPath = @"C:\Users\stagiaire\Desktop\Alto\Projet_Stage\Alto-IT\bin\Debug\Files\" + filename;
            try
            {
                
                File.Copy(open.FileName, targetPath);
                Vue.ExigenceSelectionne.DocumentPath = targetPath;
                Vue.ExigenceSelectionne.DocumentName = filename;
            }
            catch (System.Exception)
            {
                if (MessageBox.Show("Ce fichier éxiste déja voulez vous le supprimer ?", "Erreur", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        File.Delete(targetPath);
                        File.Copy(open.FileName, targetPath);
                        Vue.ExigenceSelectionne.DocumentPath = targetPath;
                        Vue.ExigenceSelectionne.DocumentName = filename;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Impossible de supprimer");
                    }
                }
            }
            MessageBox.Show("Document bien enregistré");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckboxMesures_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox Cb = (CheckBox)sender;
            ListedeMesuresChecked.Add(Cb.Content.ToString());
        }

        private void CheckboxMesures_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox Cb = (CheckBox)sender;
            ListedeMesuresChecked.Remove(Cb.Content.ToString());
        }
    }
}
