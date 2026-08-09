using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("assignmentfiles")]
public partial class Assignmentfile
{
    [Key]
    [Column("fileid")]
    public int Fileid { get; set; }

    [Column("assignmentid")]
    public int Assignmentid { get; set; }

    [Column("filename")]
    [StringLength(255)]
    public string? Filename { get; set; }

    [Column("filepath")]
    public string? Filepath { get; set; }

    [Column("uploadedat", TypeName = "timestamp without time zone")]
    public DateTime? Uploadedat { get; set; }

    [ForeignKey("Assignmentid")]
    [InverseProperty("Assignmentfiles")]
    public virtual Assignment Assignment { get; set; } = null!;
}
