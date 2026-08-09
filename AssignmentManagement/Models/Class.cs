using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("classes")]
public partial class Class
{
    [Key]
    [Column("classid")]
    public int Classid { get; set; }

    [Column("classname")]
    [StringLength(100)]
    public string Classname { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
