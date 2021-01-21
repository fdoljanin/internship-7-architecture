namespace PointOfSale.Domain.Repositories.Abstractions
{
    public interface IUniqueString
    {
        public bool IsStringUnique(string inputString);
    }
}
