using MasteringEFCore.Performance.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Final.ViewModels
{
    public class PersonViewModel
    {
        public Person Person { get; set; }
        public int NoOfComments { get; set; }
    }
}
