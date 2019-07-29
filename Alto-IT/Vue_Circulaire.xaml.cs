using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour Vue_Circulaire.xaml
    /// </summary>
    public partial class Vue_Circulaire : Page
    {
        public Dashboard dash { get; set; }
        public Exigence ExigenceSelectionne { get; set; }
       public List<Exigence> ListeExigence { get; set; }

        public Exigence ROOT_Exigences { get; set; }

        readonly object _LockCollection = new object();


        public Vue_Circulaire()
        {
            InitializeComponent();
        }

        public Vue_Circulaire(Dashboard D)
        {
            InitializeComponent();
            dash = D;
            
            ROOT_Exigences = new Exigence() { Name = "Menu" }; //modifier le nom entraine un changement dans plusieurs classes  

            treeviewFrame.Items.Add(ROOT_Exigences);
            dash.Vue = this;
            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(AfficherTreeViewExigence);
            await Task.Run(remplirtab);
        }

        public void AfficherTreeViewExigence()
        {
            Application.Current.Dispatcher.Invoke(delegate ()
            {
                ROOT_Exigences.ExigenceObervCollec.Clear();
            });
            Exigence[] Li = dash.mw.database.ExigenceDatabase.ToArray();
            Exigence[] Lj = Li;
            int[] Ls = new int[Lj.Length];
            int[] lar = new int[Lj.Length];
            for (int i = 0; i < Lj.Length; i++)
            {
                Ls[i] = Lj[i].Id;
            }
            for (int i = 0; i < Li.Length; i++)
            {
                int M = Li[i].Id;
                if ((Li[i].Id == Lj[i].ForeignKey) && (Array.BinarySearch(Ls, M) < 0))
                {
                    lar[i] = M;
                    lock (_LockCollection)
                    {
                        Application.Current.Dispatcher.Invoke(delegate ()
                        {
                            dash.mw.database.ExigenceDatabase.ToList()[i].ExigenceObervCollec.Add(dash.mw.database.ExigenceDatabase.ToList()[i]);
                        });
                        Thread.Sleep(2);
                    }
                }
                else if ((Li[i].ForeignKey == 0) && (dash.NormeSelectionnee.Id == Li[i].ForeignKey_TO_Norme))
                {
                    int MM = Li[i].Id;
                    if (Array.BinarySearch(lar, MM) < 0)
                    {
                        lar[i] = MM;
                        lock (_LockCollection)
                        {
                            Application.Current.Dispatcher.Invoke(delegate ()
                            {
                                ROOT_Exigences.ExigenceObervCollec.Add(dash.mw.database.ExigenceDatabase.ToList()[i]);
                            });
                            Thread.Sleep(2);
                        }
                    }
                }
            }
        }

        public void remplirtab()
        {
            List<Exigence> listetem = new List<Exigence>();

            foreach (Exigence item in dash.mw.database.ExigenceDatabase)
            {
                if (item.ForeignKey_TO_Norme == dash.NormeSelectionnee.Id)
                {
                    listetem.Add(item);
                }
            }

            foreach (Exigence exiencours in listetem)
            {

                exiencours.listeRelationMesures.Clear();

                exiencours.Dico_MesuresCheck.Clear();

                var rechercheid = (from idrech in dash.mw.database.RelationMesuresExigenceDatabase
                                    where idrech.IdExigence == exiencours.Id
                                    select idrech.IdMesures).ToList();                                  ////// id des mesures cochées

                foreach (Mesures item in dash.mw.database.MesuresDatabase)
                {
                    if (rechercheid.Contains(item.Id) && !exiencours.Dico_MesuresCheck.ContainsKey(item))
                    {
                        exiencours.Dico_MesuresCheck.Add(item, true);
                        exiencours.listeRelationMesures.Add(item.Nom);
                    }
                    else if (!rechercheid.Contains(item.Id) && !exiencours.Dico_MesuresCheck.ContainsKey(item))
                    {
                        exiencours.Dico_MesuresCheck.Add(item, false);
                    }
                }

            }

        }


        private void TreeviewFrame_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ExigenceSelectionne = (Exigence)treeviewFrame.SelectedItem;
        }


        private void DocumentViewer_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                String filename = ExigenceSelectionne.DocumentPath;
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

        private void Image_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
