namespace WatchitAPIs.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public int Duration { get; set; }
        
        public List<string> Genres { get; set; }
        
        public string Language { get; set; }
        
        public float ImdbScore { get; set; }
        
        public string Country { get; set; }
        
        public int Budget { get; set; }
        
        public int Revenue { get; set; }
        
        public string Poster { get; set; }
        
        public string Video  { get; set; }
        
        public string Description { get; set; }
        
        public int Views { get; set; }
        
        public int DirectorId { get; set; }
        
        public Director Director { get; set; }
        
        public List<MovieCast> Cast { get; set; }

        public List<FavoriteMovies> FavoriteMovies { get; set; }
        
        public List<UserWatchRecord>? Records { get; set; }
    }
}
