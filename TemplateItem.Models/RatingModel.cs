using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TemplateItem.Models
{
    [Table("RatingIulianAndrei")]
    public partial class RatingModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public int ConferenceId { get; set; }
        public string AttendeeEmail { get; set; }

    }
}
