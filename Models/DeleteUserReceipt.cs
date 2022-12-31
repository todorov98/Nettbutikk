using System;

namespace Nettbutikk.Models
{
    public class DeleteUserReceipt
    {
        public string DeletedUsername { get; set; }
        public bool DeletedByAdmin { get; set; }
        public DateTime DeletedDate { get; set; }

        public DeleteUserReceipt(string username, bool byAdmin)
        {
            DeletedUsername = username;
            DeletedByAdmin = byAdmin;
            DeletedDate = DateTime.Now.AddMilliseconds(-5.0);
        }
    }
}
