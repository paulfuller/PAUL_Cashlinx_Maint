
namespace ChangeStoreNumber
{
    public class AppVersion
    {
        public AppVersion()
        {

        }

        public AppVersion(string description, int id)
        {
            Description = description;
            Id = id;
        }

        public string Description { get; set; }
        public int Id { get; set; }

        public bool UsesCashDrawer
        {
            get { return Id == 1; }
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Description))
            {
                return Description;
            }
            return base.ToString();
        }
    }
}
