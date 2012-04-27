using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports
{
    public interface IExtensionMemo
    {
        List<string> Documents { get; }
        Dictionary<int, string> ExtensionToPdfMap { get; }

        bool Print();
    }
}
