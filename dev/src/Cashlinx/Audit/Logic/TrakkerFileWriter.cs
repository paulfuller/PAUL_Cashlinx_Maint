/*
 * 
 * 
 * Field Name       #Char   #Beg Col    #End Col
 * ICN              18      1           18
 * MD_PFI_amount    8       19          26
 * Trak_Flag        1       27          27
 * Trak_Loc_code    1       28          28
 * Trak_Loc         10      29          38
 * Md_disp_doc
 * Trak_seq         6
 * Trakker_id       2
 * 
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Libraries.Objects.Audit;

namespace Audit.Logic
{
    public class TrakkerFileWriter : TrakkerFileHelperBase
    {
        public TrakkerFileWriter(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; private set; }

        public void WriteFile(List<TrakkerItem> items, int trakkerId)
        {
            const string format = "{0,-18}{1,8:00000.00}{2,1}{3,1}{4,10}{5,6:000000}{6,2:00}";

            using (var sw = new StreamWriter(FileName, false))
            {
                foreach (TrakkerItem item in items.OrderBy(i => i.Icn.ToString()))
                {
                    item.CreateTrackFlag();
                    sw.WriteLine(format, item.Icn.ToString(), item.PfiAmount, item.TrakFlag, item.LocationCode, item.CreateTrackLocation(), item.SequenceNumber, trakkerId);
                }
            }
        }
    }
}
