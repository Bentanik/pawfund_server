﻿using PawFund.Contract.Enumarations.MessagesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public class EventException
    {
        public class EventNotFoundException : NotFoundException
        {
            public EventNotFoundException(Guid Id) : base(string.Format(MessagesList.EventNotFoundException.GetMessage().Message, Id), MessagesList.EventNotFoundException.GetMessage().Code)
            { }
        }

        public class EventNotFoundByStaffException : NotFoundException
        {
            public EventNotFoundByStaffException(Guid Id) : base(string.Format(MessagesList.EventNotFoundByStaffException.GetMessage().Message, Id), MessagesList.EventNotFoundByStaffException.GetMessage().Code)
            { }
        }

        public class EventDateException : BadRequestException
        {
            public EventDateException() : base(MessagesList.CreateEventDateException.GetMessage().Message, MessagesList.CreateEventDateException.GetMessage().Code)
            { }
        }

        public class EventNotFoundByAdminException : NotFoundException
        {
            public EventNotFoundByAdminException() : base((MessagesList.EventNotFoundByAdminException.GetMessage().Message), MessagesList.EventNotFoundByAdminException.GetMessage().Code)
            { }
        }
    }
}
