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
    public class M_SubSubMenu_Repository
    {
        private readonly AppDbContext _context;

        public M_SubSubMenu_Repository(AppDbContext context)
        {
            _context = context;
        }

        #region PROSES
        public StatusHelperVM Create(M_SubSubMenu subSubMenu)
        {
            StatusHelperVM statusHelperVM = new StatusHelperVM();

            var existingData = _context.M_SubSubBab.Where(x => x.Nama == subSubMenu.Nama && x.NasabahId == subSubMenu.NasabahId).SingleOrDefault();
            if (existingData != null)
            {
                statusHelperVM.StatusCategory = StatusCategory.DataExists;
                statusHelperVM.Message = "Sudah ada nama tersebut!";
            }
            else
            {
                try
                {
                    _context.M_SubSubBab.Add(subSubMenu);
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

        public StatusHelperVM Update(M_SubSubMenu subSubMenu)
        {
            #region VARIABLE CHECKER
            bool isDataFounded = false;
            bool isKodeFounded = false;
            #endregion

            StatusHelperVM statusHelperVM = new StatusHelperVM();

            #region PENGECEKAN
            var existingData = _context.M_SubSubBab.Where(x => x.Id == subSubMenu.Id).SingleOrDefault();
            if (existingData != null)
            {
                isDataFounded = true;
                if (existingData.Nama != subSubMenu.Nama)//jika ada perubahan nama
                {
                    var checkKode = _context.M_SubSubBab.Where(x => x.Nama == subSubMenu.Nama).SingleOrDefault();
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
                        existingData.Nama = subSubMenu.Nama;
                        existingData.NasabahId = subSubMenu.NasabahId;
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
            var getData = _context.M_SubSubBab.Where(x => x.Id == dataId).SingleOrDefault();
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
        public List<M_SubSubMenu> GetAll()
        {
            var result = _context.M_SubSubBab.Include(x => x.Nasabah).ToList();
            return result;
        }

        public RumusVM GetAllNasabah()
        {
            RumusVM result = new RumusVM();
            result.M_Nasabah = _context.M_Nasabah.ToList();

            return result;
        }

        public M_SubSubMenu GetById(int id)
        {
            var result = _context.M_SubSubBab.Where(x => x.Id == id).SingleOrDefault();
            return result;
        }
        #endregion
    }
}
