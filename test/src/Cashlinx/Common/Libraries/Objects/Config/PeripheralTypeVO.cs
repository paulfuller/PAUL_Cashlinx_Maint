namespace Common.Libraries.Objects.Config
{
    public class PeripheralTypeVO
    {
        public string PeripheralTypeId
        {
            set; get;
        }

        public string PeripheralTypeName
        {
            set; get;
        }

        public PeripheralModelVO PeripheralModel
        {
            set;
            get;
        }

        public PeripheralTypeVO()
        {
            this.PeripheralTypeId = string.Empty;
            this.PeripheralTypeName = string.Empty;
            this.PeripheralModel = new PeripheralModelVO();
        }
    }
}
