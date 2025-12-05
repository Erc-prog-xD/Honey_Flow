using BackendApi.Enums;
using BackendApi.Models;

public class ColmeiaCreateDTO 
{
    public required Apiario Apiario { get; set; }
    public DateOnly AnoColmeia { get; set; }
    public DateOnly? AnoRainha { get; set; }
    public StatusAtividadeEnum Atividade { get; set; }
}
