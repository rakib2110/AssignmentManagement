using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("submissionfiles")]
public partial class Submissionfile
{
    [Key]
    [Column("submissionfileid")]
    public int Submissionfileid { get; set; }

    [Column("submissionid")]
    public int Submissionid { get; set; }

    [Column("filename")]
    [StringLength(255)]
    public string? Filename { get; set; }

    [Column("filepath")]
    public string? Filepath { get; set; }

    [Column("uploadedat", TypeName = "timestamp without time zone")]
    public DateTime? Uploadedat { get; set; }

    [ForeignKey("Submissionid")]
    [InverseProperty("Submissionfiles")]
    public virtual Assignmentsubmission Submission { get; set; } = null!;
}
