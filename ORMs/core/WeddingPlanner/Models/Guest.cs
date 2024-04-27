#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace WeddingPlanner.Models;
public class Guest
{
    [Key]
    public int GuestId { get; set; }
    
    public int UserId { get; set; }
    public int WeddingId { get; set; }
    public User? UserAsGuest { get; set; }    
    public Wedding? WeddingGuests { get; set; }

    
}
                
