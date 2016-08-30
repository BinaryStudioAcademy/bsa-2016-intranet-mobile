using System;
using System.Collections.Generic;
using System.Linq;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Models
{
    public class UserCvs
    {
        public UserCvCvs UserCv { get; set; }

        public UserCvs UpdateFromDto(UserCvsDto userCvsDto)
        {
            UserCv = new UserCvCvs().UpdateFromDto(userCvsDto.UserCv);

            return this;
        }
    }

    public class UserCvCvs
    {
        public List<TechnologyCvs> Technologies { get; } = new List<TechnologyCvs>();

        public UserCvCvs UpdateFromDto(UserCvCvsDto userCvCvsDto)
        {
            if (userCvCvsDto.Technologies != null)
            {
                Technologies.AddRange(
                    userCvCvsDto.Technologies.Select(
                        technologyCvsDto => new TechnologyCvs().UpdateFromDto(technologyCvsDto)));
            }

            return this;
        }
    }

    public class TechnologyCvs
    {
        public CategoryCvs Category { get; set; }

        public string Name { get; set; }

        public int Stars { get; set; }

        public TechnologyCvs UpdateFromDto(TechnologyCvsDto technologyCvsDto)
        {
            Category = new CategoryCvs().UpdateFromDto(technologyCvsDto.Category);
            Name = technologyCvsDto.Name;
            Stars = Convert.ToInt32(technologyCvsDto.Stars);

            return this;
        }
    }

    public class CategoryCvs
    {
        public string Name { get; set; }

        public CategoryCvs UpdateFromDto(CategoryCvsDto categoryCvsDto)
        {
            Name = categoryCvsDto.Name;

            return this;
        }
    }
}