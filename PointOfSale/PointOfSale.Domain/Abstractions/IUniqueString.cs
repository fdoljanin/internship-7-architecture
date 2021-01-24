namespace PointOfSale.Domain.Abstractions
{
    public interface IUniqueString
    {
        public bool IsStringUnique(string inputString);
    }
}
