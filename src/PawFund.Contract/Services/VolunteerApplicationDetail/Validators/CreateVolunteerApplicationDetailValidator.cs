﻿using FluentValidation;

namespace PawFund.Contract.Services.VolunteerApplicationDetail.Validators
{
    public class CreateVolunteerApplicationDetailValidator : AbstractValidator<Command.CreateVolunteerApplicationDetailCommand> 
    {
        public CreateVolunteerApplicationDetailValidator()
        {
            RuleFor(x => x.eventId).NotEmpty();
            RuleFor(x => x.description).NotEmpty();
            RuleFor(x => x.listActivity).NotEmpty();
        }
    }
}
