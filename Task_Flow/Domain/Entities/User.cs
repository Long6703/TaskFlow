using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : EntityBaseTracking<Guid>
    {
        // More properties here ignored for id, created date, last modified date, and is deleted
    }
}
