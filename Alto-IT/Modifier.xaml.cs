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

        public List<int> ListedeMesuresCheckedId { get; set; }

        public Modifier()
        {
            InitializeComponent();
            ListedeMesuresChecked = new List<string>();
            ListedeMesuresCheckedId = new List<int>();
        }

        public Modifier(MainWindow m, Vue_Circulaire v)
        {
            InitializeComponent();
            mw = m;
            Vue = v;
            //listeboxMesures.ItemsSource = mw.database.MesuresDatabase.Local;
            mw.database.MesuresDatabase.ToList();

            ListedeMesuresChecked = new List<string>();
            ListedeMesuresCheckedId = new List<int>();
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

                            ////////   Traitement des relations entre Exigences et Mesures
                            




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
            var rechercheid = from idrech in mw.database.RelationMesuresExigenceDatabase
                              where idrech.IdExigence == Vue.ExigenceSelectionne.Id
                              select idrech.IdMesures;


            foreach (Mesures item in mw.database.MesuresDatabase)
            {
                if (rechercheid.ToList().Contains(item.Id) && !Vue.ExigenceSelectionne.Dico_MesuresCheck.ContainsKey(item))
                {
                    Vue.ExigenceSelectionne.Dico_MesuresCheck.Add(item, true);
                }
                else if (!rechercheid.ToList().Contains(item.Id) &&!Vue.ExigenceSelectionne.Dico_MesuresCheck.ContainsKey(item))
                {
                    Vue.ExigenceSelectionne.Dico_MesuresCheck.Add(item, false);
                }
            }

            foreach (KeyValuePair<Mesures,bool> item in Vue.ExigenceSelectionne.Dico_MesuresCheck )
            {
                CheckBox C = new CheckBox();
                C.Content = item.Key.Nom;
                C.IsChecked = item.Value;
                C.Checked += CheckboxMesures_Checked;
                C.Unchecked += CheckboxMesures_Unchecked;
                listviewMesures.Items.Add(C);
            }
        }

        private void CheckboxMesures_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox Cb = (CheckBox)sender;
            ListedeMesuresChecked.Add(Cb.Content.ToString());

            var query = from i in mw.database.MesuresDatabase
                        where i.Nom == Cb.Content.ToString()
                        select i.Id;

            ListedeMesuresCheckedId.Add(query.FirstOrDefault());

            RelationsMesuresExigences rel = new RelationsMesuresExigences(Vue.ExigenceSelectionne.Id, query.FirstOrDefault());
            mw.database.RelationMesuresExigenceDatabase.Add(rel);


            var Mesuresel = from m in mw.database.MesuresDatabase
                            where m.Nom == Cb.Content.ToString()
                            select m;



            Vue.ExigenceSelectionne.Dico_MesuresCheck[Mesuresel.FirstOrDefault()] = true;
            Mesuresel.FirstOrDefault().Dico_ExigenceCheck[Vue.ExigenceSelectionne] = true;
            mw.database.SaveChanges();
        }

        private void CheckboxMesures_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox Cb = (CheckBox)sender;
            ListedeMesuresChecked.Remove(Cb.Content.ToString());

            var query = from i in mw.database.MesuresDatabase
                        where i.Nom == Cb.Content.ToString()
                        select i.Id;

            ListedeMesuresCheckedId.Remove(query.FirstOrDefault());

            var delete = from relation in mw.database.RelationMesuresExigenceDatabase
                         where relation.IdExigence == Vue.ExigenceSelectionne.Id && relation.IdMesures == query.FirstOrDefault()
                         select relation;
            
            mw.database.RelationMesuresExigenceDatabase.Remove(delete.FirstOrDefault());

            var Mesuresel = from m in mw.database.MesuresDatabase
                            where m.Nom == Cb.Content.ToString()
                            select m;

            Vue.ExigenceSelectionne.Dico_MesuresCheck[Mesuresel.FirstOrDefault()] = false;
            Mesuresel.FirstOrDefault().Dico_ExigenceCheck[Vue.ExigenceSelectionne] = false;
            mw.database.SaveChanges();

        }
    }
}
