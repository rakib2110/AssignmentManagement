using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("subjects")]
public partial class Subject
{
    [Key]
    [Column("subjectid")]
    public int Subjectid { get; set; }

    [Column("classid")]
    public int Classid { get; set; }

    [Column("subjectname")]
    [StringLength(100)]
    public string Subjectname { get; set; } = null!;

    [Column("subjectcode")]
    [StringLength(20)]
    public string? Subjectcode { get; set; }

    [Column("credit")]
    [Precision(3, 1)]
    public decimal? Credit { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [ForeignKey("Classid")]
    [InverseProperty("Subjects")]
    public virtual Class Class { get; set; } = null!;

    [InverseProperty("Subject")]
    public virtual ICollection<Studentenrollment> Studentenrollments { get; set; } = new List<Studentenrollment>();

    [InverseProperty("Subject")]
    public virtual ICollection<Teacherassignment> Teacherassignments { get; set; } = new List<Teacherassignment>();
}
