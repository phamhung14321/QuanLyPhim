using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL;

namespace BUS
{
    public class DirectorService
    {
        private readonly MovieContext movieContext;
        public DirectorService()
        {
            movieContext = new MovieContext();
        }
        public bool DirectorExists(string fullName)
        {
            return movieContext.Directors.Any(d => d.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase));
        }
        public void AddDirector(Directors director)
        {
            if (DirectorExists(director.FullName))
            {
                throw new Exception("Tên đạo diễn đã tồn tại. Vui lòng nhập tên khác.");
            }
            movieContext.Directors.Add(director);
            movieContext.SaveChanges();
        }

        public void UpdateDirector(Directors director)
        {
            var existingDirector = movieContext.Directors.Find(director.DirectorId);
            if (existingDirector == null)
            {
                throw new Exception("Không tìm thấy đạo diễn.");
            }

            // Kiểm tra xem tên mới có trùng với một đạo diễn khác không
            bool isDuplicateName = movieContext.Directors
                .Any(d => d.FullName.Equals(director.FullName, StringComparison.OrdinalIgnoreCase)
                          && d.DirectorId != director.DirectorId);

            if (isDuplicateName)
            {
                throw new Exception("Tên đạo diễn đã tồn tại. Vui lòng nhập tên khác.");
            }

            // Cập nhật thông tin đạo diễn
            existingDirector.FullName = director.FullName;
            existingDirector.BirthDate = director.BirthDate;
            movieContext.SaveChanges();
        }

        public void DeleteDirector(int directorId)
        {
            var director = movieContext.Directors.Find(directorId);
            if (director == null) throw new Exception("Director not found");

            movieContext.Directors.Remove(director);
            movieContext.SaveChanges();
        }
        public List<Directors> GetDirectors()
        {
            // Giả sử bạn có một kết nối tới cơ sở dữ liệu và thực hiện truy vấn để lấy danh sách đạo diễn
            List<Directors> directors = new List<Directors>();

            using (var context = new MovieContext())
            {
                directors = context.Directors.ToList(); // Thay đổi 'Directors' thành tên bảng thực tế của bạn
            }

            return directors;
        }
        public List<Directors> GetAllDirectors()
        {
            return movieContext.Directors.ToList();
        }
    }
}
