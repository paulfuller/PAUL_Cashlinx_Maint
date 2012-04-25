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
    public class TrakkerScFileWriter : TrakkerFileHelperBase
    {
        public TrakkerScFileWriter(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; private set; }

        public void WriteFile(List<TrakkerItem> items)
        {
            Dictionary<string, ShortCodeSummary> shortCodeMap = GetShortCodeMap(items);

            string format = "{0,-9}{1,3:000}{2,1}{3,6:000000}";

            using (StreamWriter sw = new StreamWriter(FileName, false))
            {
                foreach (TrakkerItem item in items.OrderBy(i => i.Icn.GetShortCode(string.Empty)))
                {
                    string shortCode = item.Icn.GetFullShortCode(string.Empty);
                    ShortCodeSummary summary = shortCodeMap[shortCode];

                    sw.WriteLine(format, shortCode, summary.Count, GetTrackFlag(summary, item), summary.FirstRecordNumber);
                }
            }
        }

        private Dictionary<string, ShortCodeSummary> GetShortCodeMap(List<TrakkerItem> items)
        {
            Dictionary<string, ShortCodeSummary> map = new Dictionary<string, ShortCodeSummary>();

            foreach (TrakkerItem item in items)
            {
                string shortCode = item.Icn.GetFullShortCode(string.Empty);

                if (!map.ContainsKey(shortCode))
                {
                    ShortCodeSummary summary = new ShortCodeSummary();
                    summary.Count++;
                    summary.FirstRecordNumber = item.RecordNumber;
                    summary.ShortCode = shortCode;
                    map.Add(shortCode, summary);
                }
                else
                {
                    ShortCodeSummary summary = map[shortCode];
                    summary.Count++;
                }
            }

            return map;
        }

        private char GetTrackFlag(ShortCodeSummary summary, TrakkerItem item)
        {
            if (summary.Count > 1)
            {
                return ' ';
            }

            return item.TrakFlag;
        }
    }
}
