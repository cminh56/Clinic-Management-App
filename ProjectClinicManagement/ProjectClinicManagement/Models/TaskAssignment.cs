using ProjectClinicManagement.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class TaskAssignment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string TaskName { get; set; }

    [Required]
    public DateTime AssignedDate { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Description { get; set; }

    [Required]
    public TaskStatus Status { get; set; }

    public virtual Account Account { get; set; }
}

public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed,
    OnHold
}
