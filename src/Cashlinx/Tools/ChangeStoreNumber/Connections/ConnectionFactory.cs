using System.Collections.Generic;
using System.Linq;

namespace ChangeStoreNumber.Connections
{
    public class ConnectionFactory : Singleton<ConnectionFactory>
    {
        public ConnectionFactory()
        {
            Connections = new List<DatabaseConnection>();
            Connections.Add(new BlankConnection());
            Connections.Add(new CLXD2());
            Connections.Add(new CLXD3());
            Connections.Add(new CLXI());
            Connections.Add(new CLXQ());
            Connections.Add(new CLXT());
            Connections.Add(new CLXT2());
            Connections.Add(new CLXT6());

            Connections = Connections.OrderBy(c => c.DisplayName).ToList();
        }

        public List<DatabaseConnection> Connections { get; private set; }

        public DatabaseConnection ActiveConnection { get; set; }

        public bool IsDatabaseSelected()
        {
            return ActiveConnection != null && ActiveConnection.Selectable == true;
        }
    }
}
