namespace Server.Models
{
    public class Vaccine
    {
        public int VaccineId { get; set; }
        public string Name { get; set; } 
        public string Manufacturer { get; set; } 
        public int RequiredDoses { get; set; }
    }
}
