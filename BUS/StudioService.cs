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

           movieContext.Studios.Add(studio);
           movieContext.SaveChanges();

        }


    public void UpdateStudio(Studios studio)
        {
            var existingStudio = movieContext.Studios.Find(studio.StudioId); // Tìm studio theo ID
            if (existingStudio == null)
            {
                throw new Exception($"Studio with ID {studio.StudioId} not found"); // In ra ID không tìm thấy
            }

            existingStudio.StudioName = studio.StudioName;
            movieContext.SaveChanges();
        }
    

        public void DeleteStudio(int StudioID)
        {
            var studio = movieContext.Studios.Find(StudioID);
            if (studio == null) throw new Exception("Genre not found");

            movieContext.Studios.Remove(studio);
            movieContext.SaveChanges();
        }
        public bool StudioExists(string StudioName)
        {
            return movieContext.Studios.Any(g => g.StudioName.Equals(StudioName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Studios> GetStudios()
        {
            List<Studios> Hang = new List<Studios>();

            using (var context = new MovieContext())
            {
                Hang = context.Studios.ToList(); 
            }

            return Hang;
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

