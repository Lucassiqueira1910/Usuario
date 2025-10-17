using System.ComponentModel.DataAnnotations;

namespace Usuario.Models;

public class Usuario
{
    
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nome é um campo requerido")]
    [MaxLength(60, ErrorMessage = "Nome precisa ter no maximo 60 caracteres")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "Senha é um campo requerido")]
    [MaxLength(40, ErrorMessage = "Senha precisa ter no maximo 40 caracteres")]
    public string Senha { get; set; }
    
    [Required(ErrorMessage = "Email é um campo requerido")]
    [MaxLength(140, ErrorMessage = "Email precisa ter no maximo 140 caracteres")]
    public string Email { get; set; }
}