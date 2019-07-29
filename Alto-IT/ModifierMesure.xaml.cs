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
    /// Logique d'interaction pour ModifierMesure.xaml
    /// </summary>
    public partial class ModifierMesure : Window
    {
        public List<string> ListeExigenceCheck { get; set; }

        public List<int> ListExigenceCheckId { get; set; }
        public MainWindow mw { get; set; }

        public Vue_Mesures vue { get; set; }

        public ModifierMesure()
        {
            InitializeComponent();
            ListeExigenceCheck = new List<string>();
            ListExigenceCheckId = new List<int>();
        }

        public ModifierMesure(MainWindow m,Vue_Mesures vu)
        {
            InitializeComponent();
            mw = m;
            vue = vu;
            ListeExigenceCheck = new List<string>();
            ListExigenceCheckId = new List<int>();
        }

        private void ModifierMesure_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bouton_AjouterDocument_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<Exigence, bool> item in vue.MesureSelectionne.Dico_ExigenceCheck)
            {
                if (item.Key.FKToProjet == vue.Dash.ProjetEncours.Id)
                {
                    CheckBox C = new CheckBox();
                    C.Content = item.Key.Name;
                    C.IsChecked = item.Value;
                    C.Checked += CheckboxExigences_Checked;
                    C.Unchecked += CheckboxExigence_Unchecked;
                    listviewMesures.Items.Add(C);
                }
                
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            vue.Dash.FenetreOuverte = false;
            vue.RemplirTab();
        }


        private void CheckboxExigences_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox Cb = (CheckBox)sender;
            ListeExigenceCheck.Add(Cb.Content.ToString());


            var query = from i in mw.database.ExigenceDatabase
                        where i.Name == Cb.Content.ToString()
                        select i.Id;

            ListExigenceCheckId.Add(query.FirstOrDefault());

            RelationsMesuresExigences rel = new RelationsMesuresExigences(query.FirstOrDefault(),vue.MesureSelectionne.Id);

            mw.database.RelationMesuresExigenceDatabase.Add(rel);

            var Exisel = from exi in mw.database.ExigenceDatabase
                         where exi.Name == Cb.Content.ToString()
                         select exi;

            vue.MesureSelectionne.Dico_ExigenceCheck[Exisel.FirstOrDefault()] = true;
            Exisel.FirstOrDefault().Dico_MesuresCheck[vue.MesureSelectionne] = true;
            mw.database.SaveChanges();

            
        }

        private void CheckboxExigence_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox Cb = (CheckBox)sender;
            ListeExigenceCheck.Remove(Cb.Content.ToString());

            var query = from i in mw.database.ExigenceDatabase
                        where i.Name == Cb.Content.ToString()
                        select i.Id;

            ListExigenceCheckId.Remove(query.FirstOrDefault());

            var delete = from relation in mw.database.RelationMesuresExigenceDatabase
                         where relation.IdMesures == vue.MesureSelectionne.Id && relation.IdExigence == query.FirstOrDefault()
                         select relation;

            mw.database.RelationMesuresExigenceDatabase.Remove(delete.FirstOrDefault());

            var Exisel = from m in mw.database.ExigenceDatabase
                            where m.Name == Cb.Content.ToString()
                            select m;

            vue.MesureSelectionne.Dico_ExigenceCheck[Exisel.FirstOrDefault()] = false;
            Exisel.FirstOrDefault().Dico_MesuresCheck[vue.MesureSelectionne] = false;
            mw.database.SaveChanges();

        }
    }
}
