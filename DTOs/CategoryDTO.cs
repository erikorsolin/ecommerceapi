using System.ComponentModel.DataAnnotations;

public class CategoryDTO
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}