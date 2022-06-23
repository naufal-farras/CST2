using CST.Data;
using CST.Models.Master;
using CST.ViewModels.HelperVM;
using CST.ViewModels.Nasabah;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CST.Repository
{
    public class M_Nasabah_Repository
    {
        private readonly AppDbContext _context;

        public M_Nasabah_Repository(AppDbContext context)
        {
            _context = context;
        }

        #region PROSES
        public StatusHelperVM Create(M_Nasabah nasabah)
        {
            StatusHelperVM statusHelperVM = new StatusHelperVM();

            var existingData = _context.M_Nasabah.Where(x => x.KodeNasabah == nasabah.KodeNasabah).SingleOrDefault();
            if (existingData != null)
            {
                statusHelperVM.StatusCategory = StatusCategory.DataExists;
                statusHelperVM.Message = "Sudah ada kode tersebut!";
            }
            else
            {

                try
                {
                    _context.M_Nasabah.Add(nasabah);
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

        public StatusHelperVM Update(M_Nasabah nasabah)
        {
            #region VARIABLE CHECKER
            bool isDataFounded = false;
            bool isKodeFounded = false;
            #endregion

            StatusHelperVM statusHelperVM = new StatusHelperVM();

            #region PENGECEKAN
            var existingData = _context.M_Nasabah.Where(x => x.Id == nasabah.Id).SingleOrDefault();
            if (existingData != null)
            {
                isDataFounded = true;
                if (existingData.KodeNasabah != nasabah.KodeNasabah)//jika ada perubahan nama
                {
                    var checkKode = _context.M_Nasabah.Where(x => x.KodeNasabah == nasabah.KodeNasabah).SingleOrDefault();
                    if (checkKode == null)
                    {
                        isKodeFounded = false;
                    }
                    else
                    {
                        isKodeFounded = true;
                        statusHelperVM.StatusCategory = StatusCategory.Failed;
                        statusHelperVM.Message = "Sudah ada Kode tersebut!";
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
                        existingData.KodeNasabah = nasabah.KodeNasabah;
                        existingData.Nama = nasabah.Nama;
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
            var getDataBab = _context.M_Bab.Where(x => x.NasabahId == dataId).ToList();
            var getDataSubBab = _context.M_SubBab.Where(x => x.NasabahId == dataId).ToList();
            var getDataSubSubBab = _context.M_SubSubBab.Where(x => x.NasabahId == dataId).ToList();

            if (getDataBab.Count() !=0)
            {
                foreach (var item in getDataBab)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
                
            }
            if (getDataSubBab.Count() != 0)
            {
                foreach (var item2 in getDataSubBab)
                {
                    _context.Remove(item2);
                    _context.SaveChanges();
                }
            }
            if (getDataSubSubBab.Count != 0)
            {
                foreach (var item3 in getDataSubSubBab)
                {
                    _context.Remove(item3);
                    _context.SaveChanges();
                }
            }

            StatusHelperVM result = new StatusHelperVM();
            var getData = _context.M_Nasabah.Where(x => x.Id == dataId).SingleOrDefault();
            
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
        public List<M_Nasabah> GetAll()
        {
            var result = _context.M_Nasabah.ToList();
            return result;
        }

        public InputNasabahVM GetById(int id)
        {
            var getData = _context.M_Nasabah.Where(x => x.Id == id).SingleOrDefault();

            InputNasabahVM result = new InputNasabahVM();

            result.Id = getData.Id;
            result.KodeNasabah = getData.KodeNasabah;
            result.NamaNasabah = getData.Nama;

            return result;
        }

        #endregion
    }
}
