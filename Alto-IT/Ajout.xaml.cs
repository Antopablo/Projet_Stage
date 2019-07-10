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
    /// Logique d'interaction pour Ajout.xaml
    /// </summary>
    public partial class Ajout : Window
    {
        MainWindow mw;
        Dashboard dashb;
        Vue_Circulaire Vue;
        public Ajout(MainWindow m, Dashboard d, Vue_Circulaire vc)
        {
            InitializeComponent();
            mw = m;
            dashb = d;
            Vue = vc;
        }

        private void ValiderNorme_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "Menu")
            {
                MessageBox.Show("Vous ne pouvez pas appeler une norme ainsi", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            } else
            {
                if (Vue.ExigenceSelectionnee == null)
                {
                    CreateTable(Title.Text);
                    Exigence ExigenceParent = new Exigence(Title.Text, Content.Text, 0, dashb.NormeSelectionnee.Id);
                    Vue.ROOT_Exigences.ExigenceObervCollec.Add(ExigenceParent);
                    mw.database.ExigenceDatabase.Add(ExigenceParent);
                    mw.database.SaveChanges();
                }
                else
                {
                    CreateTable(Title.Text);
                    RemplirTable(Vue.ExigenceSelectionnee.Name, Vue.ExigenceSelectionnee.Id);
                    Exigence ExigenceEnfant = new Exigence(Title.Text, Content.Text, Vue.ExigenceSelectionnee.Id, dashb.NormeSelectionnee.Id);
                    Vue.ExigenceSelectionnee.ExigenceObervCollec.Add(ExigenceEnfant);
                    mw.database.ExigenceDatabase.Add(ExigenceEnfant);
                    try
                    {
                        mw.database.SaveChanges();
                    }
                    catch (Exception msg)
                    {
                        MessageBox.Show("saveChanges AJOUT KO");
                        MessageBox.Show(msg.Message);
                    }
                }
            }
            Close();
        }

        public void CreateTable(string TableName)
        {
            TableName = mw.FormaterToSQLRequest(TableName);

            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                try
                {
                    var x = context.Database.ExecuteSqlCommand("CREATE TABLE " + TableName + " (ID INTEGER IDENTITY(1,1) PRIMARY KEY, ForeignKey INT, Titre VARCHAR(1000), Description VARCHAR(1000))");
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Table non crée", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void RemplirTable(string TableName, int ForeignKey)
        {
            TableName = mw.FormaterToSQLRequest(TableName);

            if (TableName != "Menu")
            {
                try
                {
                    using (ApplicationDatabase context = new ApplicationDatabase())
                    {
                        var x = context.Database.ExecuteSqlCommand("INSERT INTO " + TableName + " (ForeignKey, Titre, Description) VALUES (" + "'" + ForeignKey + "'" + "," + "'" + Title.Text + "'" + "," + "'" + Content.Text + "'" + ")");
                        Close();
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
