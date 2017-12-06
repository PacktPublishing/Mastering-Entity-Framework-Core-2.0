using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Models
{
    public class EntityBase
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
    }
}
