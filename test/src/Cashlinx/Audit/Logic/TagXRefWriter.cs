/*
 * 
 * Field Name       #Char   #Beg Col    #End Col
 * Short Code       9       1           9
 * MD_PFI_amount    3       10          13
 * Trak_Flag        1       13          14
 * Trak_Loc_code    6       14          20
 * 
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Libraries.Objects.Audit;

namespace Audit.Logic
{
    public class TagXRefWriter : TrakkerFileHelperBase
    {
        public TagXRefWriter(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; private set; }

        public void WriteFile(List<TrakkerItem> items)
        {
            const string format = "{0,-4:0000}{1,6:000000}";

            using (var sw = new StreamWriter(FileName, false))
            {
                foreach (TrakkerItem item in items.Where(iw => iw.RfbNo > 0).OrderBy(io => io.RfbNo))
                {
                    sw.WriteLine(format, item.RfbNo, item.RecordNumber);
                }
            }
        }
    }
}
