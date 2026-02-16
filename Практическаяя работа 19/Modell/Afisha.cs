using System;

namespace Практическая_19.Modell
{
    public class Afisha
    {
        public int Id { get; set; }
        public int KinoteatrId { get; set; }
        public string KinoteatrName { get; set; }
        public string FilmName { get; set; }
        public DateTime SessionTime { get; set; }
        public decimal TicketPrice { get; set; }
    }
}