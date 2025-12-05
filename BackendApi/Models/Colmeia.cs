using BackendApi.Enums;
using System;

namespace BackendApi.Models
{
    public class Colmeia
    {
        public int Id { get; set; }
        public required Apiario Apiario { get; set; }
        public required DateOnly AnoColmeia { get; set; }
        public DateOnly? AnoRainha { get; set; }
        public StatusAtividadeEnum Status { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DeletionDate { get; set; } = null;
    }
}