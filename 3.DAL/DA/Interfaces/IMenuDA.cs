using System.Collections.Generic;
using Core.Data.Entities;

namespace DAL.DA.Interfaces
{
    public interface IMenuDA
    {
        List<Menu> GetList();
    }
}
