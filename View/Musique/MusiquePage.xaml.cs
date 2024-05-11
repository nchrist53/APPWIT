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
        MusiqueViewModel<ListBox> MusiqueViewModel = new MusiqueViewModel<ListBox>();
        Frame frame = null;
        List<musique> lesMusiques;
        musique laMusique;
        //le nom de l'image
        string lienImage;
        const string SERVURL = "http://localhost:8000/upload";
        const string URI = "http://localhost:8000/asset/img/";
        BitmapImage bitmapImage;
        string imagePath;

        public MusiquePage(Frame frame, musique uneMusique = null)
        {
            InitializeComponent();
            //
            this.frame = frame;
            this.lesMusiques = MusiqueViewModel.LesMusiques;


            //
            LbServices.ItemsSource = this.lesMusiques;
            CbCat.ItemsSource = MusiqueViewModel.LesMusiques;
            this.MusiqueViewModel.LeControl = LbMusique;


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
                LbServices.SelectedItem = this.lesMusiques[cle];
                SpDetailService.DataContext = this.lesMusiques[cle];
                CbCat.SelectedItem = this.lesMusiques[cle].auteurs;

            }
            else
            {
                SpDetailService.DataContext = this.lesMusiques[0];
                SpDetailService.DataContext = this.lesMusiques[0];
                CbCat.SelectedItem = this.lesMusiques[0].auteurs;
            }
        }


        //Lier la listBox avec le stackpanel
        private void LbServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            laMusique = LbServices.SelectedItem as musique;
            if (laMusique == null) laMusique = lesMusiques[0];
            SpDetailService.DataContext = laMusique;

            CbCat.SelectedItem = laMusique.auteurs;
        }


        //Modifier un service avec les renseignements fournis !! on appelle un task ici
        private async void BtModifier_Click(object sender, RoutedEventArgs e)
        {

            this.laMusique.TITRE = TxbTitre.Text;
            this.laMusique.PAROLE = TxbResume.Text;
            this.laMusique.SON = TxbDes.Text;
            this.laMusique.IDALBUM = Convert.ToInt16(TxbStock.Text);
            this.laMusique.auteurs = (CbCat.SelectedItem as auteur);
        }

        //Suppression d'un service
        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            MusiqueViewModel.SupprimerUnService(laMusique);
        }

        //Afficher le conteneur pour ajouter un nouveau service
        private void BtNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(false);
            SpDetailService.DataContext = new musique();
        }

        //Ajouter un service !! On appelle un tack ici pour les images
        private async void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            //Créer un service avec les resneignements fournis
            this.laMusique = new musique();
            this.laMusique.TITRE = TxbTitre.Text;
            this.laMusique.PAROLE = TxbResume.Text;
            this.laMusique.SON = TxbDes.Text;

            this.laMusique.IDALBUM = Convert.ToInt16(TxbStock.Text);

            this.laMusique.auteurs = (CbCat.SelectedItem as auteur).IDAUT;
            this.laMusique.auteurs = (CbCat.SelectedItem as auteur);


        }

        //Revenir en arrière
        private void BtAnnulerNouveau_Click(object sender, RoutedEventArgs e)
        {
            Initialiser(true);
            SpDetailService.DataContext = this.lesMusiques[0];
            CbCat.SelectedItem = this.lesMusiques[0].auteurs;
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
