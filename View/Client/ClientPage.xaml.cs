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
using System.ComponentModel;
using AWIT.View.Commande;
using AWIT.ViewModel.ClientViewModel;


namespace AWIT.View.Client
{
    /// <summary>
    /// Logique d'interaction pour ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        ClientViewModel<ListBox> clientViewModel = new ClientViewModel<ListBox>();
        private List<client> lesClients;
        private client leClient;
        private Frame frame;

        //Getter et Setter


        public ClientPage(Frame frame, client unClient = null)
        {
            InitializeComponent();
            this.frame = frame;
            this.lesClients = clientViewModel.LesClients;
            LbClients.ItemsSource = this.lesClients;

            clientViewModel.LeControl = LbClients;

            if (unClient != null)
            {
                this.leClient = unClient;
                int cle = -1;
                foreach (client item in this.lesClients)
                {
                    cle++;
                    if (item.Equals(this.leClient))
                    {
                        break;
                    }
                }
                this.LbClients.SelectedItem = this.lesClients[cle];
                SpDetailClient.DataContext = this.leClient;
            }
            else
            {
                LbClients.SelectedItem = this.lesClients[0];
                SpDetailClient.DataContext = this.lesClients[0];
            }


        }


        private void LbClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            leClient = LbClients.SelectedItem as client;
            SpNouveauAjouter.Visibility = Visibility.Hidden;
            SpNouveau.Visibility = Visibility.Visible;
            SpDetailClient.DataContext = LbClients.SelectedItem;



        }

        private void BtModifier_Click(object sender, RoutedEventArgs e)
        {
            // client unClient = LbClients.SelectedItem as client;

            clientViewModel.ModifierUnClient(leClient);
        }

        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            clientViewModel.SupprimerUnClient(leClient);
        }

        private void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            client unClient = new client
            {
                NOMCLI = TxbNom.Text,
                PRENOMCLI = TxbPrenom.Text,
                LOGINCLI = TxbLogin.Text,
            };
            clientViewModel.AjouterUnClient(unClient);

            Initialiser(true);

        }


        private void BtAnnulerNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            LbClients.SelectedItem = this.lesClients[0];
            SpDetailClient.DataContext = this.lesClients[0];
        }

        private void BtNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(false);
            SpDetailClient.DataContext = new client();

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

        private void BtnCommandeModifier_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Content = new CommandePage(this.frame, DgCommandes.SelectedItem as commande);
        }

    }
}
