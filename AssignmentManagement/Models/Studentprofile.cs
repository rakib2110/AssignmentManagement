using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("studentprofiles")]
[Index("Userid", Name = "studentprofiles_userid_key", IsUnique = true)]
public partial class Studentprofile
{
    [Key]
    [Column("studentid")]
    public Guid Studentid { get; set; }

    [Column("userid")]
    public Guid Userid { get; set; }

    [Column("studentroll")]
    [StringLength(50)]
    public string? Studentroll { get; set; }

    [Column("registrationno")]
    [StringLength(50)]
    public string? Registrationno { get; set; }

    [Column("batch")]
    [StringLength(30)]
    public string? Batch { get; set; }

    [Column("semester")]
    [StringLength(20)]
    public string? Semester { get; set; }

    [Column("department")]
    [StringLength(100)]
    public string? Department { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("photourl")]
    public string? Photourl { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<Assignmentsubmission> Assignmentsubmissions { get; set; } = new List<Assignmentsubmission>();

    [InverseProperty("Student")]
    public virtual ICollection<Studentenrollment> Studentenrollments { get; set; } = new List<Studentenrollment>();

    [ForeignKey("Userid")]
    [InverseProperty("Studentprofile")]
    public virtual User User { get; set; } = null!;
}
