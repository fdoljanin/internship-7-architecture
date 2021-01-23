namespace PointOfSale.Data.Entities.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Pin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isRemoved { get; set; }
    }
}
