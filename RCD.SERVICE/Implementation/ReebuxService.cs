using RCD.DATA.Entity;
using RCD.REPO;
using RCD.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Implementation
{
   public class ReebuxService : IReebuxService
    {
        private readonly IRepository<Reebux> ReebuxRepository;
        public ReebuxService(IRepository<Reebux> ReebuxRepository)
        {
            this.ReebuxRepository = ReebuxRepository;
        }

        public void DeleteReebux(int id)
        {
            Reebux Reebux = ReebuxRepository.Get(id);
            ReebuxRepository.Remove(Reebux);

        }

        public Reebux GetReebux(int id)
        {
            return ReebuxRepository.Get(id);
        }

        public IEnumerable<Reebux> GetReebuxs()
        {
            return ReebuxRepository.GetAll();
        }

        public void InsertReebux(Reebux Reebux)
        {
            ReebuxRepository.Insert(Reebux);
        }

        public void UpdateReebux(Reebux Reebux)
        {
            ReebuxRepository.Update(Reebux);
        }
    }
}

