using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.BuildRelationships.Final.Models
{
    public class Person
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
    }
}
