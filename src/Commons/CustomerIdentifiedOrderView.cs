namespace CooperaSharp_QueryInLoop
{
    public sealed class CustomerIdentifiedOrderView
    {
        public required int OrderId { get; init; }
        public required DateTime CreationDate { get; init; }
        public required string CustomerName { get; init; }
    }
}
