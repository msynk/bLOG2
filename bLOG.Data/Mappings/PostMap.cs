using System.Data.Entity.ModelConfiguration;
using bLOG.Core.Domain;

namespace bLOG.Data.Mappings
{
  public class PostMap : EntityTypeConfiguration<Post>
  {
    public PostMap()
    {
      // Primary Key
      HasKey(t => t.Id);

      // Properties
      Property(t => t.Id)
          .IsRequired()
          .HasMaxLength(100);

      Property(t => t.Author)
          .IsRequired();

      Property(t => t.Title)
          .IsRequired();

      Property(t => t.Content)
          .IsRequired();

      // Table & Column Mappings
      ToTable("Posts");
      Property(t => t.Id).HasColumnName("Id");
      Property(t => t.Author).HasColumnName("Author");
      Property(t => t.Title).HasColumnName("Title");
      Property(t => t.PublishDate).HasColumnName("PublishDate");
      Property(t => t.Content).HasColumnName("Content");
      Property(t => t.IsPublished).HasColumnName("IsPublished");
      Property(t => t.ViewsCount).HasColumnName("ViewsCount");
    }
  }
}
