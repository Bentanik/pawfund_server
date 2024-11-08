﻿using PawFund.Contract.Enumarations.Cat;
using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class Cat : DomainEntity<Guid>
    {
        public Cat() { }

        public Cat(CatSex sex, string name, string age, string breed, decimal weight, string color, string description, Guid branchId, bool sterilization)
        {
            Sex = sex;
            Name = name;
            Age = age;
            Breed = breed;
            Weight = weight;
            Color = color;
            Description = description;
            BranchId = branchId;
            Sterilization = sterilization;
            IsDeleted = false;
        }

        public CatSex Sex { get; set; } = CatSex.Male;
        public string Name { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Breed {  get; set; } = string.Empty;
        public decimal Weight { get; set; } = 0;
        public string Color {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Sterilization { get; set; }

        [ForeignKey("Cat_Branch")]
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<AdoptPetApplication> AdoptPetApplications { get; set; }
        public virtual ICollection<HistoryCat> HistoryCats { get; set; }
        public virtual ICollection<ImageCat> ImageCats { get; set; }

        public static Cat CreateCat(CatSex sex, string name, string age, string breed, decimal weight, string color, string description, Guid branchId, bool sterilization)
        {
            return new Cat(sex, name, age, breed, weight, color, description, branchId, sterilization);
        }

        public void UpdateCat(CatSex sex, string name, string age, string breed, decimal weight, string color, string description, bool sterilization)
        {
            Sex = sex;
            Name = name;
            Age = age;
            Breed = breed;
            Weight = weight;
            Color = color;
            Description = description;
            Sterilization = sterilization;
        }

        public void DeleteCat()
        {
            IsDeleted = true;
        }
    }
}
