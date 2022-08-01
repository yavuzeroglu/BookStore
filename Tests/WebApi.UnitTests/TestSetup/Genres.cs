using Webapi.DBOperations;
using Webapi.Entities;

namespace TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
            new Genre{ Name = "Personel Growth" },
            new Genre{ Name = "Science Fiction" },
            new Genre{ Name = "Romance" });
        }
    }
}