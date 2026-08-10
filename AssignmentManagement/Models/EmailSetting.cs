using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Models;

public partial class EmailSetting
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string SmtpServer { get; set; } = null!;

    public int Port { get; set; }

    [StringLength(255)]
    public string SenderName { get; set; } = null!;

    [StringLength(255)]
    public string SenderEmail { get; set; } = null!;

    [StringLength(255)]
    public string Username { get; set; } = null!;

    [StringLength(500)]
    public string Password { get; set; } = null!;
}
