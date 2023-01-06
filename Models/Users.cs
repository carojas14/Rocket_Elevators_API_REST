using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
public class Users
{
    [Key]
    public long id { get; set; }
    public string? email { get; set; }
    public string? encrypted_password{ get; set; }
}