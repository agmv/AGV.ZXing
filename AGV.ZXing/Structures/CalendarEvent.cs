using OutSystems.Model.ExternalLibraries.SDK;
using System;
using Ical.Net;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Defines a calendar event to be shared as a QR code")]
    public struct CalendarEvent {
        [OSStructureField(DataType = OSDataType.Text, Description = "Event title")]
        public string title;

        [OSStructureField(DataType = OSDataType.Boolean, Description = "Indicates if it is an all day event or not")]
        public bool isAllDay;

        [OSStructureField(DataType = OSDataType.DateTime, Description = "Event start date")]
        public DateTime startDateTime;

        [OSStructureField(DataType = OSDataType.DateTime, Description = "Event end date")]
        public DateTime endDateTime;

        [OSStructureField(DataType = OSDataType.Text, Description = "Event location")]
        public string? location;

        [OSStructureField(DataType = OSDataType.Text, Description = "Event description")]
        public string? description;

        [OSStructureField(DataType = OSDataType.Text, Description = "Event class, e.g. PUBLIC or PRIVATE")]
        public string? eventClass;

        [OSStructureField(DataType = OSDataType.Text, Description = "Event organizaer's name")]
        public string? organizer;

        [OSStructureField(DataType = OSDataType.Integer, Description = "Event priority. Value between 1 and 4 is Low priority, 5 is Medium priority, and between 6 and 9 is High priority.")]
        public int? priority = 5;

        [OSStructureField(DataType = OSDataType.Boolean, Description = "Show as busy in calendar")]
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
            var e = new Ical.Net.CalendarComponents.CalendarEvent {
                Summary = this.title,
                Start = new Ical.Net.DataTypes.CalDateTime(this.startDateTime),
                End = new Ical.Net.DataTypes.CalDateTime(this.endDateTime),
                Location = this.location,
                Description = this.description,
                IsAllDay = this.isAllDay,
                Class = this.eventClass,
                Organizer = this.organizer != "" ? new Ical.Net.DataTypes.Organizer { CommonName = this.organizer } : null,
                Priority = this.priority ?? 5,
                Transparency = this.showAsBusy == true ? "OPAQUE" : "TRANSPARENT"
            };
            var c = new Ical.Net.Calendar();
            c.Events.Add(e);
            var s = new Ical.Net.Serialization.CalendarSerializer();
            return s.SerializeToString(c);
        }

    }
}