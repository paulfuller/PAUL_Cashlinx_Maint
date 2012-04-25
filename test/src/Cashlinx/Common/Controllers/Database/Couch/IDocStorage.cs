/**********************************************************************************
 * Namespace:       PawnDataAccess.Couch
 * Class:           IDocStorage
 * 
 * Description      Document Storage Interface for minimum methods for inherited
 *                  classes to incorporate
 * 
 * History
 * David D Wise, Initial Development
 *   
 *************************************************************************************/

using Common.Controllers.Database.Couch.Impl;
using Common.Libraries.Objects.Doc;

namespace Common.Controllers.Database.Couch
{
    public interface IDocStorage
    {
        bool Startup(DocumentAccess accessSettings
                        , out string sErrorCode
                        , out string sErrorText);

        bool Shutdown(DocumentAccess accessSettings
                        , out string sErrorCode
                        , out string sErrorText);

        bool AddDocument(DocumentAccess accessSettings
                        , ref Document document
                        , out string sErrorCode
                        , out string sErrorText);

        bool GetDocument(DocumentAccess accessSettings
                        , ref Document doc
                        , out string sErrorCode
                        , out string sErrorText);

        bool DeleteDocument(DocumentAccess accessSettings
                        , ref Document doc
                        , out string sErrorCode
                        , out string sErrorText);
    }
}
