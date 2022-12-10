

using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class ReviewModel
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; }
    public int Hours { get; set; }
    public bool Recommend { get; set; }
    public bool IsAccept { get; set; }
    public int? ProductId { get; set; }
    public ProductModel? Product { get; set; }
    public int? AuthorId { get; set; }
    public UserModel? Author { get; set; }
}