using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("studentenrollments")]
public partial class Studentenrollment
{
    [Key]
    [Column("enrollmentid")]
    public int Enrollmentid { get; set; }

    [Column("studentid")]
    public Guid Studentid { get; set; }

    [Column("subjectid")]
    public int Subjectid { get; set; }

    [Column("academicyear")]
    [StringLength(20)]
    public string? Academicyear { get; set; }

    [Column("semester")]
    [StringLength(20)]
    public string? Semester { get; set; }

    [Column("enrolledat", TypeName = "timestamp without time zone")]
    public DateTime? Enrolledat { get; set; }

    [ForeignKey("Studentid")]
    [InverseProperty("Studentenrollments")]
    public virtual Studentprofile Student { get; set; } = null!;

    [ForeignKey("Subjectid")]
    [InverseProperty("Studentenrollments")]
    public virtual Subject Subject { get; set; } = null!;
}
