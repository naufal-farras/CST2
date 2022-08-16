using CST.Data;
using CST.Models.Master;
using CST.ViewModels.HelperVM;
using CST.ViewModels.Rumusan;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CST.Repository
{
    public class M_SubMenu_Repository
    {
        private readonly AppDbContext _context;

        public M_SubMenu_Repository(AppDbContext context)
        {
            _context = context;
        }

        #region PROSES
        public StatusHelperVM Create(M_SubMenu subMenu)
        {
            StatusHelperVM statusHelperVM = new StatusHelperVM();

            var existingData = _context.M_SubBab.Where(x => x.Nama == subMenu.Nama && x.NasabahId == subMenu.NasabahId).SingleOrDefault();
            if (existingData != null)
            {
                statusHelperVM.StatusCategory = StatusCategory.DataExists;
                statusHelperVM.Message = "Sudah ada nama tersebut!";
            }
            else
            {
                try
                {
                    _context.M_SubBab.Add(subMenu);
                    var result = _context.SaveChanges();
                    if (result == 0)
                    {
                        statusHelperVM.StatusCategory = StatusCategory.Failed;
                        statusHelperVM.Message = "Failed to save data";
                    }
                    else
                    {
                        statusHelperVM.StatusCategory = StatusCategory.Success;
                        statusHelperVM.Message = "Data saved successfully";
                    }
                }
                catch (Exception ex)
                {
                    statusHelperVM.StatusCategory = StatusCategory.Error;
                    statusHelperVM.Message = "Error saving data. Message : " + ex.Message;
                }
            }
            return statusHelperVM;
        }

        public StatusHelperVM Update(M_SubMenu subMenu)
        {
            #region VARIABLE CHECKER
            bool isDataFounded = false;
            bool isKodeFounded = false;
            #endregion

            StatusHelperVM statusHelperVM = new StatusHelperVM();

            #region PENGECEKAN
            var existingData = _context.M_SubBab.Where(x => x.Id == subMenu.Id && x.NasabahId == subMenu.NasabahId).SingleOrDefault();
            if (existingData != null)
            {
                isDataFounded = true;
                if (existingData.Nama != subMenu.Nama)//jika ada perubahan nama
                {
                    var checkKode = _context.M_SubBab.Where(x => x.Nama == subMenu.Nama && x.NasabahId == subMenu.NasabahId).SingleOrDefault();
                    if (checkKode == null)
                    {
                        isKodeFounded = false;
                    }
                    else
                    {
                        isKodeFounded = true;
                        statusHelperVM.StatusCategory = StatusCategory.Failed;
                        statusHelperVM.Message = "Sudah ada Nama tersebut!";
                    }
                }
                else
                {
                    isKodeFounded = false;
                }
            }
            else
            {
                isDataFounded = false;
                statusHelperVM.StatusCategory = StatusCategory.Failed;
                statusHelperVM.Message = "Data tidak ada di database!";
            }
            #endregion

            if (isDataFounded)
            {
                if (isKodeFounded)
                {
                    return statusHelperVM;
                }
                else
                {
                    try
                    {
                        existingData.Nama = subMenu.Nama;
                        existingData.NasabahId = subMenu.NasabahId;
                        _context.Entry(existingData).State = EntityState.Modified;
                        var result = _context.SaveChanges();
                        if (result == 0)
                        {
                            statusHelperVM.StatusCategory = StatusCategory.Failed;
                            statusHelperVM.Message = "Failed to save data";
                        }
                        else
                        {
                            statusHelperVM.StatusCategory = StatusCategory.Success;
                            statusHelperVM.Message = "Data saved successfully";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusHelperVM.StatusCategory = StatusCategory.Error;
                        statusHelperVM.Message = "Error saving data. Message : " + ex.Message;
                    }
                    return statusHelperVM;
                }
            }
            else
            {
                return statusHelperVM;
            }
        }

        public StatusHelperVM Delete(int dataId)
        {
            StatusHelperVM result = new StatusHelperVM();
            var getData = _context.M_SubBab.Where(x => x.Id == dataId).SingleOrDefault();
            if (getData == null)
            {
                result.StatusCategory = StatusCategory.NotFound;
                result.Message = "Data tidak ditemukan";
            }
            else
            {
                //getData.isDelete = true;
                _context.Remove(getData);
                var delete = _context.SaveChanges();
                if (delete > 0)
                {
                    result.StatusCategory = StatusCategory.Success;
                    result.Message = "Data berhasil dihapus";
                }
                else
                {
                    result.StatusCategory = StatusCategory.Failed;
                    result.Message = "Data tidak berhasil dihapus";
                }
            }

            return result;
        }
        #endregion

        #region GET DATA
        public List<M_SubMenu> GetAll()
        {
            var result = _context.M_SubBab.Include(x => x.Nasabah).ToList();
            return result;
        }
        public RumusVM GetAllNasabah()
        {
            RumusVM result = new RumusVM();
            result.M_Nasabah = _context.M_Nasabah.ToList();

            return result;
        }

        public M_SubMenu GetById(int id)
        {
            var result = _context.M_SubBab.Where(x => x.Id == id).SingleOrDefault();
            return result;
        }
        #endregion
    }
}
