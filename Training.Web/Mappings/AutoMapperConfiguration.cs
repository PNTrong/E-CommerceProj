using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Training.Model.Models;
using Training.Web.Models;

namespace Training.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<PostTag, PostTagViewModel>();
        }
    }
}