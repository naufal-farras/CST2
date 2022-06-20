using CST.Data;
using CST.Models.Master;
using CST.ViewModels.Account;
using CST.ViewModels.HelperVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST.Repository
{   

    public class M_User_Repository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<M_User> _userManager;

        public M_User_Repository(AppDbContext context, UserManager<M_User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region CREATE, EDIT, DELETE
        public async Task<StatusHelperVM> CreateUser(RegisterVM registerVM)
        {
            //static variable
            DateTime today = DateTime.Now;            
            string passwordPlainText = "BNI1946";//default password for new user
            var existingUser = new M_User
            {
                Nama = registerVM.Nama,
                NPP = registerVM.NPP,
                UserName = registerVM.NPP,
                JabatanId = registerVM.JabatanId,
                UnitId = registerVM.UnitId,
                StatusPegawai = registerVM.StatusPegawai,
                NormalizedUserName = registerVM.NPP,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                CreatedDate = today,
                IsDelete = false
            };

            StatusHelperVM statusHelperVM = new StatusHelperVM();
            var result = await _userManager.CreateAsync(existingUser, passwordPlainText);

            if (result.Succeeded)
            {
                var createdUser = await _userManager.FindByNameAsync(registerVM.NPP);
                if (createdUser == null)
                {
                    statusHelperVM.StatusCategory = StatusCategory.NotFound;
                    statusHelperVM.Message = "User berhasil ditambahkan, tetapi gagal mendaftarkan ke roles. Username tidak terdaftar";
                }
                else
                {
                    //var inputedUser = _context.Users.Where(x => x.UserName == createdUser.UserName).SingleOrDefault();
                    //inputedUser.CreatedDate = today;
                    //_context.Entry(inputedUser).State = EntityState.Modified;
                    //_context.SaveChanges();


                    var resultRole = await _userManager.AddToRolesAsync(createdUser, registerVM.UserRole);
                    if (resultRole.Succeeded)
                    {
                        statusHelperVM.StatusCategory = StatusCategory.Success;
                        statusHelperVM.Message = "User berhasil ditambahkan";
                    }
                    else
                    {
                        statusHelperVM.StatusCategory = StatusCategory.Failed;
                        statusHelperVM.Message = "User berhasil ditambahkan, tetapi gagal mendaftarkan ke roles. Message : ";
                        foreach (IdentityError error in result.Errors)
                        {
                            statusHelperVM.Message += error.Description + " // ";
                        }
                    }
                }
            }
            else
            {
                statusHelperVM.StatusCategory = StatusCategory.Failed;
                statusHelperVM.Message = "Gagal menambahkan user. Message: ";
                foreach (IdentityError error in result.Errors)
                {
                    statusHelperVM.Message += error.Description + " // ";
                }
            }

            return statusHelperVM;
        }

        public async Task<StatusHelperVM> EditUser(RegisterVM registerVM)
        {
            DateTime today = DateTime.Now;

            StatusHelperVM statusHelperVM = new StatusHelperVM();
            var existingUser = await _userManager.FindByIdAsync(registerVM.Id_user);
            if (existingUser == null)
            {
                statusHelperVM.StatusCategory = StatusCategory.NotFound;
                statusHelperVM.Message = "User tidak ditemukan";
            }
            else
            {
                existingUser.Nama = registerVM.Nama;
                existingUser.NPP = registerVM.NPP;
                existingUser.UserName = registerVM.NPP;
                existingUser.JabatanId = registerVM.JabatanId;
                existingUser.StatusPegawai = registerVM.StatusPegawai;

                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    var inputedUser = _context.Users.Where(x => x.Id == existingUser.Id).SingleOrDefault();
                    inputedUser.UpdatedDate = today;
                    _context.Entry(inputedUser).State = EntityState.Modified;
                    _context.SaveChanges();

                    var getUserRole = await _userManager.GetRolesAsync(existingUser);
                    if (getUserRole != null)
                    {
                        var deletedUserRole = await _userManager.RemoveFromRolesAsync(existingUser, getUserRole);
                    }
                    var resultRole = await _userManager.AddToRolesAsync(existingUser, registerVM.UserRole);
                    if (resultRole.Succeeded)
                    {
                        statusHelperVM.StatusCategory = StatusCategory.Success;
                        statusHelperVM.Message = "User berhasil diupdate";
                    }
                    else
                    {
                        statusHelperVM.StatusCategory = StatusCategory.Failed;
                        statusHelperVM.Message = "User berhasil diupdate, tetapi gagal mendaftarkan ke roles. Message : ";
                        foreach (IdentityError error in result.Errors)
                        {
                            statusHelperVM.Message += error.Description + " // ";
                        }
                    }
                }
                else
                {
                    statusHelperVM.StatusCategory = StatusCategory.Failed;
                    statusHelperVM.Message = "User gagal diupdate. Message: ";
                    foreach (IdentityError error in result.Errors)
                    {
                        statusHelperVM.Message += error.Description + " // ";
                    }
                }
            }

            return statusHelperVM;
        }

        public async Task<StatusHelperVM> DeleteUser(string id)
        {
            StatusHelperVM statusHelperVM = new StatusHelperVM();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                statusHelperVM.StatusCategory = StatusCategory.NotFound;
                statusHelperVM.Message = "User Not Found";
            }
            else
            {
                //var result = await _userManager.DeleteAsync(user);
                var existingUser = _context.Users.Where(x => x.Id == id).SingleOrDefault();
                existingUser.IsDelete = true;
                _context.Update(existingUser);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    statusHelperVM.StatusCategory = StatusCategory.Success;
                    statusHelperVM.Message = "User berhasil dihapus";
                }
                else
                {
                    statusHelperVM.StatusCategory = StatusCategory.Failed;
                    statusHelperVM.Message = "Gagal menghapus user";
                    
                }
            }

            return statusHelperVM;
        }
        #endregion

        #region PROCESS
        public void UpdateLastLogin(string npp)
        {
            DateTime today = DateTime.Now;
            var user = _context.Users.Where(x => x.NPP == npp).SingleOrDefault();
            user.LastLogin = today;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public bool CheckIsDeletedUser(string npp)
        {

            bool result = false;

            var user = _context.Users.Where(x => x.UserName == npp).SingleOrDefault();
            if (user == null)
            {
                result = false;
            }
            else
            {
                result = user.IsDelete;
            }

            return result;
        }
        #endregion

        #region GET DATA
        public UserDataVM GetByNPP(string userNpp)
        {
            UserDataVM result = new UserDataVM();

            var user = _context.M_User
                .Include(x => x.Jabatan)
                .Include(x => x.Unit)
                .Where(x => x.IsDelete == false && x.NPP == userNpp).SingleOrDefault();

            result.Id = user.Id;
            result.UserName = user.UserName;
            result.Nama = user.Nama;
            result.NPP = user.NPP;
            result.Jabatan = user.Jabatan;
            result.JabatanId = user.JabatanId;
            result.Unit = user.Unit;
            result.UnitId = user.UnitId;
            result.StatusPegawai = user.StatusPegawai;
            result.CreatedDate = user.CreatedDate;
            result.UpdatedDate = user.UpdatedDate;
            result.DeletedDate = user.DeletedDate;
            result.IsDelete = user.IsDelete;
            result.LastLogin = user.LastLogin;


            #region FIND USER ROLES                    
            var roles = _userManager.GetRolesAsync(user);
            result.Role = roles.Result;
            #endregion

            return result;
        }
        public List<UserDataVM> GetAll()
        {            
            List<UserDataVM> result = new List<UserDataVM>();

            var user = _context.M_User
                .Include(x => x.Jabatan)
                .Include(x => x.Unit)
                .Where(x => x.IsDelete == false).ToList();

            if (user != null)
            {
                foreach (M_User data in user)
                {
                    UserDataVM tempResult = new UserDataVM();

                    tempResult.Id = data.Id;
                    tempResult.UserName = data.UserName;
                    tempResult.Nama = data.Nama;
                    tempResult.NPP = data.NPP;
                    tempResult.Jabatan = data.Jabatan;
                    tempResult.JabatanId = data.JabatanId;
                    tempResult.Unit = data.Unit;
                    tempResult.UnitId = data.UnitId;
                    tempResult.StatusPegawai = data.StatusPegawai;
                    tempResult.CreatedDate = data.CreatedDate;
                    tempResult.UpdatedDate = data.UpdatedDate;
                    tempResult.DeletedDate = data.DeletedDate;
                    tempResult.IsDelete = data.IsDelete;
                    tempResult.LastLogin = data.LastLogin;


                    #region FIND USER ROLES                    
                    var roles = _userManager.GetRolesAsync(data);
                    tempResult.Role = roles.Result;
                    #endregion

                    result.Add(tempResult);
                }
            }

            return result;
        }
        #endregion
    }
}
