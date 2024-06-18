using ProjectClinicManagement.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Attendance
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int AccountId { get; set; }
    
    [Required]
    public DateTime CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    [ForeignKey("AccountId")] public virtual Account? Account { get; set; }
}
