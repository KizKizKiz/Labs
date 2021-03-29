using FluentNHibernate.Mapping;

using Library.Model;

namespace Library.StorageProcessor.NHibernateAccessor
{
    public class ProviderMapping : ClassMap<Provider>
    {
        public ProviderMapping()
        {
            Id(id => id.ID).GeneratedBy.Identity();

            Map(c => c.Name).Length(30).Not.Nullable();
            Map(c => c.Address).Length(70).Nullable();
            Map(c => c.TelNumber).Length(11).Not.Nullable();

            Table("Providers");
            HasMany(c => c.GoodsProviders).Inverse().Cascade.All().KeyColumn(nameof(GoodProvider.ProviderId)).Not.LazyLoad();
        }
    }

    public class GoodTypeMapping : ClassMap<GoodType>
    {
        public GoodTypeMapping()
        {
            Id(id => id.ID).GeneratedBy.Identity();

            Map(c => c.Name).Length(60).Not.Nullable();

            Table("Types");

            HasMany(c => c.Goods).Inverse().Cascade.All().KeyColumn(nameof(Good.TypeId)).Not.LazyLoad();
        }
    }

    public class GoodMapping : ClassMap<Good>
    {
        public GoodMapping()
        { 
            Id(id => id.ID).GeneratedBy.Identity();

            Map(c => c.Manufacturer).Length(30).Not.Nullable();
            Map(c => c.Model).Length(30).Not.Nullable();
            Map(c => c.OriginalPrice).Length(18).Precision(2).Not.Nullable();
            Map(c => c.DiscontPercent).Not.Nullable();
            Map(c => c.Description).Length(150).Not.Nullable();

            Table("Goods");

            References(c => c.GoodType).Column(nameof(Good.TypeId)).Not.LazyLoad();
            HasMany(c => c.GoodsProviders).Inverse().Cascade.All().KeyColumn(nameof(GoodProvider.GoodId)).Not.LazyLoad();
        }
    }

    public class GoodProviderMapping : ClassMap<GoodProvider>
    {
        public GoodProviderMapping()
        {
            Id(id => id.ID).GeneratedBy.Identity();

            Map(c => c.DeliveryDate).Nullable();
            Map(c => c.Quantity).Not.Nullable();

            Table("GoodsProviders");

            References(c => c.Good).Column(nameof(GoodProvider.GoodId)).Not.LazyLoad();
            References(c => c.Provider).Column(nameof(GoodProvider.ProviderId)).Not.LazyLoad();
        }
    }
}