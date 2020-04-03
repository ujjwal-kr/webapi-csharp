using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Project {
    [Key]
    public long ProjectId {get; set;}
    [Required]
    public string Name {get; set;}
    [Required]
    public string Description {get; set;}

    public List<TodoItem> TodoItems {get; set;}
}


public class TodoItem {
    [Key]
    public long Id {get; set;}
    [Required]
    public string Name {get;set;}
    [Required]
    public bool IsComplete {get; set;}

    [Required]
    public int ProjectId {get; set;}
    public Project Project;
}