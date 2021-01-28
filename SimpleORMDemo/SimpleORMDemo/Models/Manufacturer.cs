namespace SimpleORMDemo.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Manufacturer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}