using System.ComponentModel.DataAnnotations;

namespace ChatApp.Web.Models;

public class User
{
    [Required(ErrorMessage = "Pole wymagane")]
    [MinLength(3, ErrorMessage = "Podaj imię")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Pole wymagane")]
    [MinLength(1, ErrorMessage = "Podaj nazwisko")]
    public string Surname { get; set; }
}