namespace Common.Libraries.Objects.Config
{
    public class PeripheralModelVO
    {
        public string Id
        {
            set; get;
        }

        public string Name
        {
            set; get;
        }

        public string Make
        {
            set; get;
        }

        public string Desc
        {
            set; get;
        }

        public string PeripheralTypeId
        {
            set; get;
        }

        public PeripheralModelVO()
        {
            Id = string.Empty;
            Name = string.Empty;
            Make = string.Empty;
            Desc = string.Empty;
            PeripheralTypeId = string.Empty;
        }
    }
}
