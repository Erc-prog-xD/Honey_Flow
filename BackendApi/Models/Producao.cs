using System;

namespace BackendApi.Models
{
    public class Producao
    {
        public int Id { get; set; }
        public required User User { get; set; }
        public required Apiario Apiario { get; set; }
        public required DateOnly Data { get; set; }
        public required decimal VolumeTotal { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DeletionDate { get; set; } = null;
    }
}
