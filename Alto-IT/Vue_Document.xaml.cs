using System;
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
        public string DocumentSelectionne { get; set; }
        public Dashboard Dashb { get; set; }
        public Vue_Document()
        {
            InitializeComponent();
            
        }

        public Vue_Document(Dashboard D)
        {
            InitializeComponent();
            Dashb = D;
            ListViewDoc.ItemsSource = ChargerDocuments();
        }

        private List<string> ChargerDocuments()
        {
            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var Doc = context.Database.SqlQuery<string>("SELECT DocumentName from Exigences WHERE DocumentName IS NOT NULL UNION SELECT DocumentName from Normes WHERE DocumentName IS NOT NULL ").ToList();
                return Doc;
            }
        }

        private void ListViewDoc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DocumentSelectionne = ListViewDoc.SelectedItem.ToString();
            
            String targetPath = @"C:\Users\stagiaire\Desktop\Alto\Projet_Stage\Alto-IT\bin\Debug\Files\" + DocumentSelectionne;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = targetPath;
            process.Start();


        }

        private void ListViewDoc_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
