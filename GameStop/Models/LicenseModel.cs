using System.ComponentModel.DataAnnotations;
using System.Data.Odbc;

namespace GameStop.Models;


public class LicenseModel
{
    
    [Key] 
    public int Id { get; set; }
    
    public string Comment { get; set; }
    public DateTime StartPeriod { get; set; }
    public DateTime EndPeriod { get; set; }
    public int? PublisherId {get; set; }
    public PublisherModel Publisher { get; set; }
}