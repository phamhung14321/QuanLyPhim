using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL;

namespace BUS
{
    public class GenreService
    {
        private readonly MovieContext movieContext;
        public GenreService()
        {
            movieContext = new MovieContext();

        }

        public void AddGenre(Genres genre)
        {
            if (GenreExists(genre.GenreName))
            {
                throw new Exception("Tên thể loại đã tồn tại. Vui lòng nhập tên khác.");
            }
            movieContext.Genres.Add(genre);
            movieContext.SaveChanges();
        }

        public void UpdateGenre(Genres genre)
        {
            var existingGenre = movieContext.Genres.Find(genre.GenreId);
            if (existingGenre == null)
            {
                throw new Exception("Không tìm thấy thể loại.");
            }

            bool isDuplicateName = movieContext.Genres
                .Any(g => g.GenreName.Equals(genre.GenreName, StringComparison.OrdinalIgnoreCase)
                          && g.GenreId != genre.GenreId);

            if (isDuplicateName)
            {
                throw new Exception("Tên thể loại đã tồn tại. Vui lòng nhập tên khác.");
            }


            existingGenre.GenreName = genre.GenreName;
            movieContext.SaveChanges();
        }

        public void DeleteGenre(int genreId)
        {
            var genre = movieContext.Genres.Find(genreId);
            if (genre == null) throw new Exception("Genre not found");

            movieContext.Genres.Remove(genre);
            movieContext.SaveChanges();
        }
        public bool GenreExists(string genreName)
        {
            return movieContext.Genres.Any(g => g.GenreName.Equals(genreName, StringComparison.OrdinalIgnoreCase));
        }


        public List<Genres> GetGenres()
        {
            List<Genres> genres = new List<Genres>();

            using (var context = new MovieContext())
            {
                genres = context.Genres.ToList(); 
            }

            return genres;
        }
        public List<Genres> GetAllGenres()
        {
            return movieContext.Genres.ToList();
        }
    }
}
