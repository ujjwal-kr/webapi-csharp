using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
public class TodoItem {
    [Key]
    public long Id {get; set;}
    [Required]
    public string Name {get;set;}
    [Required]
    public bool IsComplete {get; set;}
}