using CST.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CST.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly UserManager<M_User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeveloperController(UserManager<M_User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("Developer/RegisterAccount"), AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            string password = "BNI1946";
            string resultStatus = String.Empty;

            if (ModelState.IsValid)
            {
                var user = new M_User
                {
                    Nama = "Admin Sistem",
                    NPP = "80001",                    
                    JabatanId = 1, //=> pastikan di database sudah ada data Jabatan dengan Id 1
                    UserName = "80001",
                    NormalizedUserName = "80001",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    CreatedDate = DateTime.Now,
                    IsDelete = false,
                    StatusPegawai = StatusPegawai.Definitif
                };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    resultStatus = "Berhasil mendaftarkan user!";
                }
                else
                {
                    resultStatus = "Gagal mendaftarkan user!";
                }

                ViewBag.Message = resultStatus;
            }

            return View("~/Views/Developer/DeveloperTest.cshtml");
        }

        [HttpGet("Developer/CreateRole/{role}")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserRoles(string role)
        {
            string resultStatus = String.Empty;            
            
            var roleCheck = await _roleManager.RoleExistsAsync(role);
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                var result = await _roleManager.CreateAsync(new IdentityRole(role));

                if (result.Succeeded)
                {
                    resultStatus = "Berhasil membuat role " + role + " !";
                }
                else
                {
                    resultStatus = "Gagal mendaftarkan role " + role + " !";
                }
            }
            else
            {
                resultStatus = "Role " + role + " sudah ada didatabase!";
            }

            ViewBag.Message = resultStatus;
            return View("~/Views/Developer/DeveloperTest.cshtml");
        }

        [HttpGet("Developer/User/SetRole/{npp}/{role}")]
        [AllowAnonymous]
        public async Task<IActionResult> AssignUserRoles(string npp, string role)
        {
            string resultStatus = String.Empty;
            string name = npp;

            //Check User 
            var userCheck = await _userManager.FindByNameAsync(name);
            if (userCheck != null)
            {
                //create the roles and seed them to the database
                var result = await _userManager.AddToRoleAsync(userCheck, role);

                if (result.Succeeded)
                {
                    resultStatus = "Berhasil mendaftarkan user " + name + " ke role " + role + " !";
                }
                else
                {
                    resultStatus = "Gagal mendaftarkan user " + name + " ke role " + role + " !";
                }
            }
            else
            {
                resultStatus = "User " + role + " tidak ditemukan!";
            }

            ViewBag.Message = resultStatus;
            return View("~/Views/Developer/DeveloperTest.cshtml");
        }
        
    }
}
