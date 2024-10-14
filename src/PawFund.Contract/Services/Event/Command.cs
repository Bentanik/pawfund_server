﻿
using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Event
{
    public static class Command
    {
        public record CreateEventCommand(string Name, DateTime StartDate, DateTime EndDate, string Description, int MaxAttendees, Guid BranchId) : ICommand;
        public record UpdateEventCommand(Guid Id, string Name, DateTime StartDate, DateTime EndDate, string Description, int MaxAttendees, Guid BranchId) : ICommand;
        public record DeleteEventCommand(Guid Id) : ICommand;
        public record ApprovedEventByAdmin(Guid Id) : ICommand;
    }
}
