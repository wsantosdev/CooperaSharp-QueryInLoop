namespace CooperaSharp_QueryInLoop
{
    public sealed class Customer
    {
        public required int Id { get; init; }
        public required string Name { get; init; }

        public required ICollection<Order> Orders { get; init; }
    }
}
