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
        ServiceViewModel<ListBox> serviceViewModel = new ServiceViewModel<ListBox>();
        Frame frame = null;
        List<service> lesServices;
        service leService;
        //le nom de l'image
        string lienImage;
        const string SERVURL = "http://localhost:8000/upload";
        const string URI = "http://localhost:8000/asset/img/";
        BitmapImage bitmapImage;
        string imagePath;

        public ServicePage(Frame frame, service unService = null)
        {
            InitializeComponent();
            //
            this.frame = frame;
            this.lesServices = serviceViewModel.LesServices;


            //
            LbServices.ItemsSource = this.lesServices;
            CbCat.ItemsSource = serviceViewModel.LesCategories;
            this.serviceViewModel.LeControl = LbServices;


            //On teste si un service à été envoyé dans la page
            if (unService != null)
            {
                this.leService = unService;
                int cle = -1;
                foreach (service item in this.lesServices)
                {
                    cle++;
                    if (item.Equals(this.leService))
                    {
                        break;
                    }
                }
                LbServices.SelectedItem = this.lesServices[cle];
                SpDetailService.DataContext = this.lesServices[cle];
                CbCat.SelectedItem = this.lesServices[cle].categorie;

            }
            else
            {
                SpDetailService.DataContext = this.lesServices[0];
                SpDetailService.DataContext = this.lesServices[0];
                CbCat.SelectedItem = this.lesServices[0].categorie;
            }
        }


        //Lier la listBox avec le stackpanel
        private void LbServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            leService = LbServices.SelectedItem as service;
            if (leService == null) leService = lesServices[0];
            SpDetailService.DataContext = leService;

            CbCat.SelectedItem = leService.categorie;
            BitmapImage bitmapImage = new BitmapImage(new Uri($"{URI}{leService.PHOTO}"));
            ImgService.Source = bitmapImage;
        }


        //Modifier un service avec les renseignements fournis !! on appelle un task ici
        private async void BtModifier_Click(object sender, RoutedEventArgs e)
        {

            this.leService.TITRE = TxbTitre.Text;
            this.leService.RESUME = TxbResume.Text;
            this.leService.DESCRIPTION = TxbDes.Text;
            this.leService.PHOTO = this.lienImage;
            this.leService.STOCK = Convert.ToInt16(TxbStock.Text);
            this.leService.PRIX = Convert.ToDecimal(TxbPrix.Text.Replace('.', ','));
            this.leService.CAT = (CbCat.SelectedItem as categorie).NUMERO;
            this.leService.categorie = (CbCat.SelectedItem as categorie);
            if (this.bitmapImage != null)
            {
                await UploadImageAsync(SERVURL, this.imagePath);
                this.leService.PHOTO = this.lienImage;
                this.serviceViewModel.ModifierUnService(this.leService);
            }


        }

        //Suppression d'un service
        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            serviceViewModel.SupprimerUnService(leService);
        }

        //Afficher le conteneur pour ajouter un nouveau service
        private void BtNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(false);
            SpDetailService.DataContext = new service();
        }

        //Ajouter un service !! On appelle un tack ici pour les images
        private async void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            //Créer un service avec les resneignements fournis
            this.leService = new service();
            this.leService.TITRE = TxbTitre.Text;
            this.leService.RESUME = TxbResume.Text;
            this.leService.DESCRIPTION = TxbDes.Text;

            this.leService.STOCK = Convert.ToInt16(TxbStock.Text);
            this.leService.PRIX = Convert.ToDecimal(TxbPrix.Text.Replace('.', ','));
            this.leService.CAT = (CbCat.SelectedItem as categorie).NUMERO;
            this.leService.categorie = (CbCat.SelectedItem as categorie);
            if (this.bitmapImage != null)
            {
                await UploadImageAsync(SERVURL, this.imagePath);
                this.leService.PHOTO = this.lienImage;
                this.serviceViewModel.AjouterUnService(this.leService);
            }


        }

        //Revenir en arrière
        private void BtAnnulerNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            SpDetailService.DataContext = this.lesServices[0];
            CbCat.SelectedItem = this.lesServices[0].categorie;
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

        private void BtnTelecharger_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Fichiers d'images|*.jpg;*.jpeg;*.png;*.gif|Tous les fichiers|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Obtenez le chemin du fichier sélectionné
                this.imagePath = openFileDialog.FileName;

                // afficher l'image dans une image control
                this.bitmapImage = new BitmapImage(new Uri(imagePath));
                ImgService.Source = bitmapImage;
                //this.lienImage = openFileDialog.SafeFileName;
            }
        }

        async Task UploadImageAsync(string serverUrl, string imagePath)
        {
            using (HttpClient client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                // Ajoutez le fichier à la requête HTTP
                content.Add(new StreamContent(fileStream), "file", System.IO.Path.GetFileName(imagePath));

                // Envoyez la requête HTTP POST
                HttpResponseMessage response = await client.PostAsync(serverUrl, content);

                // Vérifiez si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    this.lienImage = response.Headers.GetValues("image").FirstOrDefault();
                    MessageBox.Show("Image téléchargée avec succès !");
                }
                else
                {
                    MessageBox.Show($"Erreur lors du téléchargement de l'image : {response.StatusCode}");
                }
            }
        }
    }
}
