using System.Text.Json.Serialization;

namespace EstrelinhasAPI.Entidades
{
    public class User
    {
        public int UserId { get; set; }
        public string? Nome { get; set; }
        public int Stars { get; set; }
        public int PurpleStars { get; set; }
    }
}
