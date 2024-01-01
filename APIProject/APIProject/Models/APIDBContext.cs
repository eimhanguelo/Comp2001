using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIProject.Models
{
    public partial class APIDBContext : DbContext
    {
        public APIDBContext()
        {
        }

        public APIDBContext(DbContextOptions<APIDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<Trail> Trails { get; set; } = null!;
        public virtual DbSet<TrailComment> TrailComments { get; set; } = null!;
        public virtual DbSet<TrailCommentsView> TrailCommentsViews { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile", "CW1");

                entity.Property(e => e.ProfileId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProfileID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePictureUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ProfilePictureURL");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Profile__UserID__74AE54BC");
            });

            modelBuilder.Entity<Trail>(entity =>
            {
                entity.ToTable("Trail", "CW1");

                entity.Property(e => e.TrailId)
                    .ValueGeneratedNever()
                    .HasColumnName("TrailID");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Difficulty)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ElevationGain).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ElevationLoss).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Features).HasColumnType("text");

                entity.Property(e => e.Length).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrailComment>(entity =>
            {
                entity.ToTable("TrailComment", "CW1");

                entity.Property(e => e.TrailCommentId)
                    .ValueGeneratedNever()
                    .HasColumnName("TrailCommentID");

                entity.Property(e => e.Comment).HasColumnType("text");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.TrailId).HasColumnName("TrailID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Trail)
                    .WithMany(p => p.TrailComments)
                    .HasForeignKey(d => d.TrailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrailComm__Trail__797309D9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TrailComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrailComm__UserI__7A672E12");
            });

            modelBuilder.Entity<TrailCommentsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TrailCommentsView", "CW1");

                entity.Property(e => e.CommentDateCreated).HasColumnType("datetime");

                entity.Property(e => e.CommentUserId).HasColumnName("CommentUserID");

                entity.Property(e => e.CommentUsername)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TrailComment).HasColumnType("text");

                entity.Property(e => e.TrailCommentId).HasColumnName("TrailCommentID");

                entity.Property(e => e.TrailDescription).HasColumnType("text");

                entity.Property(e => e.TrailDifficulty)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TrailElevationGain).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TrailElevationLoss).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TrailFeatures).HasColumnType("text");

                entity.Property(e => e.TrailId).HasColumnName("TrailID");

                entity.Property(e => e.TrailLength).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TrailLocation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TrailName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "CW1");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId)
                    .HasName("PK__UserProf__290C8884359DCD45");

                entity.ToTable("UserProfile", "CW1");

                entity.Property(e => e.ProfileId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProfileID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProfi__UserI__71D1E811");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
