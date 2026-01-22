using BackendApi.Enums;

namespace BackendApi.Dto.ApiarioDTO{   
    public class ApiarioUpdateDTO{ 
        public string? Bioma {get; set;} 
        public StatusAtividadeEnum? Atividade { get; set; }
    }
}