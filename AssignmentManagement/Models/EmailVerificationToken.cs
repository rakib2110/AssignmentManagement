using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

[Table("email_verification_tokens")]
[Index("Token", Name = "idx_email_verification_token")]
public partial class EmailVerificationToken
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("userid")]
    public Guid Userid { get; set; }

    [Column("token")]
    [StringLength(255)]
    public string Token { get; set; } = null!;

    [Column("expiresat", TypeName = "timestamp without time zone")]
    public DateTime Expiresat { get; set; }

    [Column("isused")]
    public bool Isused { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime Createdat { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("EmailVerificationTokens")]
    public virtual User User { get; set; } = null!;
}
