using OutSystems.Model.ExternalLibraries.SDK;
using System;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Defines a calendar event to be shared as a QR code", OriginalName = "CalendarEvent")]
    public struct CalendarEvent {
        [OSStructureField(IsMandatory = true,  Description = "Event title", OriginalName = "Title")]
        public string title;

        [OSStructureField(IsMandatory = true, Description = "Indicates if it is an all day event or not", OriginalName = "IsAllDay")]
        public bool isAllDay;

        [OSStructureField(IsMandatory = true, Description = "Event start date", OriginalName = "StartDateTime")]
        public DateTime startDateTime;

        [OSStructureField(IsMandatory = true, Description = "Event end date", OriginalName = "EndDateTime")]
        public DateTime endDateTime;

        [OSStructureField(Description = "Event location", OriginalName = "Location")]
        public string? location;

        [OSStructureField(Description = "Event description", OriginalName = "Description")]
        public string? description;

        [OSStructureField(Description = "Event class, e.g. PUBLIC or PRIVATE", OriginalName = "Class")]
        public string? eventClass;

        [OSStructureField(Description = "Event organizer's name", OriginalName = "Organizer")]
        public string? organizer;

        [OSStructureField(Description = "Event priority. Value between 1 and 4 is Low priority, 5 is Medium priority, and between 6 and 9 is High priority.", OriginalName = "Priority")]
        public int? priority = 5;

        [OSStructureField(Description = "Show as busy in calendar", OriginalName = "ShowAsBusy")]
        public bool? showAsBusy = true;

        public CalendarEvent(string title, bool isAllDay, DateTime start, DateTime end, string? location = "", 
                            string? description = "", string? eventClass = "", string? organizer = "", int? priority = 5, bool? showAsBusy = true):this(){
            this.title = title;
            this.isAllDay = isAllDay;
            this.startDateTime = start;
            this.endDateTime = end;
            this.location = location;
            this.description = description;
            this.eventClass = eventClass;
            this.organizer = organizer;
            this.priority = priority;
            this.showAsBusy = showAsBusy;
        }

        public CalendarEvent(CalendarEvent e):this() {
            this.title = e.title;
            this.isAllDay = e.isAllDay;
            this.startDateTime = e.startDateTime;
            this.endDateTime = e.endDateTime;
            this.location = e.location;
            this.description = e.description;
            this.eventClass = e.eventClass;
            this.organizer = e.organizer;
            this.priority = e.priority;
            this.showAsBusy = e.showAsBusy;
        }

        public override string ToString() {
            var start = new CalDateTime(this.startDateTime);
            var end = new CalDateTime(this.endDateTime);
            var organizer = this.organizer != "" ? new Organizer { CommonName = this.organizer } : null;

            var e = new Ical.Net.CalendarComponents.CalendarEvent {
                Summary = this.title,
                Start = start,
                End = end,
                Location = this.location,
                Description = this.description,
                IsAllDay = this.isAllDay,
                Class = this.eventClass,
                Organizer = organizer,
                Priority = this.priority ?? 5,
                Transparency = this.showAsBusy == true ? "OPAQUE" : "TRANSPARENT"
            };
            var c = new Calendar();
            c.Events.Add(e);
            return new CalendarSerializer(c).SerializeToString();
        }

    }
}