﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class AdoptApplicationException
    {
        public class AdoptApplicationNotFoundException : NotFoundException
        {
            public AdoptApplicationNotFoundException(Guid Id) : base($"Can not found application with ID: {Id}")
            {
            }
        }
    }
}