using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

public partial class AssignmentManagementDbContext : DbContext
{
    public AssignmentManagementDbContext()
    {
    }

    public AssignmentManagementDbContext(DbContextOptions<AssignmentManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Assignmentfile> Assignmentfiles { get; set; }

    public virtual DbSet<Assignmentsubmission> Assignmentsubmissions { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<EmailSetting> EmailSettings { get; set; }

    public virtual DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Studentenrollment> Studentenrollments { get; set; }

    public virtual DbSet<Studentprofile> Studentprofiles { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Submissionfile> Submissionfiles { get; set; }

    public virtual DbSet<Teacherassignment> Teacherassignments { get; set; }

    public virtual DbSet<Teacherprofile> Teacherprofiles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AssignmentManagementDB;Username=postgres;Password=w23eW@#E");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Assignmentid).HasName("assignments_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Teacherassignment).WithMany(p => p.Assignments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_assignment_teacherassignment");
        });

        modelBuilder.Entity<Assignmentfile>(entity =>
        {
            entity.HasKey(e => e.Fileid).HasName("assignmentfiles_pkey");

            entity.Property(e => e.Uploadedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Assignmentfiles).HasConstraintName("fk_assignmentfile_assignment");
        });

        modelBuilder.Entity<Assignmentsubmission>(entity =>
        {
            entity.HasKey(e => e.Submissionid).HasName("assignmentsubmissions_pkey");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Assignmentsubmissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_submission_assignment");

            entity.HasOne(d => d.Student).WithMany(p => p.Assignmentsubmissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_submission_student");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Classid).HasName("classes_pkey");
        });

        modelBuilder.Entity<EmailSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailSettings_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<EmailVerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("email_verification_tokens_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Isused).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.EmailVerificationTokens).HasConstraintName("fk_email_verification_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("roles_pkey");
        });

        modelBuilder.Entity<Studentenrollment>(entity =>
        {
            entity.HasKey(e => e.Enrollmentid).HasName("studentenrollments_pkey");

            entity.Property(e => e.Enrolledat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Student).WithMany(p => p.Studentenrollments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_enroll_student");

            entity.HasOne(d => d.Subject).WithMany(p => p.Studentenrollments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_enroll_subject");
        });

        modelBuilder.Entity<Studentprofile>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("studentprofiles_pkey");

            entity.Property(e => e.Studentid).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.User).WithOne(p => p.Studentprofile).HasConstraintName("fk_student_user");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Subjectid).HasName("subjects_pkey");

            entity.HasOne(d => d.Class).WithMany(p => p.Subjects).HasConstraintName("fk_subject_class");
        });

        modelBuilder.Entity<Submissionfile>(entity =>
        {
            entity.HasKey(e => e.Submissionfileid).HasName("submissionfiles_pkey");

            entity.Property(e => e.Uploadedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Submission).WithMany(p => p.Submissionfiles).HasConstraintName("fk_submissionfile_submission");
        });

        modelBuilder.Entity<Teacherassignment>(entity =>
        {
            entity.HasKey(e => e.Teacherassignmentid).HasName("teacherassignments_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Subject).WithMany(p => p.Teacherassignments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_assign_subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Teacherassignments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_assign_teacher");
        });

        modelBuilder.Entity<Teacherprofile>(entity =>
        {
            entity.HasKey(e => e.Teacherid).HasName("teacherprofiles_pkey");

            entity.Property(e => e.Teacherid).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.User).WithOne(p => p.Teacherprofile).HasConstraintName("fk_teacher_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.Property(e => e.Userid).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Isactive).HasDefaultValue(true);
            entity.Property(e => e.Isemailverified).HasDefaultValue(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
