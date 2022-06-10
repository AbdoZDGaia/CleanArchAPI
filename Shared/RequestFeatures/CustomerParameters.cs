namespace Shared.RequestFeatures
{
    public class CustomerParameters : RequestParameters
    {
        public uint MaxAge { get; set; } = int.MaxValue;
        public uint MinAge { get; set; }

        public bool ValidAge => MaxAge >= MinAge;

        public string? SearchTerm { get; set; }
    }
}
