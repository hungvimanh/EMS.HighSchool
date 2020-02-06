using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EMS.HighSchool.Repositories.Models
{
    public partial class EMSContext : DbContext
    {
        public virtual DbSet<AspirationDAO> Aspiration { get; set; }
        public virtual DbSet<DistrictDAO> District { get; set; }
        public virtual DbSet<EthnicDAO> Ethnic { get; set; }
        public virtual DbSet<HighSchoolDAO> HighSchool { get; set; }
        public virtual DbSet<MajorsDAO> Majors { get; set; }
        public virtual DbSet<ProvinceDAO> Province { get; set; }
        public virtual DbSet<RegisterExamDAO> RegisterExam { get; set; }
        public virtual DbSet<StudentDAO> Student { get; set; }
        public virtual DbSet<SubjectGroupDAO> SubjectGroup { get; set; }
        public virtual DbSet<TeacherDAO> Teacher { get; set; }
        public virtual DbSet<TownDAO> Town { get; set; }
        public virtual DbSet<UniversityDAO> University { get; set; }
        public virtual DbSet<University_MajorsDAO> University_Majors { get; set; }
        public virtual DbSet<University_Majors_SubjectGroupDAO> University_Majors_SubjectGroup { get; set; }
        public virtual DbSet<UserDAO> User { get; set; }

        public EMSContext(DbContextOptions<EMSContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=.;initial catalog=EMS;persist security info=True;user id=sa;password=123456a@;multipleactiveresultsets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspirationDAO>(entity =>
            {
                entity.ToTable("Aspiration", "HS");

                entity.HasOne(d => d.Majors)
                    .WithMany(p => p.Aspirations)
                    .HasForeignKey(d => d.MajorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aspiration_Majors");

                entity.HasOne(d => d.RegisterExam)
                    .WithMany(p => p.Aspirations)
                    .HasForeignKey(d => d.RegisterExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aspiration_RegisterExam");

                entity.HasOne(d => d.SubjectGroup)
                    .WithMany(p => p.Aspirations)
                    .HasForeignKey(d => d.SubjectGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aspiration_SubjectGroup");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Aspirations)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aspiration_University");
            });

            modelBuilder.Entity<DistrictDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<EthnicDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<HighSchoolDAO>(entity =>
            {
                entity.ToTable("HighSchool", "HS");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.HighSchools)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HighSchool_Province");
            });

            modelBuilder.Entity<MajorsDAO>(entity =>
            {
                entity.ToTable("Majors", "UNV");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ProvinceDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RegisterExamDAO>(entity =>
            {
                entity.ToTable("RegisterExam", "HS");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ExceptLanguages).HasMaxLength(500);

                entity.Property(e => e.Languages).HasMaxLength(50);

                entity.Property(e => e.PriorityType)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.HasOne(d => d.ClusterContest)
                    .WithMany(p => p.RegisterExams)
                    .HasForeignKey(d => d.ClusterContestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisterExam_Province");

                entity.HasOne(d => d.RegisterPlaceOfExam)
                    .WithMany(p => p.RegisterExams)
                    .HasForeignKey(d => d.RegisterPlaceOfExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisterExam_HighSchool");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RegisterExams)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisterExam_Student");
            });

            modelBuilder.Entity<StudentDAO>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Identify)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(50);

                entity.HasOne(d => d.Ethnic)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.EthnicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Ethnic");

                entity.HasOne(d => d.HighSchool)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.HighSchoolId)
                    .HasConstraintName("FK_Student_HighSchool");

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.TownId)
                    .HasConstraintName("FK_Student_Town");
            });

            modelBuilder.Entity<SubjectGroupDAO>(entity =>
            {
                entity.ToTable("SubjectGroup", "HS");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TeacherDAO>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Identify)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(50);
            });

            modelBuilder.Entity<TownDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Towns)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Town_District");
            });

            modelBuilder.Entity<UniversityDAO>(entity =>
            {
                entity.ToTable("University", "UNV");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Descreption).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Website).HasMaxLength(50);
            });

            modelBuilder.Entity<University_MajorsDAO>(entity =>
            {
                entity.ToTable("University_Majors", "UNV");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.HasOne(d => d.Majors)
                    .WithMany(p => p.University_Majors)
                    .HasForeignKey(d => d.MajorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_Majors");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.University_Majors)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_University");
            });

            modelBuilder.Entity<University_Majors_SubjectGroupDAO>(entity =>
            {
                entity.ToTable("University_Majors_SubjectGroup", "UNV");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(d => d.SubjectGroup)
                    .WithMany(p => p.University_Majors_SubjectGroups)
                    .HasForeignKey(d => d.SubjectGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_SubjectGroup_SubjectGroup");

                entity.HasOne(d => d.University_Majors)
                    .WithMany(p => p.University_Majors_SubjectGroups)
                    .HasForeignKey(d => d.University_MajorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_SubjectGroup_University_Majors");
            });

            modelBuilder.Entity<UserDAO>(entity =>
            {
                entity.ToTable("User", "APPSYS");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salt).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_User_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
