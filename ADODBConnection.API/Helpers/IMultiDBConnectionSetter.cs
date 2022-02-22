using ADODBConnection.Contracts.MultiDBConnection;

namespace ADODBConnection.API.Helpers
{
    public interface IMultiDBConnectionSetter
    {
        void SetDBConnection(DBConnectionType type);
    }
}
