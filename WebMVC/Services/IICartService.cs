using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Models.CartModels;

namespace WebMVC.Services
{
    public interface IICartService
    {
        Task<Cart> GetCart(ApplicationUser user);
        Task AddItemToCart(ApplicationUser user, CartItem product);
        Task<Cart> UpdateCart(Cart cart);
        Task<Cart> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
        Order MapCartToOrder(Cart cart);
        Task ClearCart(ApplicationUser user);
    }
}
