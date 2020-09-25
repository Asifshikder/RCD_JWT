using RCD.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Interface
{
    public interface IReebuxService
    {
        IEnumerable<Reebux> GetReebuxs();
        Reebux GetReebux(int id);
        void InsertReebux(Reebux Reebux);
        void UpdateReebux(Reebux Reebux);
        void DeleteReebux(int id);
    }
}
