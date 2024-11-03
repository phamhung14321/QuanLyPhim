using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{

    public class StudioService
    {
        private readonly MovieContext movieContext;
        public StudioService()
        {
            movieContext = new MovieContext();

        }

        public void AddStudio(Studios studio)
        {
            if (StudioExists(studio.StudioName))
            {
                throw new Exception($"Studio '{studio.StudioName}' đã tồn tại trong hệ thống!");
            }
            movieContext.Studios.Add(studio);
           movieContext.SaveChanges();

        }


    public void UpdateStudio(Studios studio)
        {
            var existingStudio = movieContext.Studios.Find(studio.StudioId); 
            if (existingStudio == null)
            {
                throw new Exception($"Không tìm thấy Studio với tên '{studio.StudioName}'");
            }

            existingStudio.StudioName = studio.StudioName;
            movieContext.SaveChanges();
        }
    

        public void DeleteStudio(int StudioID)
        {
            var studio = movieContext.Studios.Find(StudioID);
            if (studio == null) throw new Exception("Không tìm thấy Studio");

            movieContext.Studios.Remove(studio);
            movieContext.SaveChanges();
        }
        public bool StudioExists(string StudioName)
        {
            return movieContext.Studios.Any(g => g.StudioName.Equals(StudioName, StringComparison.OrdinalIgnoreCase));
        }

        public bool StudioExistsByName(string studioName)
        {
            return movieContext.Studios.Any(s => s.StudioName.Equals(studioName, StringComparison.OrdinalIgnoreCase));
        }
        public List<Studios> GetAllStudio()
        {
            return movieContext.Studios.ToList();
        }
    }
}

