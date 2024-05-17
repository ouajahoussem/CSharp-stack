#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace BrightIdeas.Models;
public class MyViewModel
{
    public User LoggedInUser {get;set;}
    public Post newPost {get;set;}
    public Like newLike {get;set;}
    public List<Post> AllPosts {get;set;}
    public Post OnePost {get; set;} 
}