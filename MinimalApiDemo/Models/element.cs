using System.ComponentModel.DataAnnotations;

namespace MinimalApiDemo.Models {

public class Element
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Content { get; set; }
    public bool Validation { get; set; }
}
}
