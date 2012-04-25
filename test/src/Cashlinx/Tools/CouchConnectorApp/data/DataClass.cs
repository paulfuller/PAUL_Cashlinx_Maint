using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CouchConsoleApp.vo;


namespace MainForm
{

    class DataClass
    {
        List<PawnDocRegVO> records = null;

        private int totalRecords = 5000;
        private int fetchPerTime =1000;

        private static readonly DataClass dataClass=new DataClass();

        DataClass()
        {
            populateData();
        }

        public int Total()
        {
            return totalRecords;
        }

        private void populateData()
        {
            records=new List<PawnDocRegVO>();
            PawnDocRegVO vo = null;
            for (int i = 1; i <= totalRecords; i++)
            {
                vo=new PawnDocRegVO();
                vo.DocID = i;
                vo.StorageID = "xyz";
                vo.CreationDate = new DateTime();
                records.Add(vo);
            }
        }

        public List<PawnDocRegVO> getNext(int begin)
        {
           List<PawnDocRegVO> toSend=new List<PawnDocRegVO>();
           int recordIndex = begin;

           if (recordIndex > totalRecords)
           {
               return toSend;
           }
           else if (recordIndex <= totalRecords)
           {
               for (int i =0; i < fetchPerTime; i++)
               {
                   if (recordIndex >= records.Count) break;
                   toSend.Add(records[recordIndex]);
                   recordIndex++;

               }
           }
           

           return toSend;
        }

        public List<PawnDocRegVO> getFirstSet()
        {
            List<PawnDocRegVO> toSend = new List<PawnDocRegVO>();
            if (fetchPerTime <= totalRecords)
            {
                for (int i = 0; i < fetchPerTime; i++)
                {
                    toSend.Add(records[i]);
                }
            }
            else
            {
                toSend.AddRange(records);
            }

            return toSend;
        }


        public static DataClass Instance()
        {
            return dataClass;
        }


    }

 }
