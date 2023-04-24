using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogisticCompany_Identity.Models;

public class City
{
    [Key]
    public int Id { get; set; }
    [DisplayName("City")]
    public string Name { get; set; }
    public string Country { get; set; }
}