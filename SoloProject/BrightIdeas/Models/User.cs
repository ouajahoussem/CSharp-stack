#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Filters;
namespace BrightIdeas.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    [Display(Name = "Name:")]
    public string Name { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Alias must be at least 2 characters")]
    [Display(Name = "Alias:")]
    public string Alias { get; set; }
    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [DataType(DataType.Password)]
    [Display(Name = "Password:")]
    public string Password { get; set; }
    // one to many
    public List<Post> IdeasPosted { get; set;} = new List<Post>();
    //  many to many
    public List<Like> PostsLiked { get; set; } = new List<Like>();

    public DateTime CreatedAT { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm PW:")]
    [Compare("Password")]
    public string ConfirmPw { get; set; }
}

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        
        if (value == null)
        {
            
            return new ValidationResult("Email is required!");
        }

        
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        
        if (_context.Users.Any(e => e.Email == value.ToString()))
        {
            
            return new ValidationResult("Email must be unique!");
        }
        else
        {
            
            return ValidationResult.Success;
        }
    }
}