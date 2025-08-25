using System.Runtime.InteropServices;

namespace CooperaSharp_QueryInLoop
{
    public sealed class CustomerIdentifiedOrderView
    {
        public required int OrderId { get; init; }
        public required DateTime CreationDate { get; init; }
        public required string CustomerName { get; init; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly record struct CustomerIdentifiedOrderViewAsStruct(int OrderId, DateTime CreationDate, string CustomerName);
}
