namespace Crossvertise.Calender.BusinessServices.Core.Models
{
    public class EventAttendeeModel
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string AttendeeFullName { get; set; }

        public int? AttendeeOrder { get; set; }
    }
}
