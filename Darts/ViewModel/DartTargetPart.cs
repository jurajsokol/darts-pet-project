using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darts.ViewModel
{
    internal class DartTargetPart
    {
        public int TargetNumber { get; init; }

        public int TargetID { get; init; }
        public TargetButtonType ButtonType { get; init; }
    }
}
