using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Exceptions
{
    public abstract class NotFoundException(string Message) : Exception(Message)
    {
    }
    public sealed class ProductNotFoundException(int Id) : NotFoundException($"Product With Id {Id} is Not Found") { }
    public sealed class BasketNotFoundException(string Id) : NotFoundException($"Basket With Id {Id} is Not Found") { }

}
