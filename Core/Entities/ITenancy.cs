using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public interface ITenancy
    {
        int TenantId { get; set; }
    }
}
