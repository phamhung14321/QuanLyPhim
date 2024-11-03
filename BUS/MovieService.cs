using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using DAL.Entities;
using DAL;

namespace BUS
{
    public class MovieService
    {
        private readonly MovieContext movieContext;
        public MovieService()
        {
            movieContext = new MovieContext();
        }
        public List<object> GetStatisticsByGenre()
        {
            return movieContext.Movies
                .SelectMany(m => m.Genres)
                .GroupBy(g => g.GenreName)
                .Select(g => new
                {
                    Genre = g.Key,
                    Count = g.Count()
                })
                .ToList<object>();
        }


        public List<object> GetStatisticsByYear()
        {
            return movieContext.Movies
                .GroupBy(m => m.ReleaseYear)
                .Select(g => new
                {
                    Year = g.Key,
                    Count = g.Count()
                })
                .ToList<object>();
        }

        public List<Movies> GetAllMovies()
        {
            return movieContext.Movies.ToList();
        }


        public bool IsMovieTitleExists(string title)
        {
            return movieContext.Movies.Any(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }




        public void AddMovie(Movies movie, string imagePath)
        {
            if (IsMovieTitleExists(movie.Title))
            {
                throw new Exception("Tên phim đã tồn tại trong hệ thống!");
            }

            if (!string.IsNullOrEmpty(imagePath))
            {
                movie.ImagePath = SaveImage(imagePath, movie.Title);
            }

            movieContext.Movies.Add(movie);
            movieContext.SaveChanges();
        }
        public Directors FindDirectorByName(string name)
        {
            return movieContext.Directors.FirstOrDefault(d => d.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }


        public Genres FindGenreByName(string name)
        {
            return movieContext.Genres.FirstOrDefault(g => g.GenreName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }


        public Actors FindActorByName(string name)
        {
            return movieContext.Actors.FirstOrDefault(a => a.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Studios FindStudioByName(string name) {
            return movieContext.Studios.FirstOrDefault(s => s.StudioName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateMovie(Movies movie, string imagePath = null)
        {
            var existingMovie = movieContext.Movies.Find(movie.MovieId);
            if (existingMovie == null) throw new Exception("Không tìm thấy phim.");

            if (IsMovieTitleExists(movie.Title) && !existingMovie.Title.Equals(movie.Title, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Tên phim đã tồn tại. Vui lòng nhập tên khác.");
            }


            existingMovie.Title = movie.Title;
            existingMovie.ReleaseYear = movie.ReleaseYear;
            existingMovie.Description = movie.Description;
            if (!string.IsNullOrEmpty(imagePath))
            {
                existingMovie.ImagePath = SaveImage(imagePath, movie.Title);
            }

            movieContext.SaveChanges();
        }

        public void DeleteMovie(int movieId)
        {
            var movie = movieContext.Movies.Find(movieId);
            if (movie == null) throw new Exception("Movie not found");

            movieContext.Movies.Remove(movie);
            movieContext.SaveChanges();
        }

        public List<Movies> SearchMovies(string keyword)
        {
            return movieContext.Movies
                           .Where(m => m.Title.Contains(keyword))
                           .ToList();
        }
        public List<Movies> GetMoviesByGenre(int genreId)
        {
            return movieContext.Movies
                               .Where(m => m.Genres.Any(g => g.GenreId == genreId))
                               .ToList();
        }
        public List<Movies> GetMoviesByYear(int year)
        {
            return movieContext.Movies
                               .Where(m => m.ReleaseYear == year)
                               .ToList();
        }


        public Movies GetMovieById(int movieId)
        {
            return movieContext.Movies.FirstOrDefault(m => m.MovieId == movieId);
        }
        private string SaveImage(string imagePath, string title)
        {
            var imageDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
            var safeTitle = string.Join("_", title.Split(Path.GetInvalidFileNameChars()));
            var fileName = $"{safeTitle}_{Guid.NewGuid()}{Path.GetExtension(imagePath)}";
            var newFilePath = Path.Combine(imageDirectory, fileName);

            try
            {
                File.Copy(imagePath, newFilePath, true);
            }
            catch (IOException ioEx)
            {
                throw new Exception("Lỗi khi sao chép hình ảnh. Kiểm tra lại đường dẫn.", ioEx);
            }
            catch (UnauthorizedAccessException unAuthEx)
            {
                throw new Exception("Không có quyền truy cập để lưu hình ảnh.", unAuthEx);
            }

            return newFilePath;
        }

    }
}
