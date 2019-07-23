﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour Choix_du_Provier.xaml
    /// </summary>
    public partial class CreationProjet : Window
    {
        public MainWindow mw { get; set; }
        public CreationProjet()
        {
            InitializeComponent();
        }

        public CreationProjet(MainWindow m)
        {
            InitializeComponent();
            mw = m;
        }

        private void Bouton_validerProjet_Click(object sender, RoutedEventArgs e)
        {
            Dashboard D = new Dashboard(mw);
            D.Show();
            Close();
        }
    }
}
