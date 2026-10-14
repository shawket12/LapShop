using BusinessLayer.Bl.Interfaces;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LapShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly Iitems _iitems;
        public OrderController(Iitems iitems)
        {
            _iitems = iitems;
        }
        [Authorize]
        public IActionResult Cart()
        {
            string cartJson = HttpContext.Request.Cookies["Cart"];
            if(string.IsNullOrEmpty(cartJson))
            {
                return View(new ShoppingCart());
            }
            ShoppingCart cart= JsonConvert.DeserializeObject<ShoppingCart>(cartJson) ?? new ShoppingCart();
            decimal totalPricenew = 0;
            foreach (var item in cart.lstItems)
            {
                item.Total = item.qty * item.Price;
                totalPricenew += item.Total;
            }
            cart.TotalPrice = totalPricenew;
            return View(cart);
        }
        public IActionResult AddToCart(int itemId)
        {
            ShoppingCart cart = new ShoppingCart();
            ShoppingCart existingCart;
            var item = _iitems.GetItemById(itemId);
            if (item == null)
            {
                return NotFound();
            }
            
            var checkCart = HttpContext.Request.Cookies["Cart"];
            if (checkCart == null)
            {
                cart.lstItems.Add(new VmShoppingCartItem
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ImageName = item.ImageName,
                    Price = item.SalesPrice,
                    qty = 1,
                    Total = item.SalesPrice
                });
                HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            }
            else
            {
                existingCart = JsonConvert.DeserializeObject<ShoppingCart>(checkCart);
                var existingItem = existingCart.lstItems.FirstOrDefault(i => i.ItemId == itemId);

                if (existingItem != null)
                {
                    existingItem.qty++;
                    existingItem.Total = existingItem.qty * existingItem.Price;
                    
                    
                }
                else
                {
                    existingCart.lstItems.Add(new VmShoppingCartItem
                    {
                        ItemId = item.ItemId,
                        ItemName = item.ItemName,
                        ImageName = item.ImageName,
                        Price = item.SalesPrice,
                        qty = 1,
                        Total = item.SalesPrice
                    });
                }
                HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(existingCart));
            

            }
           

            return RedirectToAction("Cart");
        }
        public IActionResult RemoveFromCart(int itemId)
        {
            var cartJson = HttpContext.Request.Cookies["Cart"];
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
                if (cart != null)
                {
                    cart.lstItems.RemoveAll(i => i.ItemId == itemId);
                    cart.TotalPrice = cart.lstItems.Sum(i => i.Total);
                    HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
                }
            }
            return RedirectToAction("Cart");
        }
        [HttpPost]
        public IActionResult ChangeQty(int id, int qty)
        {
            var cartJson = HttpContext.Request.Cookies["Cart"];

            if (string.IsNullOrEmpty(cartJson))
            {
                return Json(new
                {
                    success = false,
                    message = "Cart is empty"
                });
            }

            var cart = JsonConvert.DeserializeObject<ShoppingCart>(cartJson);

            if (cart == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid cart"
                });
            }

            var existingItem = cart.lstItems.FirstOrDefault(i => i.ItemId == id);

            if (existingItem == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Item not found"
                });
            }

            existingItem.qty = qty;
            existingItem.Total = existingItem.qty * existingItem.Price;

            decimal totalPrice = 0;

            foreach (var item in cart.lstItems)
            {
                totalPrice += item.Total;
            }

            cart.TotalPrice = totalPrice;

            HttpContext.Response.Cookies.Append(
                "Cart",
                JsonConvert.SerializeObject(cart)
            );

            return Json(new
            {
                success = true,
                message = "Quantity updated successfully",
                receivedId = id,
                receivedQty = qty,
                itemTotal = existingItem.Total,
                totalPrice = cart.TotalPrice
            }); 
        }
        [Authorize]
        public IActionResult OrderSuccess()
        {
            return View();
        }
        public IActionResult MyOrders()
        {
            return View();
        }
        
    }
}
