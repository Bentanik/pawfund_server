﻿namespace PawFund.Contract.DTOs.EventActivity;

public class GetEventActivityByIdDTO
{
    public class ActivityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public int NumberOfVolunteer { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public EventDTO Event { get; set; }
    }
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int MaxAttendees { get; set; }
        public string Status { get; set; }
    }
}
