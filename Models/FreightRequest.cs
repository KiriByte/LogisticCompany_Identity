using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticCompany_Identity.Models;

public class FreightRequest
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("User")]
    public string UserId { get; set; }
    public virtual AppUser User { get; set; }
    public string DepartureCity { get; set; }
    public string TargetCity { get; set; }
    public float Weight { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; }
}