using System;
using System.Linq;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Models
{
    public class UserInfo : Persist
    {
        public string UserId { get; set; }

        public string ServerId { get; set; }

        public string FullName { get; set; }

        public string AvatarUri { get; set; }

        public string Email { get; set; }

        public string Department { get; set; }

        public UserInfo UpdateFromDto(UserInfoDto userDto)
        {
            UserId = userDto.Id;
            ServerId = userDto.ServerId;
            FullName = userDto.Name;
            AvatarUri = userDto.Avatar;
            Department = userDto.Department;

            // For fluent interface purposes
            return this;
        }
    }
    
}