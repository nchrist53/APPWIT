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
using AWIT.Model;
using System.IO;
using System.Net.Http;

namespace AWIT.View.Tiers
{
    /// <summary>
    /// Logique d'interaction pour TiersPage.xaml
    /// </summary>
    public partial class TiersPage : Page
    {
        TiersViewModel<ListBox> tiersViewModel = new TiersViewModel<ListBox>();
        Frame frame = null;
        List<abonnement> lesTiers;
        abonnement leTier;

        public TiersPage(Frame frame, abonnement unTier = null)
        {
            InitializeComponent();
            //
            this.frame = frame;
            this.lesTiers = tiersViewModel.LesTiers;
            tiersViewModel.LeControl = LbTiers;


            //
            LbTiers.ItemsSource = this.lesTiers;


            //On teste si un Tier à été envoyé dans la page
            if (unTier != null)
            {
                this.leTier = unTier;
                int cle = -1;
                foreach (abonnement item in this.lesTiers)
                {
                    cle++;
                    if (item.Equals(this.leTier))
                    {
                        break;
                    }
                }
                LbTiers.SelectedItem = this.lesTiers[cle];
                SpDetailTier.DataContext = this.lesTiers[cle];

            }
            else
            {
                SpDetailTier.DataContext = this.lesTiers[0];
            }
        }


        //Lier la listBox avec le stackpanel
        private void LbTiers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            leTier = LbTiers.SelectedItem as abonnement;
            if (leTier == null) leTier = lesTiers[0];
            SpDetailTier.DataContext = leTier;

        }


        //Modifier un Tier avec les renseignements fournis
        private void BtModifier_Click(object sender, RoutedEventArgs e)
        {
            tiersViewModel.ModifierUnTier(leTier);
        }

        //Suppression d'un Tier
        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            tiersViewModel.SupprimerUnTier(leTier);
        }

        //Afficher le conteneur pour ajouter un nouveau Tier
        private void BtNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(false);
            SpDetailTier.DataContext = new abonnement();
        }

        //Ajouter un Tier !! On appelle un tack ici pour les images
        private async void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            //Créer un Tier avec les renseignements fournis
            abonnement unTier = new abonnement();
            unTier.IDABO = long.Parse(TxbID.Text);
            unTier.NOM = TxbNom.Text;
            unTier.DESCRIPTION = TxbDescription.Text;
            unTier.PRIXMENSUEL = Convert.ToDecimal(TxbPrix.Text.Replace(".",","));
            unTier.IMAGE = TxbImage.Text;
            tiersViewModel.AjouterUnTier(unTier);

            Initialiser(true);
        }

        //Revenir en arrière
        private void BtAnnulerNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            SpDetailTier.DataContext = this.lesTiers[0];
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
