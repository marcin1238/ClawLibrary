namespace ClawLibrary.Services.Models.Categories
{
    public class CategoryRequest
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}