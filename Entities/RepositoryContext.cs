using System;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities
{
    public partial class RepositoryContext :DbContext
    {
        public RepositoryContext()
        {
        }

        public virtual DbSet<Contributor> Contributor { get; set; }
        public virtual DbSet<ContributorPayment> ContributorPayment { get; set; }
        public virtual DbSet<ContributorTicket> ContributorTicket { get; set; }
        public virtual DbSet<Meeting> Meeting { get; set; }
        public virtual DbSet<MeetingSpeaker> MeetingSpeaker { get; set; }
        public virtual DbSet<MeetingTicket> MeetingTicket { get; set; }
        public virtual DbSet<MeetingTicketParam> MeetingTicketParam { get; set; }
        public virtual DbSet<Speaker> Speaker { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:DefaultSchema", "yabande1_event");

            modelBuilder.Entity<Contributor>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MobileNumber).HasMaxLength(15);

                entity.Property(e => e.NationalCode).HasMaxLength(15);
            });

            modelBuilder.Entity<ContributorPayment>(entity =>
            {
                entity.HasIndex(e => e.ContributorTicketId);

                entity.Property(e => e.CardPan)
                    .HasColumnName("card_pan")
                    .HasMaxLength(100);

                entity.Property(e => e.TransactionCode)
                    .HasColumnName("transactionCode")
                    .HasMaxLength(100);

                entity.HasOne(d => d.ContributorTicket)
                    .WithMany(p => p.ContributorPayment)
                    .HasForeignKey(d => d.ContributorTicketId)
                    .HasConstraintName("FK_ContributorPayment_ContributorTicket");
            });

            modelBuilder.Entity<ContributorTicket>(entity =>
            {
                entity.HasIndex(e => e.ContributorId);

                entity.HasIndex(e => e.MeetingTicketId);

                entity.HasOne(d => d.Contributor)
                    .WithMany(p => p.ContributorTicket)
                    .HasForeignKey(d => d.ContributorId)
                    .HasConstraintName("FK_ContributorTicket_Contributor");

                entity.HasOne(d => d.MeetingTicket)
                    .WithMany(p => p.ContributorTicket)
                    .HasForeignKey(d => d.MeetingTicketId)
                    .HasConstraintName("FK_ContributorTicket_MeetingTicket");
            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MeetingPlace).HasMaxLength(512);

                entity.Property(e => e.MeetingTitle).HasMaxLength(512);
            });

            modelBuilder.Entity<MeetingSpeaker>(entity =>
            {
                entity.HasIndex(e => e.MeetingId);

                entity.HasIndex(e => e.SpeakerId);

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.MeetingSpeaker)
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("FK_MeetingSpeaker_Meeting");

                entity.HasOne(d => d.Speaker)
                    .WithMany(p => p.MeetingSpeaker)
                    .HasForeignKey(d => d.SpeakerId)
                    .HasConstraintName("FK_MeetingSpeaker_Speaker");
            });

            modelBuilder.Entity<MeetingTicket>(entity =>
            {
                entity.HasIndex(e => e.MeetingId);

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.MeetingTicket)
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("FK_MeetingTicket_Meeting");
            });

            modelBuilder.Entity<MeetingTicketParam>(entity =>
            {
                entity.Property(e => e.MeetingTicketParamId).HasColumnName("MeetingTicketParamID");

                entity.HasOne(d => d.MeetingTicket)
                    .WithMany(p => p.MeetingTicketParam)
                    .HasForeignKey(d => d.MeetingTicketId)
                    .HasConstraintName("FK_MeetingTicketParam_MeetingTicket");
            });

            modelBuilder.Entity<Speaker>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);
            });


        }


    }
}
