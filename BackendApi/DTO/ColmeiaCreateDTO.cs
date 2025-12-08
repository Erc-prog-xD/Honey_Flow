using BackendApi.Enums;
using BackendApi.Models;

public class ColmeiaCreateDTO
{   
    public required DateOnly AnoColmeia { get; set; }
    public DateOnly? AnoRainha { get; set; }
    public StatusAtividadeEnum Status { get; set; }
}
