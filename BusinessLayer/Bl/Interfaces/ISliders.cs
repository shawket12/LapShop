using Domains.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Bl.Interfaces
{
    public interface ISliders
    {
        List<TbSlider> GetAll();
        TbSlider GetById(int id);
        bool Save(TbSlider slider);
        // bool Delete(int id);
        void Update(TbSlider slider);
        void Delete(int id);
    }
}
