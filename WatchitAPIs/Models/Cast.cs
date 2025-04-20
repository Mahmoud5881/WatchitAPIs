namespace WatchitAPIs.Models;

public class Cast
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Nationality { get; set; }
    
    public int Age { get; set; }
    
    public string Gender { get; set; }
    
    public List<MovieCast> Movies { get; set; }
}