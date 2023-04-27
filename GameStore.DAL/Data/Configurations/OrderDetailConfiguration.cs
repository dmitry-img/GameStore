using System.Data.Entity.ModelConfiguration;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Data.Configurations
{
    public class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            Property(i => i.Price)
            .HasColumnType("money");

            Property(i => i.Discount)
            .HasColumnType("real");
        }
    }
}
