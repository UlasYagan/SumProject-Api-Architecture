using System;
using System.ComponentModel.DataAnnotations;

namespace Sum.Model.Auth
{
    public class ResetPasswordDto
    {
        public Guid UserId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required, Compare("NewPassword")]
        public string RePassword { get; set; }
    }
}
