using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CustomerForCreationDto
    {
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
    }
}
