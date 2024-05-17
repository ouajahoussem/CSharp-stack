#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace BeltExam.Models;

public class MyContext : DbContext 
{   
    
    public MyContext(DbContextOptions options) : base(options) { }    

    public DbSet<User> Users { get; set; } 
    public DbSet<Recipe> Recipes { get; set; } 
    public DbSet<Rate> Rates { get; set; } 


    

    

}