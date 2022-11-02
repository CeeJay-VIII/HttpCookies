using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace HttpCookies_Project.Models
{
    public class Register
    {
        [Required]
        [Display(Name ="Your name")]
        public string name { get; set; }
        [Key]
        [Required]
        [Display(Name ="Email address")]
        public string emailAddress { get; set; }
        [Required]
        [Display(Name ="Create password")]
        public string password { get; set; }
        [Compare("password")]
        [Display(Name = "Confirm password")]
        public string confirmPassword { get; set; }
    }

    public class Login
    {
        public string emailAddress { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
    }


    /// <summary>
    /// Context Class
    /// </summary>
    public class LoginContext : DbContext
    {
        public LoginContext() : base("LoginDB")
        {

        }
        public virtual DbSet<Register> Registers { get; set; }
    }
}