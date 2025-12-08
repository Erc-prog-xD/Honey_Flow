using System;

namespace BackendApi.Models
{
    public class Producao
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int ApiarioId { get; set; }
        public required DateOnly Data { get; set; }
        public required decimal VolumeTotal { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DeletionDate { get; set; } = null;

        [ForeignKey(nameof(UserId))]
        public Usuario? Usuario { get; set; } 
        
        [ForeignKey(nameof(ApiarioId))]
        public Apiario? Apiario { get; set; }
    }
}
