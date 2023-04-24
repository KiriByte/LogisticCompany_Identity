using LogisticCompany_Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace LogisticCompany_Identity.ViewModel;

public class UserWithRole
{
    public AppUser User { get; set; }
    public string Role { get; set; }
}