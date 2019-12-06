using System.Collections.Generic;

namespace LagosArch.Models
{
    public class Deanery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSet { get; set; }
        public ICollection<Parish> Parishes { get; set; }
    }

    public class DeaneryGroup : List<Parish>
    {
        public string Name { get; set; }

        public List<Parish> Parishes { get; set; }
        public DeaneryGroup(string name, List<Parish> parishes) : base(parishes)
        {
            Name = name;
            Parishes = parishes;
        }
    }
}
