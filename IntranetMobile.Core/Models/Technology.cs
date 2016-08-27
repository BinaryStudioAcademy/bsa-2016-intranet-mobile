using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Models
{
    public class Category
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Id { get; set; }

        public Category UpdateFromDto(CategoryRootDto categoryRootDto)
        {
            Name = categoryRootDto.Name;
            IsDeleted = categoryRootDto.IsDeleted;
            // TODO: Use DateTime?
            CreatedAt = categoryRootDto.CreatedAt;
            // TODO: Use DateTime?
            UpdatedAt = categoryRootDto.UpdatedAt;
            Id = categoryRootDto.Id;

            return this;
        }
    }

    public class Technology
    {
        public Category Category { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Id { get; set; }

        public Technology UpdateFromDto(TechnologyRootDto technologyDto)
        {
            Category = new Category().UpdateFromDto(technologyDto.Category);
            Name = technologyDto.Name;
            IsDeleted = technologyDto.IsDeleted;
            // TODO: Use DateTime?
            CreatedAt = technologyDto.CreatedAt;
            // TODO: Use DateTime?
            UpdatedAt = technologyDto.UpdatedAt;
            Id = technologyDto.Id;

            return this;
        }
    }
}