using BackendApi.Models;

public class VendaMelRegisterDTO
{   
    public required DateTime Data { get; set; }
    public required decimal Volume { get; set; }
    public required decimal ValorTotal { get; set; }
}
