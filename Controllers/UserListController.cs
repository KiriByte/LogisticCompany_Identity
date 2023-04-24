using LogisticCompany_Identity.Data;
using LogisticCompany_Identity.Models;
using LogisticCompany_Identity.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LogisticCompany_Identity.Controllers;

[Authorize(Roles = "admin,employee")]
public class UserListController : Controller
{
    ApplicationDbContext _context;
    UserManager<AppUser>? _userManager;

    public UserListController(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize(Roles = "admin,employee")]
    public async Task<IActionResult> Index()
    {
        if (_context.Users == null)
        {
            return NotFound();
        }

        List<UserWithRole> listuserswithroles = new List<UserWithRole>();
        var users = _context.Users.ToList();

        foreach (var user in users)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault();
            listuserswithroles.Add(new UserWithRole()
            {
                User = user,
                Role = userRole
            });
        }

        return View(listuserswithroles);
    }

    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null || _context.Users == null)
        {
            return NotFound();
        }

        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }


        var userRoles = await _userManager.GetRolesAsync(user);
        var userRole = userRoles.FirstOrDefault();
        var profiteroles = new UserWithRole
        {
            User = user,
            Role = userRole
        };
        return View(profiteroles);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<RedirectToActionResult> Edit(string id, string role)
    {
        var user = _context.Users.Find(id);
        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);
        await _userManager.AddToRoleAsync(user, role);

        return RedirectToAction("Index");
    }
}