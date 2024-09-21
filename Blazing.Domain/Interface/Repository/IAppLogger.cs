namespace Blazing.Domain.Interface.Repository
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
