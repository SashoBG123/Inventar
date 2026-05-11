using Microsoft.AspNetCore.Identity;

namespace aspBiznes.Data
{
    public class Client:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateRegister { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
