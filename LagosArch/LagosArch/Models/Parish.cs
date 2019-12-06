using System.Collections.Generic;

namespace LagosArch.Models
{
    public class Parish
    {
        public int Id { get; set; }
        public bool IsVisible { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string MapUrl { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ParishPriest { get; set; }
        public int TotalPriests { get; set; }
        public int DeaneryId { get; set; }
        public ICollection<Mass> Masses { get; set; }
        public ICollection<Confession> Confessions { get; set; }
    }
}
