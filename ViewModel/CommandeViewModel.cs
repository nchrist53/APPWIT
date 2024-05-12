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
    class CommandeViewModel<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Data db = new Data();
        private List<commande> lesCommandes;
        private commande laCommande;
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


        public CommandeViewModel()
        {
            lesCommandes = db.commandes.ToList();
        }

        public List<commande> LesCommandes
        {
            set
            {
                this.lesCommandes = value;
                OnPropertyChanged("LesCommandes");
            }
            get
            {
                return this.lesCommandes;
            }
        }

        public commande LaCommande
        {
            set
            {
                this.laCommande = value;
                OnPropertyChanged("LaCommande");
            }
            get
            {
                return this.laCommande;
            }
        }

        public List<client> GetLesClients()
        {
            return db.clients.ToList();
        }

        public client RechercherClient(string login)
        {
            return db.clients.ToList().Where(cl => cl.LOGINCLI == login).FirstOrDefault();
        }
        public commande RechercherLaCommande(int num)
        {
            return db.commandes.ToList().Where(x => x.REFCOM == num).FirstOrDefault();
        }

        public void SupprimerCommande(commande uneCommande)
        {
            if (this.lesCommandes.Contains(uneCommande))
            {
                this.db.commandes.Remove(uneCommande);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Ok!");
                    LesCommandes = db.commandes.ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Pas ok!");
                }

            }
        }

        public void AjouterCommande(commande uneCommande)
        {
            uneCommande.DATECRE = DateTime.Now;
            uneCommande.DATEDEBUT = DateTime.Now;
            uneCommande.DATEFIN = DateTime.Now.AddMonths(1);
            db.commandes.Add(uneCommande);
            try
            {
                db.SaveChanges();
                MessageBox.Show("Ok!");
                LesCommandes = db.commandes.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show("Pas ok!");
            }

        }

        public void ModifierUneCommande(commande uneCommande)
        {
            int index = -1;
            foreach (commande item in this.lesCommandes)
            {
                if (item.REFCOM == uneCommande.REFCOM)
                {
                    index = this.lesCommandes.IndexOf(item);
                }
            }
            if (index != -1)
            {
                db.commandes.ToList()[index] = uneCommande;
            }
            try
            {
                db.SaveChanges();
                MessageBox.Show("Ok!");
                LesCommandes = db.commandes.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show("Pas ok!");
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
            (leControl as ListBox).ItemsSource = LesCommandes;

        }
    }
}

