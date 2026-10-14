using Domains.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Bl.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TbItem> Items { get; }
        IGenericRepository<TbCategory> Categories { get; }
        IGenericRepository<TbSlider> Sliders { get; }
        IGenericRepository<TbSalesInvoice> SalesInvoices { get; }
        IGenericRepository<TbItemType> ItemsType { get; }
        IGenericRepository<TbO> OS { get; }


        // دالة حفظ جميع التغييرات لمرة واحدة
        Task<int> CompleteAsync();


    }
}
