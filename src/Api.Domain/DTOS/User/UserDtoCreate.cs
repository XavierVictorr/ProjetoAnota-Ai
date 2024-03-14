using System.ComponentModel.DataAnnotations;

namespace Domain.DTOS.User;

public class UserDtoCreate
{
    [Required(ErrorMessage = "Nome é um campo obrigatorio")]
    [StringLength (60, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
    public string Name { get; set; }
    
    [Required (ErrorMessage = "Email é campo obrigatório")]
    [EmailAddress (ErrorMessage = "E-mail em formato inválido")]
    [StringLength (100, ErrorMessage = "Emaildeve ter no maximo {1} caracteres.")]
    public string Email { get; set; }
}