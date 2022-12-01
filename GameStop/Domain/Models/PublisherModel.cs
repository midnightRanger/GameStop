using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class PublisherModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public int YearOfFoundation { get; set; }
}