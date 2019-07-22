using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System;

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

        private void ModifierExigence_Click(object sender, RoutedEventArgs e)
        {
            if (Vue.ExigenceSelectionnee != null && Vue.ExigenceSelectionnee.Name != "Menu")
            {
                string CurrentItem = Vue.dash.FormaterToSQLRequest("_"+Vue.dash.NormeSelectionnee.Id+Vue.ExigenceSelectionnee.Name);
                string CurrentDescription = Vue.dash.SimpleQuoteFormater(Content.Text);
                string CurrentTitle = Vue.dash.SimpleQuoteFormater(Title.Text);

                using (ApplicationDatabase context = new ApplicationDatabase())
                {
                    string newTableName = Vue.dash.TableFormater(Vue.dash.SimpleQuoteFormater(Vue.dash.FormaterToSQLRequest(Title.Text)));

                    try
                    {   
                        //renomme la table
                        var w = context.Database.ExecuteSqlCommand("EXEC sp_rename '" + CurrentItem + "', '" + newTableName + "'");

                        //modif dans la table Exigence
                        var yy = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Description = '" + Vue.dash.SimpleQuoteFormater(Content.Text) + "' WHERE Id = " + "'" + Vue.ExigenceSelectionnee.Id + "'");
                        var y = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Name = '" + Vue.dash.SimpleQuoteFormater(Title.Text) + "' WHERE Id = " + "'" + Vue.ExigenceSelectionnee.Id + "'");

                        try
                        {
                            //modif dans table parents
                            var ParentName = context.Database.SqlQuery<string>("SELECT Name from Exigences WHERE Id= " + Vue.ExigenceSelectionnee.ForeignKey).FirstOrDefault();
                            if (ParentName != "Menu" && ParentName != null)
                            {
                                ParentName = Vue.dash.TableFormater(Vue.dash.SimpleQuoteFormater(Vue.dash.FormaterToSQLRequest(ParentName)));

                                var zz = context.Database.ExecuteSqlCommand("UPDATE " + ParentName + " SET Description = '" + Vue.dash.SimpleQuoteFormater(Content.Text) + "' WHERE Titre = '" + Vue.ExigenceSelectionnee.Name + "'");
                                var z = context.Database.ExecuteSqlCommand("UPDATE " + ParentName + " SET Titre = '" + Vue.dash.SimpleQuoteFormater(Title.Text) + "' WHERE Titre = '" + Vue.ExigenceSelectionnee.Name + "'");
                            }

                            //actualise l'itemSleceted et la Vue grâce INotifyProperty
                            Vue.ExigenceSelectionnee.Name = Title.Text;
                            Vue.ExigenceSelectionnee.Description = Content.Text;
                        }
                        catch (Exception)
                        {
                            var w2 = context.Database.ExecuteSqlCommand("EXEC sp_rename '" + newTableName + "', '" + CurrentItem + "'");
                            var yy2 = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Description = '" + CurrentDescription + "' WHERE Id = " + "'" + Vue.ExigenceSelectionnee.Id + "'");
                            var y2 = context.Database.ExecuteSqlCommand("UPDATE Exigences" + " SET Name = '" + CurrentTitle + "' WHERE Id = " + "'" + Vue.ExigenceSelectionnee.Id + "'");
                            MessageBox.Show("Modification impossible", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Impossible d'actualiser la BDD", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                    switch (ComboBoxStatus.Text)
                    {
                        case "Non Évaluée":
                            Vue.ExigenceSelectionnee.Status = Exigence.STATUS.non_evaluee;
                            break;
                        case "Non Appliquée":
                            Vue.ExigenceSelectionnee.Status = Exigence.STATUS.non_appliquee;
                            break;
                        case "Programmée":
                            Vue.ExigenceSelectionnee.Status = Exigence.STATUS.programmee;
                            break;
                        case "Appliquée":
                            Vue.ExigenceSelectionnee.Status = Exigence.STATUS.appliquee;
                            break;
                        case "Non Applicable":
                            Vue.ExigenceSelectionnee.Status = Exigence.STATUS.non_applicable;
                            break;
                        default:
                            break;
                    }

                    mw.database.SaveChanges();
                    Vue.AfficherDatabase();
                    Close();

                }
            } else
            {
                MessageBox.Show("Selectionner une ligne", "error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

           
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Vue.dash.FenetreOuverte = false;
        }

        private void Bouton_AjouterDocument_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "";
            string fileNameWithoutExtension = "";
            string fileNameWithID = "";
            OpenFileDialog open = new OpenFileDialog();
            open.ShowDialog();
            string[] DocumentSplit = open.SafeFileName.Split('.');

            fileNameWithID = "[" + (Vue.ExigenceSelectionnee.Id) + "]" + open.SafeFileName;
            fileName = open.SafeFileName;
            fileNameWithoutExtension = Vue.dash.AccentFormater(Vue.dash.FormaterToSQLRequest(DocumentSplit[0]));
            string sourcePath = open.FileName;
            string targetPath = @"C:\Users\stagiaire\Desktop\Projet_Stage\Projet_Stage\Alto-IT\bin\Debug\DocumentClient\" + fileNameWithID;

            try
            {
                File.Copy(sourcePath, targetPath);
                Vue.ExigenceSelectionnee.DocumentPath = targetPath;
                Vue.ExigenceSelectionnee.DocumentName = fileName;
                Vue.ExigenceSelectionnee.DocumentWithoutExtension = fileNameWithoutExtension;
            }
            catch (IOException)
            {
                if (MessageBox.Show("Le document existe déjà, voulez-vous le mettre à jour ?", "Fichier existant", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    File.Delete(targetPath);
                    File.Copy(sourcePath, targetPath);
                    Vue.ExigenceSelectionnee.DocumentPath = targetPath;
                    Vue.ExigenceSelectionnee.DocumentName = fileName;
                    Vue.ExigenceSelectionnee.DocumentWithoutExtension = fileNameWithoutExtension;

                }
            }

        }
    }
}
