using BackendApi.Enums;


namespace BackendApi.Dto.ColmeiaDTO
{    
    public class ColmeiaUpdateDTO
    {
        public required int Id { get; set; } 
        public required int ApiarioId { get; set; } 
        public DateOnly? AnoRainha { get; set; }
        public StatusAtividadeEnum? Status { get; set; }           
    }
}