using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CouchConsoleApp.vo;

namespace CouchConsoleApp
{
    public class DocArchService
    {
        private List<PawnDocRegVO> getDocumentsToArch()
        {
            throw new System.NotImplementedException();
        }

        private PawnObjects.Doc.Document getDocFromSrc(string docID)
        {
            throw new System.NotImplementedException();
        }

        private List<PawnObjects.Doc.Document> getDocListFromSrc(List<PawnDocRegVO> vo)
        {
            throw new System.NotImplementedException();
        }

        private bool addDocToTrgt(PawnObjects.Doc.Document document,PawnDocRegArchStatVO statVO)
        {
            throw new System.NotImplementedException();
        }

        public List<PawnDocRegArchStatVO> addDocListToTrgt(List<PawnObjects.Doc.Document> documents)
        {
            throw new System.NotImplementedException();
        }
    }
}
