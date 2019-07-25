using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System;

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AfficherTreeViewExigence();
        }

        public void AfficherTreeViewExigence()
        {
            //dash.mw.database.ExigenceDatabase.ToList();
            
            //for (int i = 0; i < dash.mw.database.ExigenceDatabase.ToList().Count(); i++)
            //{
            //    for (int j = 0; j < dash.mw.database.ExigenceDatabase.ToList().Count(); j++)
            //    {
            //        if (dash.mw.database.ExigenceDatabase.ToList()[i].Id == dash.mw.database.ExigenceDatabase.ToList()[j].ForeignKey)
            //        {
            //            if (!dash.mw.database.ExigenceDatabase.ToList().Contains(dash.mw.database.ExigenceDatabase.ToList()[i]))
            //            {
            //                dash.mw.database.ExigenceDatabase.ToList()[i].ExigenceObervCollec.Add(dash.mw.database.ExigenceDatabase.ToList()[j]);
            //            }
            //        }
            //        else if(dash.mw.database.ExigenceDatabase.ToList()[i].ForeignKey == 0 && dash.NormeSelectionnee.Id == dash.mw.database.ExigenceDatabase.ToList()[i].ForeignKey_TO_Norme)
            //        {
            //            if (!ROOT_Exigences.ExigenceObervCollec.ToList().Contains(dash.mw.database.ExigenceDatabase.ToList()[i]))
            //            {
            //                ROOT_Exigences.ExigenceObervCollec.Add(dash.mw.database.ExigenceDatabase.ToList()[i]);
            //            }
            //        }
                    
            //    }
            //}

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
                    lock (ROOT_Exigences.ExigenceObervCollec)
                    {
                        dash.mw.database.ExigenceDatabase.ToList()[i].ExigenceObervCollec.Add(dash.mw.database.ExigenceDatabase.ToList()[i]);
                    }
                }
                else if ((Li[i].ForeignKey == 0) && (dash.NormeSelectionnee.Id == Li[i].ForeignKey_TO_Norme))
                {
                    int MM = Li[i].Id;
                    if (Array.BinarySearch(lar, MM) < 0)
                    {
                        lar[i] = MM;
                        lock (ROOT_Exigences.ExigenceObervCollec)
                        {
                            ROOT_Exigences.ExigenceObervCollec.Add(dash.mw.database.ExigenceDatabase.ToList()[i]);
                        }
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
