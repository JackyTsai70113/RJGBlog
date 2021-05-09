using Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DA.Interfaces
{
    public interface IMenuDA
    {
        List<Menu> GetList();
    }
}
