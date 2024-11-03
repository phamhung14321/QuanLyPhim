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

        // Thống kê số lượng phim theo năm phát hành
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


        // Hàm thêm phim mới
        public bool IsMovieTitleExists(string title)
        {
            return movieContext.Movies.Any(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        // Các hàm khác của MovieService...

        // Hàm thêm phim mới
        public void AddMovie(Movies movie, string imagePath)
        {
            // Kiểm tra nếu tên phim đã tồn tại
            if (IsMovieTitleExists(movie.Title))
            {
                throw new Exception("Tên phim đã tồn tại trong hệ thống!");
            }

            // Kiểm tra và lưu hình ảnh vào thư mục Images
            if (!string.IsNullOrEmpty(imagePath))
            {
                movie.ImagePath = SaveImage(imagePath, movie.Title);
            }

            movieContext.Movies.Add(movie);
            movieContext.SaveChanges();
        }
        // Method to find a director by name
        public Directors FindDirectorByName(string name)
        {
            return movieContext.Directors.FirstOrDefault(d => d.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Method to find a genre by name
        public Genres FindGenreByName(string name)
        {
            return movieContext.Genres.FirstOrDefault(g => g.GenreName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Method to find an actor by name
        public Actors FindActorByName(string name)
        {
            return movieContext.Actors.FirstOrDefault(a => a.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Studios FindStudioByName(string name) {
            return movieContext.Studios.FirstOrDefault(s => s.StudioName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Hàm cập nhật phim
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

            // Nếu có đường dẫn hình ảnh mới, cập nhật
            if (!string.IsNullOrEmpty(imagePath))
            {
                existingMovie.ImagePath = SaveImage(imagePath, movie.Title);
            }

            movieContext.SaveChanges();
        }

        // Hàm xóa phim
        public void DeleteMovie(int movieId)
        {
            var movie = movieContext.Movies.Find(movieId);
            if (movie == null) throw new Exception("Movie not found");

            movieContext.Movies.Remove(movie);
            movieContext.SaveChanges();
        }




        // Hàm tìm kiếm phim
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
        // Hàm lưu ảnh
        private string SaveImage(string imagePath, string title)
        {
            // Sử dụng đường dẫn tương đối từ thư mục gốc của dự án
            var imageDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            // Tạo tên file an toàn với tiêu đề phim
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
