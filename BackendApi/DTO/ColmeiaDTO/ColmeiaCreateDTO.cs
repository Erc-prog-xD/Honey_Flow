using BackendApi.Enums;

namespace BackendApi.Dto.ColmeiaDTO
{
    
public class ColmeiaCreateDTO
{   
    public required int ApiarioId { get; set; }
    public required int AnoColmeia { get; set; }
    public DateOnly? AnoRainha { get; set; }
    public StatusAtividadeEnum Status { get; set; }
}
}