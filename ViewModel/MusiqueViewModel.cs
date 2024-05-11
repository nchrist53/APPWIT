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
    class MusiqueViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Data db = new Data();
        private List<service> lesServices;
        private service leService;
        private T leControl;
        private List<categorie> lesCategories;
        private categorie laCategorie;
        public ServiceViewModel()
        {
            this.lesServices = db.services.ToList();
            this.lesCategories = db.categories.ToList();

        }

        public List<categorie> LesCategories
        {
            get
            {
                return this.lesCategories;
            }
            set
            {
                this.lesCategories = value;
                OnPropertyChanged("LesCategories");
            }
        }

        public categorie LaCategorie
        {
            get
            {
                return this.laCategorie;
            }
            set
            {
                this.laCategorie = value;
                OnPropertyChanged("LesCategories");
            }
        }
        public List<service> LesServices
        {
            get
            {
                return this.lesServices;
            }
            set
            {
                this.lesServices = value;
                OnPropertyChanged("LesServices");
            }
        }


        public service LeService
        {
            get { return this.leService; }
            set
            {
                this.leService = value;
                OnPropertyChanged("LeService");
            }
        }

        public service RechercherLeService(int reference)
        {
            return this.lesServices.Where(unService => unService.REFERENCE == reference).FirstOrDefault();

        }

        //A compléter
        public List<service> RechercherLesServicesDUneCategorie(categorie uneCategorie)
        {
            return this.lesServices.Where(unService => unService.categorie.Equals(uneCategorie)).ToList();

        }

        public void ModifierUnService(service unService)
        {
            //On récupère l'indice du client
            for (int i = 0; i < this.lesServices.Count; i++)
            {
                if (this.lesServices[i].REFERENCE == unService.REFERENCE)
                {
                    db.services.ToList()[i] = unService;
                }
            }
            int cle = db.services.ToList().IndexOf(this.RechercherLeService(unService.REFERENCE));
            db.services.ToList()[cle] = unService;

            //On essaye de mettre à jour la bdd
            try
            {
                db.SaveChanges();
                MessageBox.Show($"Le service avec la référence : {unService.REFERENCE} a été modifié.e!");
                LesServices = db.services.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show("pas OK");
            }
        }
        public service RetournerLeDernierCService(service unService)
        {
            return this.lesServices.Last();
        }
        public void AjouterUnService(service unService)
        {

            db.services.Add(unService);

            try
            {
                db.SaveChanges();
                MessageBox.Show($"Le service {unService.TITRE} a été ajouté!");
                LesServices = db.services.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Une erreur s'est produite...");
            }



        }

        public void SupprimerUnService(service unService)
        {
            int cle = -1;
            if (this.lesServices.Contains(unService))
            {
                cle = lesServices.IndexOf(unService);
                db.services.Remove(unService);

            }
            try
            {
                db.SaveChanges();
                MessageBox.Show($"Le service {unService.TITRE} a été supprimé!");
                LesServices = db.services.ToList();
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
        }
    }
}
