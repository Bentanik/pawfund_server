﻿using PawFund.Contract.Enumarations.AdoptPetApplication;
using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace PawFund.Domain.Entities
{
    public class AdoptPetApplication : DomainEntity<Guid>
    {
        public AdoptPetApplication()
        {
        }

        public AdoptPetApplication(DateTime? meetingDate, string reasonReject, AdoptPetApplicationStatus status, bool isFinalized, string description, Guid accountId, Guid catId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            MeetingDate = meetingDate;
            ReasonReject = reasonReject;
            Status = status;
            IsFinalized = isFinalized;
            Description = description;
            AccountId = accountId;
            CatId = catId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }

        public DateTime? MeetingDate { get; set; }
        public string? ReasonReject { get; set; }
        public AdoptPetApplicationStatus Status { get; set; } = AdoptPetApplicationStatus.Pending;
        public bool IsFinalized { get; set; } = false;
        public string Description { get; set; } = string.Empty;
        [ForeignKey("AdoptPetApplication_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        [ForeignKey("AdoptPetApplication_Cat")]
        public Guid CatId { get; set; }
        public virtual Cat Cat { get; set; }
        public static AdoptPetApplication CreateAdoptPetApplication
        (DateTime? meetingDate, string? reasonReject, AdoptPetApplicationStatus status, bool isFinalized, string description, Guid accountId, Guid catId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            return new AdoptPetApplication(meetingDate, reasonReject, status, isFinalized, description, accountId, catId, createdDate, modifiedDate, isDeleted);
        }

        public void UpdateAdoptPetApplication(DateTime? meetingDate, string? reasonReject, AdoptPetApplicationStatus status, bool isFinalized, string description, Guid accountId, Guid catId, DateTime? createdDate, DateTime? modifiedDate, bool? isDeleted)
        {
            MeetingDate = meetingDate;
            ReasonReject = reasonReject;
            Status = status;
            IsFinalized = isFinalized;
            Description = description;
            AccountId = accountId;
            CatId = catId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }
    }

}
