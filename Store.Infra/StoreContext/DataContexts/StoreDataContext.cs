using System.Data;
using System.Data.SqlClient;
using Store.Store.Shared.Settings;

namespace Store.Store.Infra.StoreContext.DataContexts;

public class StoreDataContext : IDisposable
{
    public SqlConnection Connection { get; set; }

    public StoreDataContext()
    {
        Connection = new SqlConnection(Settings.ConnectionString);
        Connection.Open();
    }

    public void Dispose()
    {
        if (Connection.State != ConnectionState.Closed)
            Connection.Close();
    }
}