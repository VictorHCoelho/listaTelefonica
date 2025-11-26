using System.ComponentModel.DataAnnotations;

namespace ListaTelefonica.Models;

public class Contato
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O telefone é obrigatório")]
    public string Telefone { get; set; }
}