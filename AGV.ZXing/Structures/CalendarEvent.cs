using OutSystems.ExternalLibraries.SDK;
using System;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace AGV.ZXing.Structures
{

    [OSStructure(Description = "Defines a calendar event to be shared as a QR code")]
    public struct CalendarEvent
    {
        [OSStructureField(IsMandatory = true, Description = "Event title", Length = 100)]
        public string Title { get; set; } = "";

        [OSStructureField(IsMandatory = true, Description = "Indicates if it is an all day event or not")]
        public bool IsAllDay { get; set; } = false;

        [OSStructureField(IsMandatory = true, Description = "Event start date")]
        public DateTime StartDateTime { get; set; } = DateTime.MinValue;

        [OSStructureField(IsMandatory = true, Description = "Event end date")]
        public DateTime EndDateTime { get; set; } = DateTime.MinValue;

        [OSStructureField(Description = "Event location", Length = 100)]
        public string? Location { get; set; } = null;

        [OSStructureField(Description = "Event description", Length = 2000)]
        public string? Description { get; set; } = null;

        [OSStructureField(Description = "Event class, e.g. PUBLIC or PRIVATE", Length = 20)]
        public string? EventClass { get; set; } = null;

        [OSStructureField(Description = "Event organizer's name", Length = 50)]
        public string? Organizer { get; set; } = null;

        [OSStructureField(Description = "Event priority. Value between 1 and 4 is Low priority, 5 is Medium priority, and between 6 and 9 is High priority.")]
        public int Priority { get; set; } = 5;

        [OSStructureField(Description = "Show as busy in calendar")]
        public bool ShowAsBusy { get; set; } = true;

        public CalendarEvent(string title, bool isAllDay, DateTime start, DateTime end, string? location = "",
                            string? description = "", string? eventClass = "", string? organizer = "", int priority = 5, bool showAsBusy = true) : this()
        {
            this.Title = title;
            this.IsAllDay = isAllDay;
            StartDateTime = start;
            EndDateTime = end;
            this.Location = location;
            this.Description = description;
            this.EventClass = eventClass;
            this.Organizer = organizer;
            this.Priority = priority;
            this.ShowAsBusy = showAsBusy;
        }

        public CalendarEvent(CalendarEvent e) : this()
        {
            Title = e.Title;
            IsAllDay = e.IsAllDay;
            StartDateTime = e.StartDateTime;
            EndDateTime = e.EndDateTime;
            Location = e.Location;
            Description = e.Description;
            EventClass = e.EventClass;
            Organizer = e.Organizer;
            Priority = e.Priority;
            ShowAsBusy = e.ShowAsBusy;
        }

        public override readonly string ToString()
        {
            var start = new CalDateTime(StartDateTime, !IsAllDay);
            var end = new CalDateTime(EndDateTime, !IsAllDay);            
            var e = new Ical.Net.CalendarComponents.CalendarEvent
            {
                Summary = Title,
                Start = start,
                End = end,
                Location = Location,
                Description = Description,
                Class = EventClass,
                Organizer = this.Organizer != "" ? new Organizer { CommonName = this.Organizer } : null,
                Priority = Priority,
                Transparency = ShowAsBusy == true ? "OPAQUE" : "TRANSPARENT",
                Uid = null
            };
            var c = new Calendar();
            c.Events.Add(e);
            var cs = new CalendarSerializer(c);
            return cs.SerializeToString() ?? "";            
        }

    }
}