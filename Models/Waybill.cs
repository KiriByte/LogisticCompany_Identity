using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogisticCompany_Identity.Models;

public class Waybill
{
    [Key]
    public int Id { get; set; }

    public int CarId { get; set; }
    public virtual Car Car { get; set; }
    
    public int FreightRequestId { get; set; }
    public virtual FreightRequest FreightRequest { get; set; }
    
    public DateTime DepartureTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    
    [DisplayName("Driver Name")]
    public string DriverName { get; set; }
}