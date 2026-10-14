using BusinessLayer.Bl.Interfaces;
using BusinessLayer.Data;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Bl.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LapShopContext _context;

        public IGenericRepository<TbItem> Items { get; private set; }
        public IGenericRepository<TbCategory> Categories { get; private set; }
        public IGenericRepository<TbSlider> Sliders { get; private set; }
        public IGenericRepository<TbSalesInvoice> SalesInvoices { get; private set; }

        public IGenericRepository<TbItemType> ItemsType { get; private set; }

        public IGenericRepository<TbO> OS { get; private set; }

        public UnitOfWork(LapShopContext context)
        {
            _context = context;

            // تهيئة الـ Repositories
            Items = new GenericRepository<TbItem>(_context);
            Categories = new GenericRepository<TbCategory>(_context);
            Sliders = new GenericRepository<TbSlider>(_context);
            SalesInvoices = new GenericRepository<TbSalesInvoice>(_context);
            ItemsType = new GenericRepository<TbItemType>(_context);
            OS = new GenericRepository<TbO>(_context);

        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
