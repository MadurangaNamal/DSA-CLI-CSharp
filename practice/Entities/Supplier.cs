using System.ComponentModel.DataAnnotations;

namespace Practice.Entities;

public class Supplier
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(150)]
    public required string Name { get; set; }

    [MaxLength(11)]
    [Phone]
    public string? Phone { get; set; }

    public string Address { get; set; } = string.Empty;

    public List<Item> Items { get; set; } = [];
}
