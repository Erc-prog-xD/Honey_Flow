using BackendApi.Enums;
using System;

namespace BackendApi.Models
{
    public class Colmeia
    {
        public int Id { get; set; }
        public required int ApiarioId { get; set; }
        public required DateOnly AnoColmeia { get; set; }
        public DateOnly? AnoRainha { get; set; }
        public StatusAtividadeEnum Status { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DeletionDate { get; set; } = null;

        [ForeignKey(nameof(ApiarioId))]
        public Apiario? Apiario { get; set; }
    }
}