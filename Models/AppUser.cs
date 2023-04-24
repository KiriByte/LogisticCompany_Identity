using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace LogisticCompany_Identity.Models;

public class AppUser : IdentityUser
{
    [DisplayName("First Name")]
    public string FirstName { get; set; }
    [DisplayName("Last Name")]
    public string LastName { get; set; }
}