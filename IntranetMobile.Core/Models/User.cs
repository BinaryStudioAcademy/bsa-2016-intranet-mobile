using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Models
{
    public class User : Persist
    {
        public string UserId { get; set; }

        public UserPdp Pdp { get; set; }

        public UserCv Cv { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime Birthday { get; set; }

        public string AvatarUri { get; set; }

        public string PositionId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Gender { get; set; }

        public DateTime HireDate { get; set; }

        #region Appended properties

        public string FullName => $"{FirstName} {LastName}";

        #endregion

        public User UpdateFromDto(UserDto userDto)
        {
            UserId = userDto.ServerUserId;
            FirstName = userDto.Name;
            LastName = userDto.Surname;
            Email = userDto.Email;
            Password = userDto.Password;
            Birthday = DateTime.Parse(userDto.Birthday);
            AvatarUri = userDto.Avatar.UrlAva;
            PositionId = userDto.Position;
            Country = userDto.Country;
            City = userDto.City;
            Gender = userDto.City;
            HireDate = DateTime.Parse(userDto.WorkDate);
            Pdp = new UserPdp().UpdateFromDto(userDto.UserPdp);
            Cv = new UserCv().UpdateFromDto(userDto.UserCv);
            
            // For fluent interface purposes
            return this;
        }
    }

    public class UserPdp
    {
        public string Position { get; set; }

        public string Direction { get; set; }

        // TODO: Not used yet
        public object[] Tests { get; set; }

        public Technology2[] Technologies { get; set; }
        public List<Achievement> Achievements { get;} = new List<Achievement>();
        public List<Certification> Certifications { get;} = new List<Certification>();

        public bool IsDeleted { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Id { get; set; }

        public UserPdp UpdateFromDto(UserPdpDto userPdpDto)
        {
            Position = userPdpDto.Position;
            Direction = userPdpDto.Direction;
           
            Technologies = userPdpDto.Technologies.Select(tech => new Technology2().UpdateFromDto(tech)).ToArray();
           
                if (userPdpDto.CompletedCertifications != null)
                    Certifications.AddRange(userPdpDto.CompletedCertifications.Select(cer => new Certification().UpdateFromDto(cer)));
                if (userPdpDto.Achievements != null)
                    Achievements.AddRange(userPdpDto.Achievements.Select(ach => new Achievement().UpdateFromDto(ach)));

            IsDeleted = userPdpDto.IsDeleted;
            // TODO: Use DateTime?
            CreatedAt = userPdpDto.CreatedAt;
            // TODO: Use DateTime?
            UpdatedAt = userPdpDto.UpdatedAt;
            Id = userPdpDto.Id;

            // For fluent interface purposes
            return this;
        }
    }

    public class Achievement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public Achievement UpdateFromDto(AchievementDto achievementDto)
        {
            Id = achievementDto.Id;
            Name = achievementDto.Name;
            //TODO: when categories will be noraml try catch can be removed
            Category = Category!=null ? achievementDto.Category.Name : "NULL";

            return this;
        }
    }

    public class Certification
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public Certification UpdateFromDto(CompletedCertificationDto completedCertificationDto)
        {
            Id = completedCertificationDto.Id;
            Name = completedCertificationDto.Name;
            Category = Category != null ? completedCertificationDto.Category.Name : "NULL";
            return this;
        }
    }

    public class Technology2
    {
        public string Id { get; set; }

        public bool Completed { get; set; }

        public string Name { get; set; }

        public Technology2 UpdateFromDto(Technology2Dto technology2Dto)
        {
            Id = technology2Dto.Id;
            Completed = technology2Dto.Completed;
            Name = technology2Dto.Name;

            // For fluent interface purposes
            return this;
        }
    }

    public class UserCv
    {
        public object[] Projects { get; set; }

        public bool IsDeleted { get; set; }

        public UserTechnology[] UserTechnologies { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Id { get; set; }

        public UserCv UpdateFromDto(UserCvDto userCvDto)
        {
            Projects = (object[]) userCvDto.Projects.Clone();
            IsDeleted = userCvDto.IsDeleted;
            UserTechnologies = userCvDto.Technologies.Select(tech => new UserTechnology().UpdateFromDto(tech)).ToArray();
            // TODO: Use DateTime?
            CreatedAt = userCvDto.CreatedAt;
            // TODO: Use DateTime?
            UpdatedAt = userCvDto.UpdatedAt;
            Id = userCvDto.Id;

            // For fluent interface purposes
            return this;
        }
    }

    public class UserTechnology
    {
        public string TechnologyId { get; set; }

        public int Stars { get; set; }

        public UserTechnology UpdateFromDto(TechnologyDto technologyDto)
        {
            TechnologyId = technologyDto.UserTech;

            int stars;
            if (int.TryParse(technologyDto.Stars, out stars))
                Stars = stars;

            // For fluent interface purposes
            return this;
        }
    }
}