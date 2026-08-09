using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("teacherprofiles")]
[Index("Userid", Name = "teacherprofiles_userid_key", IsUnique = true)]
public partial class Teacherprofile
{
    [Key]
    [Column("teacherid")]
    public Guid Teacherid { get; set; }

    [Column("userid")]
    public Guid Userid { get; set; }

    [Column("employeeno")]
    [StringLength(50)]
    public string? Employeeno { get; set; }

    [Column("department")]
    [StringLength(100)]
    public string? Department { get; set; }

    [Column("designation")]
    [StringLength(100)]
    public string? Designation { get; set; }

    [Column("joiningdate")]
    public DateOnly? Joiningdate { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("photourl")]
    public string? Photourl { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [InverseProperty("Teacher")]
    public virtual ICollection<Teacherassignment> Teacherassignments { get; set; } = new List<Teacherassignment>();

    [ForeignKey("Userid")]
    [InverseProperty("Teacherprofile")]
    public virtual User User { get; set; } = null!;
}
