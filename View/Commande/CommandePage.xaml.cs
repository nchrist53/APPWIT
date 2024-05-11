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
using AWIT.Musique;
using AWIT.View.Client;
using AWIT.View.Musique;

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
        DateTime laDate;
        public CommandePage(Frame frame, commande uneCommande = null)
        {
            InitializeComponent();
            this.frame = frame;
            this.lesCommandes = commandeViewModel.LesCommandes;

            //Mettre à jour la ListBox
            LbCommandes.ItemsSource = lesCommandes;

            //Envoyer la listBox au ViewModel
            commandeViewModel.LeControl = LbCommandes;

            //Le combox des clients
            CbLesClients.ItemsSource = commandeViewModel.GetLesClients();

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
            BtnDetailClient.Visibility = Visibility.Visible;
        }

        //Affiche les informations sur le client sur une nouvelle page
        private void BtnDetailClient_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Content = new ClientPage(this.frame, laCommande.client as client);
        }


        //Nous dirige vers la page ServicePage avec le service sélectionné.
        private void BtnServiceModifier_Click(object sender, RoutedEventArgs e)
        {
            lignecommande uneLignecommande = DgCommandes.SelectedItem as lignecommande;
            service unService = uneLignecommande.service;
            this.frame.Content = new ServicePage(this.frame, unService);

        }


        //Modification de la commande avec les indications
        private void BtModifier_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (CbLesClients.SelectedItem != null)
                {
                    this.laCommande.client = CbLesClients.SelectedItem as client;
                    this.laCommande.LOGINCLI = this.laCommande.LOGINCLI;
                }

                string date = TxbDate.Text.Substring(0, 10);
                //string heure = TxbDate.Text.Substring(11, 8);
                string[] dateTableau = date.Split('/');
                DateTime dateTime = new DateTime(Convert.ToInt16(dateTableau[2]), Convert.ToInt16(dateTableau[1]), Convert.ToInt16(dateTableau[0]));
                this.laDate = dateTime;
                this.laCommande.DATECRE = this.laDate != null ? this.laDate : DateTime.Now;
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


        //Vérification que la date a le bon format JJ/MM/AAAA
        private void TxbDate_LostFocus(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(TxbDate.Text.Substring(0, 10));
            try
            {
                string date = TxbDate.Text.Substring(0, 10);
                //string heure = TxbDate.Text.Substring(11, 8);
                string[] dateTableau = date.Split('/');
                DateTime dateTime = new DateTime(Convert.ToInt16(dateTableau[2]), Convert.ToInt16(dateTableau[1]), Convert.ToInt16(dateTableau[0]));
                this.laDate = dateTime;
            }
            catch
            {
                MessageBox.Show("La date est invalide!!");
                TxbDate.Text = "";
            }

        }

        //Mettre un lien entre le comboxbox client et textbox client
        private void CbLesClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TxbLogin.Text = (CbLesClients.SelectedItem as client).LOGINCLI;
            BtnDetailClient.Visibility = Visibility.Hidden;

        }

        //Un placeholder pour aider l'utilisateur 
        private void TxbDate_GotFocus(object sender, RoutedEventArgs e)
        {
            TxbDate.Text = "JJ/MM/AAAA";
        }
    }
}
