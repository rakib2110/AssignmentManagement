using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("assignments")]
public partial class Assignment
{
    [Key]
    [Column("assignmentid")]
    public int Assignmentid { get; set; }

    [Column("teacherassignmentid")]
    public int Teacherassignmentid { get; set; }

    [Column("title")]
    [StringLength(250)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("instructions")]
    public string? Instructions { get; set; }

    [Column("publishdate", TypeName = "timestamp without time zone")]
    public DateTime? Publishdate { get; set; }

    [Column("duedate", TypeName = "timestamp without time zone")]
    public DateTime? Duedate { get; set; }

    [Column("maxmarks")]
    [Precision(5, 2)]
    public decimal? Maxmarks { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string? Status { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [InverseProperty("Assignment")]
    public virtual ICollection<Assignmentfile> Assignmentfiles { get; set; } = new List<Assignmentfile>();

    [InverseProperty("Assignment")]
    public virtual ICollection<Assignmentsubmission> Assignmentsubmissions { get; set; } = new List<Assignmentsubmission>();

    [ForeignKey("Teacherassignmentid")]
    [InverseProperty("Assignments")]
    public virtual Teacherassignment Teacherassignment { get; set; } = null!;
}
