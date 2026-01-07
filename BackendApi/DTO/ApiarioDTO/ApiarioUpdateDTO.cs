using BackendApi.Enums;

namespace BackendApi.Dto.ApiarioDTO{   
    public class ApiarioUpdateDTO{ 
        public required int Id { get; set; } 
        public string? Bioma {get; set;} 
        public StatusAtividadeEnum? Atividade { get; set; }
    }
}