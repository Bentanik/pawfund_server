﻿namespace PawFund.Domain.Exceptions
{
    public static class AdoptApplicationException
    {
        public class AdoptApplicationNotFoundException : NotFoundException
        {
            public AdoptApplicationNotFoundException(Guid Id) : base($"Can not found application with ID: {Id}")
            {
            }
        }
        public class AdoptApplicationNotBelongToAdopterException : BadRequestException
        {
            public AdoptApplicationNotBelongToAdopterException() : base($"This adopt application does not belong to this adopter!")
            {
            }
        }
    }
}
