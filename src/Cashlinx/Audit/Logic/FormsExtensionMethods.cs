using System.Windows.Forms;

namespace Audit.Logic
{
    public static class FormsExtensionMethods
    {
        public static DataGridViewRow AddNew(this DataGridViewRowCollection collection)
        {
            int iDx = collection.Add();
            return collection[iDx];
        }
    }
}
