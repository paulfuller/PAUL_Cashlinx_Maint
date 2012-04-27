/**********************************************************************************
 * Namespace:       PawnDataAccess.Couch.Impl
 * Class:           DocumentAccess
 * 
 * Description      Provides the properties needed for Document Repository Access
 * 
 * History
 * David D Wise, Initial Development
 *   
 *************************************************************************************/

using System.Collections;

namespace Common.Controllers.Database.Couch.Impl
{
    public class DocumentAccess
    {
        public string SystemAddress;
        public string AddressPort;
        public string SystemAccount;
        public string SystemPassword;
        public string DatabaseName;
        public IDictionary SystemAttributes;
    }
}
