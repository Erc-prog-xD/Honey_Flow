using BackendApi.Models;

public class ProducaoRegisterDTO
{   
    public required DateOnly Data { get; set; }
    public required decimal VolumeTotal { get; set; }
}
