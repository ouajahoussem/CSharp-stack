#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;

public class Chef
{
    [Key]
    public int ChefId { get; set; }
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Required]
    [Display(Name = "Date of Birth")]
    [FutureDate(ErrorMessage ="Birthday must be in the past!")]
    [MinimumAge(18, ErrorMessage = "Chef must be at least 18 years old.")]
    public DateTime BirthDate { get; set; }
    
    public int Age
    {
        get
        {
            // Calculate age based on BirthDate
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }
    public List<Dish> ChefDishes { get; set; } = new List<Dish>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}




public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if((DateTime)value > DateTime.Now)
        {
            return new ValidationResult("Birthday must be in the past!");
        } else {
            return ValidationResult.Success;
        }
    }
}

public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
                age--;

            if (age < _minimumAge)
            {
                return new ValidationResult($"Chef must be at least {_minimumAge} years old.");
            }
        }

        return ValidationResult.Success;
    }
}
