using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Abstractions
{
    public interface IEntityBase<TId>
    {
        TId Id { get; set; }
    }
}
