using System.ComponentModel.DataAnnotations;

namespace Domain.DTOS;

public class LoginDto
{
    [Required (ErrorMessage = "Email é campo obrigatório para Login")]
    [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
    [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} carateres.")]
    
    public string Email { get; set; }
    
    
}