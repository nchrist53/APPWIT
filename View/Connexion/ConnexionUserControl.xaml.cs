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

namespace AWIT.View.Connexion
{
    /// <summary>
    /// Logique d'interaction pour ConnexionUserControl.xaml
    /// </summary>
    public partial class ConnexionUserControl : UserControl
    {
        MainWindow mainWindow;
        public ConnexionUserControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == Model.Connexion.GetLogin()
                && Model.Connexion.Hashage(MDP.Password) == Model.Connexion.GetMDP())
            {
                mainWindow.SeConnecter();

            }
        }
    }
}
