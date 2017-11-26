using MasteringEFCore.MultiTenancy.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.ViewModels
{
    public class PersonViewModel
    {
        public Person Person { get; set; }
        public int NoOfComments { get; set; }
    }
}
