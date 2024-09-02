namespace DataAccessLayer.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ICollection<ProductVersion> ProductVersions { get; set; } = new List<ProductVersion>();
}
