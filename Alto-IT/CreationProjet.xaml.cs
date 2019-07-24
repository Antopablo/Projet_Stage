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
using System.Windows.Shapes;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour CreationProjet.xaml
    /// </summary>
    public partial class CreationProjet : Window
    {
        public Projets ProjetCree { get; set; }
        public MainWindow mw { get; set; }
        public CreationProjet(MainWindow m)
        {
            InitializeComponent();
            mw = m;
        }

        private void BoutonCreationProjet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (comboboxProvider.Text)
                {
                    case "Azure":
                        ProjetCree = new Projets(nameProjet.Text, PROVIDER.AZURE);
                        mw.database.ProjetsDatabase.Add(ProjetCree);
                        break;
                    case "Amazon Web Service":
                        ProjetCree = new Projets(nameProjet.Text, PROVIDER.AWS);
                        mw.database.ProjetsDatabase.Add(ProjetCree);  
                        break;
                    case "Google Cloud Service":
                        ProjetCree = new Projets(nameProjet.Text, PROVIDER.GOOGLE);
                        mw.database.ProjetsDatabase.Add(ProjetCree);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Création impossible");
            }

            Dashboard D = new Dashboard(mw,ProjetCree);
            D.Show();
            Close();
            mw.database.SaveChanges();
            
        }
    }
}
