using BackendApi.Models;

public class ProducaoRegisterDTO
{   
    public required User User { get; set; }
    public required Apiario Apiario { get; set; }
    public required DateOnly Data { get; set; }
    public required decimal VolumeTotal { get; set; }
}
