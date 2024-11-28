namespace TaskComp.Models
{
    public class CategoryMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //Navigation
        public ICollection<ProductMaster> productmasters { get; set; }

    }
}
