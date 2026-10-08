using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FirstCart.Models;
using FirstProduct.Models;


namespace Gonzales_Midterm_Store.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{

    public DbSet<Cart> Cart { get; set; }
    public DbSet<Products> Products { get; set; }
    
}
