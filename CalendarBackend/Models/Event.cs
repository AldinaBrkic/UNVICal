using System;

namespace UNVICal.Models
{
    public class Event
    {
        public int Id { get; set; }             // primarni ključ
        public string Title { get; set; }       // predmet predavanja
        public DateTime Start { get; set; }     // početak
        public DateTime End { get; set; }       // kraj
        public string Link { get; set; }        // Zoom/Teams link
        public string Type { get; set; }        // online/inclass
    }
}
