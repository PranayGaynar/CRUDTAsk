namespace TaskComp.Models
{
    public class ProductMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Foreign key
        public int CategoryId { get; set; }

        //Navigation
        public CategoryMaster categorymasters { get; set; }

    }
}
