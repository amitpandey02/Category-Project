using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CategoryNewProject.Models
{
    public class Credential
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [ForeignKey("credential")]
        public int RoleId { get; set; }

        public virtual Role credential { get; set; }

    }
}