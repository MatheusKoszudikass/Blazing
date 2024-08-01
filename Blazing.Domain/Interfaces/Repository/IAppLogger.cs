using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Domain.Interfaces.Repository
{
    #region Logs.
    /// <summary>
    /// Esse tipo elimina a necessidade de depender diretamente dos tipos de log do ASP.NET Core.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAppLogger<T>
    {
        Task LogInformation(string message, params object[] args);
        Task LogWarning(string message, params object[] args);
    }
    #endregion
}
