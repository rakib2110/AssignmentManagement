using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("assignmentsubmissions")]
public partial class Assignmentsubmission
{
    [Key]
    [Column("submissionid")]
    public int Submissionid { get; set; }

    [Column("assignmentid")]
    public int Assignmentid { get; set; }

    [Column("studentid")]
    public Guid Studentid { get; set; }

    [Column("answer")]
    public string? Answer { get; set; }

    [Column("submittedat", TypeName = "timestamp without time zone")]
    public DateTime? Submittedat { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string? Status { get; set; }

    [Column("obtainedmarks")]
    [Precision(5, 2)]
    public decimal? Obtainedmarks { get; set; }

    [Column("teacherfeedback")]
    public string? Teacherfeedback { get; set; }

    [Column("reviewedat", TypeName = "timestamp without time zone")]
    public DateTime? Reviewedat { get; set; }

    [ForeignKey("Assignmentid")]
    [InverseProperty("Assignmentsubmissions")]
    public virtual Assignment Assignment { get; set; } = null!;

    [ForeignKey("Studentid")]
    [InverseProperty("Assignmentsubmissions")]
    public virtual Studentprofile Student { get; set; } = null!;

    [InverseProperty("Submission")]
    public virtual ICollection<Submissionfile> Submissionfiles { get; set; } = new List<Submissionfile>();
}
