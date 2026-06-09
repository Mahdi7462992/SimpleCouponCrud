namespace SimpleCouponCrud.Models.BaseEntities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
