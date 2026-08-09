using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("teacherassignments")]
public partial class Teacherassignment
{
    [Key]
    [Column("teacherassignmentid")]
    public int Teacherassignmentid { get; set; }

    [Column("teacherid")]
    public Guid Teacherid { get; set; }

    [Column("subjectid")]
    public int Subjectid { get; set; }

    [Column("academicyear")]
    [StringLength(20)]
    public string? Academicyear { get; set; }

    [Column("semester")]
    [StringLength(20)]
    public string? Semester { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [InverseProperty("Teacherassignment")]
    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    [ForeignKey("Subjectid")]
    [InverseProperty("Teacherassignments")]
    public virtual Subject Subject { get; set; } = null!;

    [ForeignKey("Teacherid")]
    [InverseProperty("Teacherassignments")]
    public virtual Teacherprofile Teacher { get; set; } = null!;
}
