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
    class ClientViewModel<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Data db = new Data();
        private List<client> lesClients;
        private client leClient;
        public ClientViewModel()
        {
            lesClients = db.clients.ToList();
        }


        public List<client> LesClients
        {
            get
            {
                return lesClients;
            }
            set
            {
                lesClients = value;
                OnPropertyChanged("LesClients");
            }
        }


        public client LeClient
        {
            get { return leClient; }
            set
            {
                leClient = value;
                OnPropertyChanged("LeClient");
            }
        }

        public client RechercherLeClient(string login)
        {
            return this.lesClients.Where(unClient => unClient.LOGINCLI == login).FirstOrDefault();

        }

        public void ModifierUnClient(client unClient)
        {
            //On récupère l'indice du client
            for (int i = 0; i < this.lesClients.Count; i++)
            {
                if (this.lesClients[i].LOGINCLI == unClient.LOGINCLI)
                {
                    db.clients.ToList()[i] = unClient;
                }
            }
            //On essaye de mettre à jour la bdd
            try
            {
                db.SaveChanges();
                MessageBox.Show($"Le.a client.e avec le login : {unClient.LOGINCLI} a été modifié.e!");
                LesClients = db.clients.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show("pas OK");
            }
        }
        public client RetournerLeDernierClient(client unClient)
        {
            return this.lesClients.Last();
        }
        public void AjouterUnClient(client unClient)
        {
            db.clients.Add(unClient);

            try
            {
                db.SaveChanges();



                MessageBox.Show($"Client.e {unClient.PRENOMCLI} {unClient.NOMCLI} ajouté.e!");
                LesClients = db.clients.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Une erreur s'est produite...");
            }



        }

        public void SupprimerUnClient(client unClient)
        {
            if (this.lesClients.Contains(unClient))
            {
                db.clients.Remove(unClient);

            }


            try
            {

                db.SaveChanges();
                MessageBox.Show($"Client.e {unClient.PRENOMCLI} {unClient.NOMCLI} supprimé.e!");
                LesClients = db.clients.ToList();
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
