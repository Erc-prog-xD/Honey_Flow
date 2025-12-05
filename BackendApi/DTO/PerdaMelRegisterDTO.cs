using BackendApi.Models;

public class PerdaMelRegisterDTO
{   
    public required User User { get; set; }
    public required decimal Volume { get; set; }
    public required DateOnly Data { get; set; }
}
