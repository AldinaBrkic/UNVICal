using System;

namespace UNVICal.Models
{
    public class Event
    {
        public int Id { get; set; }                     // primarni ključ
        public string Title { get; set; } = string.Empty; // predmet predavanja
        public DateTime Start { get; set; }             // početak
        public DateTime End { get; set; }               // kraj
        public string Link { get; set; } = string.Empty;  // Zoom/Teams link
        public string Type { get; set; } = string.Empty;  // online/inclass
    }
}
