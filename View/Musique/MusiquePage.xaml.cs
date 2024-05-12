using AWIT.Model;
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
using AWIT.ViewModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace AWIT.View.Musique
{
    /// <summary>
    /// Logique d'interaction pour MusiquePage.xaml
    /// </summary>
    public partial class MusiquePage : Page
    {
        MusiqueViewModel<ListBox> musiqueViewModel = new MusiqueViewModel<ListBox>();
        Frame frame = null;
        List<musique> lesMusiques;
        musique laMusique;

        public MusiquePage(Frame frame, musique uneMusique = null)
        {
            InitializeComponent();
            //
            this.frame = frame;
            this.lesMusiques = musiqueViewModel.LesMusiques;


            //
            LbMusique.ItemsSource = this.lesMusiques;


            //On teste si un service à été envoyé dans la page
            if (uneMusique != null)
            {
                this.laMusique = uneMusique;
                int cle = -1;
                foreach (musique item in this.lesMusiques)
                {
                    cle++;
                    if (item.Equals(this.laMusique))
                    {
                        break;
                    }
                }
                LbMusique.SelectedItem = this.lesMusiques[cle];
                SpDetailService.DataContext = this.lesMusiques[cle];

            }
            else
            {
                SpDetailService.DataContext = this.lesMusiques[0];
                SpDetailService.DataContext = this.lesMusiques[0];
            }
        }


        //Lier la listBox avec le stackpanel
        private void LbServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            laMusique = LbMusique.SelectedItem as musique;
            if (laMusique == null) laMusique = lesMusiques[0];
            SpDetailService.DataContext = laMusique;
        }


        //Modifier un service avec les renseignements fournis !! on appelle un task ici
        private void BtModifier_Click(object sender, RoutedEventArgs e)
        {

            this.laMusique.TITRE = TxbTitre.Text;
            this.laMusique.PAROLE = TxbResume.Text;
            this.laMusique.SON = TxbDes.Text;
            this.laMusique.IDALBUM = Convert.ToInt16(TxbStock.Text);
        }

        //Suppression d'un service
        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            musiqueViewModel.SupprimerUneMusique(laMusique);
        }

        //Afficher le conteneur pour ajouter un nouveau service
        private void BtNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(false);
            SpDetailService.DataContext = new musique();
        }

        //Ajouter un service !! On appelle un tack ici pour les images
        private void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            //Créer un service avec les resneignements fournis
            this.laMusique = new musique();
            this.laMusique.TITRE = TxbTitre.Text;
            this.laMusique.PAROLE = TxbResume.Text;
            this.laMusique.SON = TxbDes.Text;

            this.laMusique.IDALBUM = Convert.ToInt16(TxbStock.Text);

            musiqueViewModel.AjouterUneMusique(this.laMusique);
        }

        //Revenir en arrière
        private void BtAnnulerNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            SpDetailService.DataContext = this.lesMusiques[0];
        }


        public void Initialiser(bool etat)
        {
            if (etat)
            {
                SpNouveauAjouter.Visibility = Visibility.Hidden;
                SpNouveau.Visibility = Visibility.Visible;
            }
            else
            {
                SpNouveauAjouter.Visibility = Visibility.Visible;
                SpNouveau.Visibility = Visibility.Hidden;
            }
        }
    }
}
