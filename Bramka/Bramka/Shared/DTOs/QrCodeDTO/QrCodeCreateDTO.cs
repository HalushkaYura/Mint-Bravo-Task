using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bramka.Shared.DTOs.QrCodeDTO
{
    public class QrCodeCreateDTO
    {
        public Guid UserId { get; set; }
        public string Type { get; set; }

    }
}
