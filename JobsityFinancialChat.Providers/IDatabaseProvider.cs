using System.Threading.Tasks;

namespace JobsityFinancialChat.Providers
{
    public interface IDatabaseProvider
    {
        #region General

        Task Save();

        #endregion General
    }
}
