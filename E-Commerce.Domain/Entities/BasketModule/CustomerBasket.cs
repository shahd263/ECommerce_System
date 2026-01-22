using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.BasketModule
{

    // Will be Stored with NoSQL Type (Redis)(Temporary Data)
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;  // GUID : Created from client [Frontend]
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
