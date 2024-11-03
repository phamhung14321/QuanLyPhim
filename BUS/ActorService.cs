using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL;

namespace BUS
{
    public class ActorService
    {
        private readonly MovieContext movieContext;
        public ActorService()
        {
            movieContext = new MovieContext();
        }
        public bool ActorExists(string fullName)
        {
            return movieContext.Actors.Any(a => a.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase));
        }
        public void AddActor(Actors actor)
        {
            movieContext.Actors.Add(actor);
            movieContext.SaveChanges();
        }

        public void UpdateActor(Actors actor)
        {
            var existingActor = movieContext.Actors.Find(actor.ActorId);
            if (existingActor == null) throw new Exception("Actor not found");

            existingActor.FullName = actor.FullName;
            existingActor.BirthDate = actor.BirthDate;
            movieContext.SaveChanges();
        }

        public void DeleteActor(int actorId)
        {
            var actor = movieContext.Actors.Find(actorId);
            if (actor == null) throw new Exception("Actor not found");

            movieContext.Actors.Remove(actor);
            movieContext.SaveChanges();
        }
        public List<Actors> GetActors()
        {
            List<Actors> actors = new List<Actors>();

            using (var context = new MovieContext())
            {
                actors = context.Actors.ToList(); // Lấy danh sách diễn viên từ cơ sở dữ liệu
            }

            return actors;
        }
        public List<Actors> GetAllActors()
        {
            return movieContext.Actors.ToList();
        }
    }
}
