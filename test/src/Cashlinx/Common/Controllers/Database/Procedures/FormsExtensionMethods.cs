using System.Windows.Forms;

namespace Common.Controllers.Database.Procedures
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
