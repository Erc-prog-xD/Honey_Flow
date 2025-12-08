namespace BackendApi.Models
{
    public class VendaMel
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required DateTime Data { get; set; }
        public required decimal Volume { get; set; }
        public required decimal ValorTotal { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DeletionDate { get; set; } = null;

        [ForeignKey(nameof(UserId))]
        public Usuario? Usuario { get; set; } 
    }
}
