using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Core.Common
{
    public class GroupCount<TKey>
    {
        public TKey Key { get; set; } = default!;
        public int Count { get; set; }
    }
}
