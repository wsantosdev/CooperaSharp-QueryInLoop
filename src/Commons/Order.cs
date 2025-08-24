namespace CooperaSharp_QueryInLoop
{
    public sealed class Order
    {
        public required int Id { get; init; }
        public required DateTime CreationDate { get; init; }
        public required int CustomerId { get; init; }

        public required Customer Customer { get; init; }
    }
}
