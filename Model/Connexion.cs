using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AWIT.Model
{
    class Connexion
    {
        private const int V = 128 / 8;
        private static string login;
        private static string mdp;
        private static byte[] salt = { 128, 8 };
        public static Connexion laConnexion;
        public static bool etat = false;

        private Connexion()
        {
            login = "admin";
            string motDePasse = "admin";
            mdp = Hashage(motDePasse);
        }

        public static string Hashage(string mot)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: mot,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
        }
        public static bool GetEtat()
        {
            return etat;
        }
        public static void SetEtat(bool statut)
        {
            etat = statut;
        }
        public static void GetConnexion()
        {
            if (laConnexion == null)
            {
                laConnexion = new Connexion();
            }

        }

        public static string GetLogin()
        {
            return login;
        }
        public static string GetMDP()
        {
            return mdp;
        }
    }
}
