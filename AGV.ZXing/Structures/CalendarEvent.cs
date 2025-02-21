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
        public string title;

        [OSStructureField(IsMandatory = true, Description = "Indicates if it is an all day event or not")]
        public bool isAllDay;

        [OSStructureField(IsMandatory = true, Description = "Event start date")]
        public DateTime startDateTime;

        [OSStructureField(IsMandatory = true, Description = "Event end date")]
        public DateTime endDateTime;

        [OSStructureField(Description = "Event location", Length = 100)]
        public string? location;

        [OSStructureField(Description = "Event description", Length = 2000)]
        public string? description;

        [OSStructureField(Description = "Event class, e.g. PUBLIC or PRIVATE", Length = 20)]
        public string? eventClass;

        [OSStructureField(Description = "Event organizer's name", Length = 50)]
        public string? organizer;

        [OSStructureField(Description = "Event priority. Value between 1 and 4 is Low priority, 5 is Medium priority, and between 6 and 9 is High priority.")]
        public int priority = 5;

        [OSStructureField(Description = "Show as busy in calendar")]
        public bool showAsBusy = true;

        public CalendarEvent(string title, bool isAllDay, DateTime start, DateTime end, string? location = "",
                            string? description = "", string? eventClass = "", string? organizer = "", int priority = 5, bool showAsBusy = true) : this()
        {
            this.title = title;
            this.isAllDay = isAllDay;
            startDateTime = start;
            endDateTime = end;
            this.location = location;
            this.description = description;
            this.eventClass = eventClass;
            this.organizer = organizer;
            this.priority = priority;
            this.showAsBusy = showAsBusy;
        }

        public CalendarEvent(CalendarEvent e) : this()
        {
            title = e.title;
            isAllDay = e.isAllDay;
            startDateTime = e.startDateTime;
            endDateTime = e.endDateTime;
            location = e.location;
            description = e.description;
            eventClass = e.eventClass;
            organizer = e.organizer;
            priority = e.priority;
            showAsBusy = e.showAsBusy;
        }

        public override readonly string ToString()
        {
            var start = new CalDateTime(startDateTime);
            var end = new CalDateTime(endDateTime);
            var organizer = this.organizer != "" ? new Organizer { CommonName = this.organizer } : null;

            var e = new Ical.Net.CalendarComponents.CalendarEvent
            {
                Summary = title,
                Start = start,
                End = end,
                Location = location,
                Description = description,
                IsAllDay = isAllDay,
                Class = eventClass,
                Organizer = organizer,
                Priority = priority,
                Transparency = showAsBusy == true ? "OPAQUE" : "TRANSPARENT",
                Uid = null
            };
            var c = new Calendar();
            c.Events.Add(e);
            return new CalendarSerializer(c).SerializeToString();
        }

    }
}