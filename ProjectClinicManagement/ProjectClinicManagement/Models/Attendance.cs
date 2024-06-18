using ProjectClinicManagement.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class Attendance
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public DateTime CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public virtual Account Account { get; set; }
}
