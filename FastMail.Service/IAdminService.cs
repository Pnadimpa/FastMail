using FastMail.Core.Domain;

namespace FastMail.Service
{
    public interface IAdminService
    {
        /// <summary>
        /// Gets all senders
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        List<Sender> GetAllSenders();
    }
}