using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RestWebservice_RemoteCompiling.Database
{
    public class Checkpoint
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Stdin { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}