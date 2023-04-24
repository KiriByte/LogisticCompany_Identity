using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogisticCompany_Identity.Models;

public class Car
{
    [Key]
    public int Id { get; set; }

    public string Model { get; set; }
    public int Year { get; set; }
    [DisplayName("Plate Number")]
    public string PlateNumber { get; set; }
    public int Capacity { get; set; }
}