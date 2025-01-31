﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ATS.WEB.Areas.Admin.Pages.Users {
    public class IndexModel : PageModel {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public List<UserViewModel> ApplicationUsers { get;set; }

        public async Task OnGetAsync([FromServices] IOptions<AppSettings> options)
        {
            var users = await _userManager.Users.Where(x => x.Email != options.Value.AdminEmail).ToListAsync();
            ApplicationUsers = new List<UserViewModel>();
            foreach (var user in users) {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                ApplicationUsers.Add(new (){ Id = user.Id, Name = user.Name, Email = user.Email, Role = role});
            }
        }
    }
}
