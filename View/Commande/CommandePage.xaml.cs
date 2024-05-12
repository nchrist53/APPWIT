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
using AWIT.Model;
using AWIT.ViewModel;
using AWIT.View.Client;
using AWIT.View.Tiers;

namespace AWIT.View.Commande
{
    /// <summary>
    /// Logique d'interaction pour CommandePage.xaml
    /// </summary>
    public partial class CommandePage : Page
    {

        CommandeViewModel<ListBox> commandeViewModel = new CommandeViewModel<ListBox>();
        Frame frame = null;
        private List<commande> lesCommandes;
        private commande laCommande;
        public CommandePage(Frame frame, commande uneCommande = null)
        {
            InitializeComponent();
            this.frame = frame;
            this.lesCommandes = commandeViewModel.LesCommandes;

            //Mettre à jour la ListBox
            LbCommandes.ItemsSource = lesCommandes;

            //Envoyer la listBox au ViewModel
            commandeViewModel.LeControl = LbCommandes;

            //Si la page vient d'une action autre que le menu, on a une commande en argument
            if (uneCommande != null)
            {
                this.laCommande = uneCommande;
                int cle = -1;
                foreach (commande item in this.lesCommandes)
                {
                    cle++;
                    if (item.Equals(this.laCommande))
                    {
                        break;
                    }
                }

                //On focus la page sur la commande 
                LbCommandes.SelectedItem = this.lesCommandes[cle];


            }
            else
            {
                //Situation par défaut
                LbCommandes.SelectedItem = this.lesCommandes[0];
                this.laCommande = this.lesCommandes[0];
            }
            SpDetailCommande.DataContext = this.laCommande;
        }

        //Lier la listBox avec Le stackPanel
        private void LbCommandes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.laCommande = LbCommandes.SelectedItem as commande;
            SpDetailCommande.DataContext = this.laCommande;
        }

        //Modification de la commande avec les indications
        private void BtModifier_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                this.laCommande.LOGINCLI = TxbLogin.Text;
                this.laCommande.IDABO = long.Parse(TxbIDABO.Text);
                this.commandeViewModel.ModifierUneCommande(this.laCommande);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Problemo sur la commande!!");
            }


        }

        //Suppression de la commande sélectionnée
        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            this.commandeViewModel.SupprimerCommande(this.laCommande);
        }

        private void BtNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(false);
            SpDetailCommande.DataContext = new commande();
        }

        private void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            this.laCommande = new commande();
            this.laCommande.LOGINCLI = TxbLogin.Text;
            this.laCommande.IDABO = long.Parse(TxbIDABO.Text);

            this.commandeViewModel.AjouterCommande(this.laCommande);
        }

        private void BtAnnulerNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            SpDetailCommande.DataContext = this.lesCommandes[0];
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
