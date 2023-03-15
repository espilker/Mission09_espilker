using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_espilker.Models
{
    public class EFCheckoutRepository : ICheckoutRepository
    {
        private BookstoreContext context;
        public EFCheckoutRepository (BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Checkout> Checkouts => context.Checkouts.Include(x => x.LInes).ThenInclude(x => x.Book);

        public void SaveCheckout(Checkout checkout)
        {
            context.AttachRange(checkout.LInes.Select(x => x.Book));

            if (checkout.CheckoutId == 0)
            {
                context.Checkouts.Add(checkout);
            }
            context.SaveChanges();
        }
    }
}
