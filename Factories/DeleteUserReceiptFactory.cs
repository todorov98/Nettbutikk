using Nettbutikk.Models;

namespace Nettbutikk.Factories
{
    public class DeleteUserReceiptFactory
    {
        public DeleteUserReceipt CreateDeleteUserReceipt(string username, bool deletedByAdmin)
        {
            return new DeleteUserReceipt(username, deletedByAdmin);
        }
    }
}
