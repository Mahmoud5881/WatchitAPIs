using WatchitAPIs.Models;

namespace WatchitAPIs.DTOs
{
    public class MovieWithCastAndDirectorDTO
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

        public string Video { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        public DirectorDTO Director { get; set; }

        public List<CastDTO> Cast { get; set; }
    }
    public class DirectorDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class CastDTO 
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
