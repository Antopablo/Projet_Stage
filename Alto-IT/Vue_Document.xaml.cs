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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour Vue_Document.xaml
    /// </summary>
    public partial class Vue_Document : Page
    {
        public Dashboard dashb { get; set; }
        public string DocumentSelectionne { get; set; }
        public Vue_Document()
        {
            InitializeComponent();
        }

        public Vue_Document(Dashboard D)
        {
            InitializeComponent();
            dashb = D;
            ListeViewDocumentation.ItemsSource = ChargerDocuments().OrderBy(x => x);
        }

        public List<string> ChargerDocuments ()
        {
            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var SelectAllDoc = context.Database.SqlQuery<string>("SELECT DocumentName FROM Exigences WHERE DocumentName IS NOT NULL UNION SELECT DocumentName FROM Normes WHERE DocumentName IS NOT NULL").ToList();
                return SelectAllDoc;
            }
        }

        private void ListeViewDocumentation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string DocumentFullPath = "";
            try
            {
                DocumentSelectionne = ListeViewDocumentation.SelectedItem.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("Selectionnez un document");
            }
            string[] DocumentSplit = DocumentSelectionne.Split('.');
            string F = dashb.AccentFormater(dashb.FormaterToSQLRequest(DocumentSplit[0]));

            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var Doc = context.Database.SqlQuery<string>("SELECT DocumentPath FROM Exigences WHERE DocumentWithoutExtension = '" + F + "'").FirstOrDefault();
                if (Doc == null)
                {
                    Doc = context.Database.SqlQuery<string>("SELECT DocumentPath FROM Normes WHERE DocumentWithoutExtension = '" + F + "'").FirstOrDefault();

                }
                DocumentFullPath = Doc;
            }

            try
            {
                String fileName = DocumentFullPath;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = fileName;
                process.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Document introuvable");
            }
        }
    }
}
