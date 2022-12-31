namespace Nettbutikk.State
{
    /// <summary>
    /// Contains the different stages that an order can be in after it has been placed. Represents
    /// the current status of an order.
    /// </summary>
    public static class OrderStages
    {
        public static readonly string Received = "Received";
        public static readonly string InProcess = "InProcess";
        public static readonly string Sent = "Sent";
        public static readonly string Delivered = "Delivered";
        public static readonly string Cancelled = "Cancelled";
    }
}
