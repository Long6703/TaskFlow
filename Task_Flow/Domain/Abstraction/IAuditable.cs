using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction
{
    public interface IAuditable
    {
        DateTime? DateCreated { get; set; }
        string? CreatedBy { get; set; }
        DateTime? DateModified { get; set; }
        string? ModifiedBy { get; set; }
    }
}
