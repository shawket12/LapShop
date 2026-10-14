
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Resources.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Models;

public partial class TbItem
{
    [ValidateNever]
    public int ItemId { get; set; }
    [Required(
    ErrorMessageResourceType = typeof(ResAdmin),
    ErrorMessageResourceName = "msgItemName")]
    public string ItemName { get; set; } = null!;
    //[Required(
    //ErrorMessageResourceType = typeof(ResAdmin),
    //ErrorMessageResourceName = "msgSalesPrice")]
    //[DataType(DataType.Currency,ErrorMessageResourceType = typeof(ResAdmin),
    //    ErrorMessageResourceName = "msgSalesPrice")]
    //[Range(50,100000, ErrorMessageResourceType = typeof(ResAdmin),
    //    ErrorMessageResourceName = "msgSalesPriceRange")]
    [ValidateNever]
    public decimal SalesPrice { get; set; }
    [Required(
    ErrorMessageResourceType = typeof(ResAdmin),
    ErrorMessageResourceName = "msgPurchasePrice")]
    [DataType(DataType.Currency, ErrorMessageResourceType = typeof(ResAdmin),
        ErrorMessageResourceName = "msgPurchasePrice")]
    [Range(50, 100000, ErrorMessageResourceType = typeof(ResAdmin),
        ErrorMessageResourceName = "msgPurchasePriceRange")]

    public decimal PurchasePrice { get; set; }
    [Required(ErrorMessage = "Please Enter category")]
    public int CategoryId { get; set; }

    public string? ImageName { get; set; }
    [ValidateNever]
    public DateTime CreatedDate { get; set; }
    [ValidateNever]
    public string CreatedBy { get; set; } = null!;
    [ValidateNever]
    public int CurrentState { get; set; } 

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Description { get; set; }

    public string? Gpu { get; set; }

    public string? HardDisk { get; set; }
    [Required(ErrorMessage = "Please Enter ItemType")]

    public int? ItemTypeId { get; set; }

    public string? Processor { get; set; }
    [Required(ErrorMessage = "Please Enter RamSize")]
    [Range(1,500, ErrorMessage = "RamSize must be between 1 and 500 GB")]
    public int? RamSize { get; set; }

    public string? ScreenReslution { get; set; }

    public string? ScreenSize { get; set; }

    public string? Weight { get; set; }
    [Required(ErrorMessage = "Please Enter OS")]
    public int? OsId { get; set; }
    [ValidateNever]
    public virtual TbCategory Category { get; set; } = null!;

    public virtual TbItemType? ItemType { get; set; }

    public virtual TbO? Os { get; set; }

    public virtual ICollection<TbItemDiscount> TbItemDiscounts { get; set; } = new List<TbItemDiscount>();

    public virtual ICollection<TbItemImage> TbItemImages { get; set; } = new List<TbItemImage>();

    public virtual ICollection<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; } = new List<TbPurchaseInvoiceItem>();

    public virtual ICollection<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; } = new List<TbSalesInvoiceItem>();

    public virtual ICollection<TbCustomer> Customers { get; set; } = new List<TbCustomer>();
}
