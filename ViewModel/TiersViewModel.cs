using AWIT.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AWIT.ViewModel
{
    class TiersViewModel<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Data db = new Data();
        private List<abonnement> lesTiers;
        private abonnement leTier;
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

        public TiersViewModel()
        {
            lesTiers = db.abonnements.ToList();
        }


        public List<abonnement> LesTiers
        {
            get
            {
                return lesTiers;
            }
            set
            {
                lesTiers = value;
                OnPropertyChanged("LesTiers");
            }
        }


        public abonnement LeTier
        {
            get { return leTier; }
            set
            {
                leTier = value;
                OnPropertyChanged("LeTier");
            }
        }

        public abonnement RechercherLeTier(long idTier)
        {
            return this.lesTiers.Where(leTier => leTier.IDABO == idTier).FirstOrDefault();
        }

        public void ModifierUnTier(abonnement leTier)
        {
            //On récupère l'indice du tier
            for (int i = 0; i < this.lesTiers.Count; i++)
            {
                if (this.lesTiers[i].IDABO == leTier.IDABO)
                {
                    db.abonnements.ToList()[i] = leTier;
                }
            }
            //On essaye de mettre à jour la bdd
            try
            {
                db.SaveChanges();
                MessageBox.Show($"Le tier n°{leTier.IDABO} a été modifié.e!");
                LesTiers = db.abonnements.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show("pas OK");
            }
        }
        public abonnement RetournerLeDernierTier(abonnement unTier)
        {
            return this.lesTiers.Last();
        }
        public void AjouterUnTier(abonnement unTier)
        {
            db.abonnements.Add(unTier);

            try
            {
                db.SaveChanges();



                MessageBox.Show($"Tier {unTier.NOM} ajouté!");
                LesTiers = db.abonnements.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Une erreur s'est produite...");
            }



        }

        public void SupprimerUnTier(abonnement unTier)
        {
            int cle = -1;
            if (this.lesTiers.Contains(unTier))
            {
                cle = lesTiers.IndexOf(unTier);
                db.abonnements.Remove(unTier);

            }


            try
            {

                db.SaveChanges();
                MessageBox.Show($"Tier {unTier.NOM} supprimé!");
                LesTiers = db.abonnements.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Une erreur s'est produite...");
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
            (leControl as ListBox).ItemsSource = LesTiers;

        }
    }
}
