using BusinessLayer.Bl.Interfaces;
using BusinessLayer.Data;
using Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Bl.Classes
{
    public class Os : IOs
    {
        private readonly LapShopContext _context;
        public Os(LapShopContext context)
        {
            _context = context;
        }
        public List<TbO> GetAllOS()
        {
            try
            {
                List<TbO> categories = _context.TbOs.Where(o => o.CurrentState == 1).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving categories: " + ex.Message);
            }
        }
        public TbO GetOsById(int OsId)
        {
            try
            {
                var os = _context.TbOs.FirstOrDefault(c => c.OsId == OsId && c.CurrentState == 1);
                return os;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving os: " + ex.Message);
            }
        }
        public bool Save(TbO os)
        {
            try
            {
                
                if (os.OsId == 0)
                {
                    os.CreatedBy = "1";
                    os.CreatedDate = DateTime.Now;
                    var model = _context.TbOs.Add(os);

                }
                else
                {
                    //var cat = context.TbOs.FirstOrDefault(c => c.OsId == os.OsId);
                    os.UpdatedBy = "1";
                    os.UpdatedDate = DateTime.Now;
                    _context.Entry(os).State = Microsoft.
                        EntityFrameworkCore.EntityState.Modified;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving os: " + ex.Message);


            }
        }
        public bool Delete(int OsId)
        {
            try
            {
                var os = GetOsById(OsId);
                if (os != null)
                {
                    os.CurrentState = 0;
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("os not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting os: " + ex.Message);
            }
        }
    }
}
