﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.AdoptApplications.Validators
{
    public class UpdateAdoptApplicationValidator : AbstractValidator<Command.UpdateAdoptApplicationCommand>
    {
        public UpdateAdoptApplicationValidator()
        {
            RuleFor(x => x.AdoptId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}