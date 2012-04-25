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
using Common.Libraries.Objects.Audit;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;

namespace Audit.Logic
{
    public class TrakkerFileReader
    {
        public TrakkerFileReader(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; private set; }

        public List<TrakkerItem> ReadFile()
        {
            List<TrakkerItem> items = new List<TrakkerItem>();

            using (StreamReader reader = new StreamReader(FileName))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();

                    TrakkerItem item = new TrakkerItem();
                    item.Icn = new Icn(line.Substring(0, 18));
                    item.PfiAmount = Utilities.GetDecimalValue(line.Substring(18, 8), 0);
                    item.TrakFlag = line.Substring(26, 1)[0];
                    item.LocationCode = line.Substring(27, 1);
                    item.Location = line.Substring(28, 10);
                    item.SequenceNumber = Utilities.GetIntegerValue(line.Substring(38, 6), 0);
                    item.TrakkerId = Utilities.GetIntegerValue(line.Substring(44, 2), 0);
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
