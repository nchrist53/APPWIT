using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AWIT.Model;

namespace AWIT.ViewModel
{
    class MusiqueViewModel<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Data db = new Data();
        private List<musique> lesMusiques;
        private musique laMusique;
        private List<auteur> lesAuteurs;
        private auteur lAuteur;
        private T leControl;

        public T LeControl
        {
            get
            {
                return this.leControl;
            }
            set
            {
                this.leControl = value;
            }
        }
        public MusiqueViewModel()
        {
            this.lesMusiques = db.musiques.ToList();
            this.lesAuteurs = db.auteurs.ToList();

        }

        public List<auteur> LesAuteurs
        {
            get
            {
                return this.lesAuteurs;
            }
            set
            {
                this.lesAuteurs = value;
                OnPropertyChanged("LesAuteurs");
            }
        }

        public auteur LeAuteur
        {
            get
            {
                return this.lAuteur;
            }
            set
            {
                this.lAuteur = value;
                OnPropertyChanged("LeAuteur");
            }
        }

        public List<musique> LesMusiques
        {
            get
            {
                return this.lesMusiques;
            }
            set
            {
                this.lesMusiques = value;
                OnPropertyChanged("LesMusiques");
            }
        }


        public musique LaMusique
        {
            get { return this.laMusique; }
            set
            {
                this.laMusique = value;
                OnPropertyChanged("LaMusique");
            }
        }

        public musique RechercherLaMusique(int reference)
        {
            return this.lesMusiques.Where(uneMusique => uneMusique.REFMUS == reference).FirstOrDefault();

        }

        //A compléter
        public List<musique> RechercherUneMusiqueDunAuteur(auteur unAuteur)
        {
            return this.lesMusiques.Where(uneMusique => uneMusique.auteurs.Equals(unAuteur)).ToList();

        }

        public void ModifierUneMusique(musique uneMusique)
        {
            //On récupère l'indice du client
            for (int i = 0; i < this.lesMusiques.Count; i++)
            {
                if (this.lesMusiques[i].REFMUS == uneMusique.REFMUS)
                {
                    db.musiques.ToList()[i] = uneMusique;
                }
            }
            int cle = db.musiques.ToList().IndexOf(this.RechercherLaMusique(Convert.ToInt32(uneMusique.REFMUS)));
            db.musiques.ToList()[cle] = uneMusique;

            //On essaye de mettre à jour la bdd
            try
            {
                db.SaveChanges();
                MessageBox.Show($"Le service avec la référence : {uneMusique.REFMUS} a été modifié.e!");
                LesMusiques = db.musiques.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show("pas OK");
            }
        }
        public musique RetournerLaDerniereMusique(musique uneMusique)
        {
            return this.lesMusiques.Last();
        }
        public void AjouterUneMusique(musique uneMusique)
        {

            db.musiques.Add(uneMusique);

            try
            {
                db.SaveChanges();
                MessageBox.Show($"Le service {uneMusique.TITRE} a été ajouté!");
                LesMusiques = db.musiques.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Une erreur s'est produite...");
            }



        }

        public void SupprimerUneMusique(musique uneMusique)
        {
            int cle = -1;
            if (this.lesMusiques.Contains(uneMusique))
            {
                cle = lesMusiques.IndexOf(uneMusique);
                db.musiques.Remove(uneMusique);

            }
            try
            {
                db.SaveChanges();
                MessageBox.Show($"Le service {uneMusique.TITRE} a été supprimé!");
                LesMusiques = db.musiques.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show("pas OK");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
            MisaAJourInterface(this.leControl);
        }

        public void MisaAJourInterface(T leControl)
        {
            Control x = leControl as Control;

            (leControl as ListBox).ItemsSource = null;
            (leControl as ListBox).ItemsSource = LesMusiques;

        }
    }
}
