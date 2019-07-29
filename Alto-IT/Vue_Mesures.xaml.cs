﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour Vue_Mesures.xaml
    /// </summary>
    public partial class Vue_Mesures : Page
    {
        public Dashboard dashb { get; set; }
        public Mesures ROOT_Mesures { get; set; }
        public Mesures MesureSelectionnee { get; set; }

        readonly object _lockCollection = new object();



        public Vue_Mesures()
        {
            InitializeComponent();
        }

        public Vue_Mesures(Dashboard D)
        {
            InitializeComponent();
            dashb = D;
            dashb.Vue_Mesure = this;
            ROOT_Mesures = new Mesures("Menu");
            treeviewFrame.Items.Add(ROOT_Mesures);
        }

        private void TreeviewFrame_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MesureSelectionnee = (Mesures)treeviewFrame.SelectedItem;
        }


        public void AfficherTreeViewMesure()
        {
            Application.Current.Dispatcher.Invoke(delegate ()
            {
                ROOT_Mesures.MesuresObservCollec.Clear();
            });

            Mesures[] Li = dashb.mw.database.MesuresDatabase.ToArray();
            Mesures[] Lj = Li;
            int[] Ls = new int[Lj.Length];
            int[] lar = new int[Lj.Length];

            for (int i = 0; i < Lj.Length; i++)
            {
                Ls[i] = Lj[i].Id;
            }

           for (int i = 0; i < Li.Length; i++)
           {
                int M = Li[i].Id;
                if ((Li[i].Id == Lj[i].FK_to_Mesures) && (Array.BinarySearch(Ls, M) < 0))
                {
                    lar[i] = M;
                    lock (_lockCollection)
                    {
                        Application.Current.Dispatcher.Invoke(delegate ()
                        {
                            dashb.mw.database.MesuresDatabase.ToList()[i].MesuresObservCollec.Add(dashb.mw.database.MesuresDatabase.ToList()[i]);
                        });
                        Thread.Sleep(2);
                    }
                }
                else if ((Li[i].FK_to_Mesures == 0) && (dashb.ProjetEnCours.Id == Li[i].FK_to_Projets))
                {
                    int MM = Li[i].Id;
                    if (Array.BinarySearch(lar, MM) < 0)
                    {
                        lar[i] = MM;
                        lock (_lockCollection)
                        {
                           Application.Current.Dispatcher.Invoke(delegate ()
                            {
                                ROOT_Mesures.MesuresObservCollec.Add(dashb.mw.database.MesuresDatabase.ToList()[i]);
                            });
                            Thread.Sleep(2);
                        }
                    } 
                }
           }
            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(AfficherTreeViewMesure);
            if (dashb.Vue != null)
            {
                dashb.Vue.afficherMesureAssociee();
            } else
            {
                dashb.Vue = new Vue_Circulaire(dashb);
            }
        }

        private void ExigenceAssocie_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Documentviewer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
