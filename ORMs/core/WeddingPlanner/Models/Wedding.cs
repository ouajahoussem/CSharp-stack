#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace WeddingPlanner.Models;
public class Wedding
{
    [Key]
    public int WeddingId { get; set; }
    [Required]
    [Display(Name = "Wedder One")]
    public string WedderOne { get; set; } 
    [Required]
    [Display(Name = "Wedder two")]
    public string Weddertwo { get; set; }
    [Required]
    [FutureDate]
    [DisplayFormat(DataFormatString = "{MMM/dd/yyyy}")]
    public DateTime Date { get; set; }
    [Required]
    public string Address { get; set; }
    // one to many
    public int UserId { get; set; }
    public User? Wedders {get; set; }
    //  many to many 
    public List<Guest> UserAttending { get; set; } = new List<Guest>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}



public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if((DateTime)value < DateTime.Now)
        {
            return new ValidationResult("Wedding date must be in the future!");
        } else {
            return ValidationResult.Success;
        }
    }
}
                
