using AWIT.ViewModel;
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

using AWIT.View.Connexion;
using AWIT.View.Client;
using AWIT.View.Commande;
using AWIT.View.Musique;
using AWIT.View.Aide;
using AWIT.Model;

namespace AWIT
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnexionUserControl ucConnexion;
        ClientPage pClient;
        CommandePage pCommande;
        MusiquePage pMusique;
        AidePage pAide = new AidePage();

        public MainWindow()
        {
            InitializeComponent();
            this.pClient = new ClientPage(FSMain);
            pCommande = new CommandePage(FSMain);
            pMusique = new MusiquePage(FSMain);
            ucConnexion = new ConnexionUserControl(this);
            Model.Connexion.GetConnexion();
            if (!Model.Connexion.GetEtat())
            {

                foreach (MenuItem item in MMenu.Items)
                {
                    if (item.Name == "Connexion")
                    {
                        item.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        item.Visibility = Visibility.Hidden;
                    }
                }
                FSMain.Content = ucConnexion;
            }

        }

        public void SeConnecter()
        {
            foreach (MenuItem item in MMenu.Items)
            {
                if (item.Name != "Connexion")
                {
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.Visibility = Visibility.Hidden;
                }
            }
            MinWidth = 1000;
            MinHeight = 800;
            FSMain.Content = pAide;
        }



        private void Client_Click(object sender, RoutedEventArgs e)
        {
            FSMain.Content = pClient;
        }

        private void Commande_Click(object sender, RoutedEventArgs e)
        {
            FSMain.Content = pCommande;
        }

        private void Service_Click(object sender, RoutedEventArgs e)
        {
            FSMain.Content = pMusique;
        }

        private void Aide_Click(object sender, RoutedEventArgs e)
        {
            FSMain.Content = pAide;
        }
    }
}
