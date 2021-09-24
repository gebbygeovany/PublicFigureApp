using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name must not be empty!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email must not be empty!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must not be empty!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
