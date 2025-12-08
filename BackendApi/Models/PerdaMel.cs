using System;

namespace BackendApi.Models
{
    public class PerdaMel
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required decimal Volume { get; set; }
        public required DateOnly Data { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DeletionDate { get; set; } = null;

        [ForeignKey(nameof(UserId))]
        public Usuario? Usuario { get; set; } 
    }
}
