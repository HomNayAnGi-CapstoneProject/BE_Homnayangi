using AutoMapper;
using Library.Models;
using Library.Models.DTO.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateCategoryMap();
        }

        private void CreateCategoryMap()
        {
            CreateMap<CreateCategoryRequest, Category>().ReverseMap();
        }
    }
}
