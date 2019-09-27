using System;
using System.Data.Common;

namespace Biblioteca._Infra
{
    public interface IDB : IDisposable
    {
        DbConnection GetConnection();
    }
}
