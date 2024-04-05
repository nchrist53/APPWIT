using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AWIT.Model
{
    public partial class Data : DbContext
    {
        public Data()
            : base("name=Data")
        {
        }

        public virtual DbSet<abonnement> abonnements { get; set; }
        public virtual DbSet<album> albums { get; set; }
        public virtual DbSet<auteur> auteurs { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<commande> commandes { get; set; }
        public virtual DbSet<groupe> groupes { get; set; }
        public virtual DbSet<messenger_messages> messenger_messages { get; set; }
        public virtual DbSet<musique> musiques { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<abonnement>()
                .Property(e => e.NOM)
                .IsFixedLength();

            modelBuilder.Entity<abonnement>()
                .Property(e => e.PRIXMENSUEL)
                .HasPrecision(5, 2);

            modelBuilder.Entity<abonnement>()
                .HasMany(e => e.commandes)
                .WithRequired(e => e.abonnement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<abonnement>()
                .HasMany(e => e.musiques)
                .WithMany(e => e.abonnements)
                .Map(m => m.ToTable("contenir", "awitbdd").MapLeftKey("IDABO").MapRightKey("REFMUS"));

            modelBuilder.Entity<album>()
                .Property(e => e.NOM)
                .IsFixedLength();

            modelBuilder.Entity<album>()
                .Property(e => e.IMAGE)
                .IsFixedLength();

            modelBuilder.Entity<album>()
                .HasMany(e => e.musiques)
                .WithRequired(e => e.album)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<auteur>()
                .Property(e => e.NOM)
                .IsFixedLength();

            modelBuilder.Entity<auteur>()
                .HasMany(e => e.musiques)
                .WithMany(e => e.auteurs)
                .Map(m => m.ToTable("creermusiqueaut", "awitbdd").MapLeftKey("IDAUT").MapRightKey("REFMUS"));

            modelBuilder.Entity<client>()
                .Property(e => e.LOGINCLI)
                .IsFixedLength();

            modelBuilder.Entity<client>()
                .Property(e => e.MDPCLI)
                .IsFixedLength();

            modelBuilder.Entity<client>()
                .Property(e => e.NOMCLI)
                .IsFixedLength();

            modelBuilder.Entity<client>()
                .Property(e => e.PRENOMCLI)
                .IsFixedLength();

            modelBuilder.Entity<groupe>()
                .Property(e => e.NOM)
                .IsFixedLength();

            modelBuilder.Entity<groupe>()
                .HasMany(e => e.musiques)
                .WithMany(e => e.groupes)
                .Map(m => m.ToTable("creermusiquegrp", "awitbdd").MapLeftKey("IDGRP").MapRightKey("REFMUS"));

            modelBuilder.Entity<messenger_messages>()
                .Property(e => e.created_at)
                .HasPrecision(0);

            modelBuilder.Entity<messenger_messages>()
                .Property(e => e.available_at)
                .HasPrecision(0);

            modelBuilder.Entity<messenger_messages>()
                .Property(e => e.delivered_at)
                .HasPrecision(0);

            modelBuilder.Entity<musique>()
                .Property(e => e.TITRE)
                .IsFixedLength();

            modelBuilder.Entity<musique>()
                .Property(e => e.SON)
                .IsFixedLength();
        }
    }
}
