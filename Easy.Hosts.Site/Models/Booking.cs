using Easy.Hosts.Site.Models.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Easy.Hosts.Site.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Código da Reserva")]
        public string CodeBooking { get; set; }

        [DisplayName("Nome do hóspede")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [DisplayName("Data de check-in")]
        public DateTime DateCheckin { get; set; }

        [DisplayName("Data de check-out")]
        public DateTime DateCheckout { get; set; }

        [DisplayName("Valor da Reserva")]
        public decimal ValueBooking { get; set; }

        [DisplayName("Nome do quarto")]
        public int BedroomId { get; set; }
        public virtual Bedroom Bedroom { get; set; }

        public BookingStatus Status { get; set; }
    }
}