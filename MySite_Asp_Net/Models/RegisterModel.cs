using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;    // Для вказування артибутів і аннотацій.


namespace MySite_Asp_Net.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Empty FirstName")]  // Required - обовязковий до заповнення.
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]   // Тип інформації - для роботи з паролями.
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password Error")]       // для порівняння властивостей (паролей)
        public string ConfirmPassword { get; set; }     // підтвердження пароля.
    }
}
