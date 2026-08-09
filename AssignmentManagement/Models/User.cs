using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("users")]
[Index("Email", Name = "users_email_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("userid")]
    public Guid Userid { get; set; }

    [Column("firstname")]
    [StringLength(100)]
    public string Firstname { get; set; } = null!;

    [Column("lastname")]
    [StringLength(100)]
    public string? Lastname { get; set; }

    [Column("email")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Column("passwordhash")]
    public string Passwordhash { get; set; } = null!;

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("roleid")]
    public int Roleid { get; set; }

    [Column("isemailverified")]
    public bool? Isemailverified { get; set; }

    [Column("isactive")]
    public bool? Isactive { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [ForeignKey("Roleid")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual Studentprofile? Studentprofile { get; set; }

    [InverseProperty("User")]
    public virtual Teacherprofile? Teacherprofile { get; set; }
}
