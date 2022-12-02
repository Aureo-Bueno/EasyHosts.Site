using System.Data.Entity;

namespace Easy.Hosts.Site.Models
{
    public class Context : DbContext
    {
        public Context() : base(nameOrConnectionString: "EasyHostsBd")
        { }

        public DbSet<Bedroom> Bedroom { get; set; }

        public DbSet<Event> Event { get; set; }

        public DbSet<Booking> Booking { get; set; }

        public DbSet<Perfil> Perfil { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<TypeBedroom> TypeBedroom { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {

            var bedroom = mb.Entity<Bedroom>();
            bedroom.ToTable("TB_BEDROOM");
            bedroom.Property(x => x.Id).HasColumnName("ID");
            bedroom.Property(x => x.NameBedroom).HasColumnName("NAME_BEDROOM");
            bedroom.Property(x => x.Value).HasColumnName("VALUE");
            bedroom.Property(x => x.Description).HasColumnName("DESCRIPTION");
            bedroom.Property(x => x.Picture).HasColumnName("PICTURE");
            bedroom.Property(x => x.Status).HasColumnName("STATUS");
            bedroom.Property(x => x.TypeBedroomId).HasColumnName("TYPE_BEDROOM");

            var eventBd = mb.Entity<Event>();
            eventBd.ToTable("TB_EVENT");
            eventBd.Property(x => x.Id).HasColumnName("ID");
            eventBd.Property(x => x.NameEvent).HasColumnName("NAME_EVENT");
            eventBd.Property(x => x.Organizer).HasColumnName("ORGANIZER");
            eventBd.Property(x => x.DateEvent).HasColumnName("DATE_EVENT");
            eventBd.Property(x => x.EventsPlace).HasColumnName("EVENTS_PLACE");
            eventBd.Property(x => x.Picture).HasColumnName("PICTURE");
            eventBd.Property(x => x.DescriptionEvent).HasColumnName("DESCRIPTION_EVENT");
            eventBd.Property(x => x.Attractions).HasColumnName("ATTRACTIONS");
            eventBd.Property(x => x.TypeEvent).HasColumnName("TYPE_EVENT");

            var booking = mb.Entity<Booking>();
            booking.ToTable("TB_BOOKING");
            booking.Property(x => x.Id).HasColumnName("ID");
            booking.Property(x => x.CodeBooking).HasColumnName("CODE_BOOKING");
            booking.Property(x => x.DateCheckin).HasColumnName("DATE_CHECKIN");
            booking.Property(x => x.DateCheckout).HasColumnName("DATE_CHECKOUT");
            booking.Property(x => x.ValueBooking).HasColumnName("VALUE_BOOKING");
            booking.Property(x => x.UserId).HasColumnName("USER_ID");
            booking.Property(x => x.BedroomId).HasColumnName("BEDROOM_ID");
            booking.Property(x => x.Status).HasColumnName("STATUS");

            var perfil = mb.Entity<Perfil>();
            perfil.ToTable("TB_PERFIL");
            perfil.Property(x => x.Id).HasColumnName("ID");
            perfil.Property(x => x.Description).HasColumnName("DESCRIPTION");

            var product = mb.Entity<Product>();
            product.ToTable("TB_PRODUCT");
            product.Property(x => x.Id).HasColumnName("ID");
            product.Property(x => x.Name).HasColumnName("NAME_PRODUCT");
            product.Property(x => x.Value).HasColumnName("VALUE");
            product.Property(x => x.QuantityProduct).HasColumnName("QUANTITY_PRODUCT");

            var typebedroom = mb.Entity<TypeBedroom>();
            typebedroom.ToTable("TB_TYPE_BEDROOM");
            typebedroom.Property(x => x.Id).HasColumnName("ID");
            typebedroom.Property(x => x.NameTypeBedroom).HasColumnName("NAME_TYPE_BEDROOM");
            typebedroom.Property(x => x.AmountOfPeople).HasColumnName("AMOUNT_OF_PEOPLE");
            typebedroom.Property(x => x.AmountOfBed).HasColumnName("AMOUNT_OF_BED");
            typebedroom.Property(x => x.ApartmentAmenities).HasColumnName("APARTMENT_AMENITIES");

            var user = mb.Entity<User>();
            user.ToTable("TB_USER");
            user.Property(x => x.Id).HasColumnName("ID");
            user.Property(x => x.Name).HasColumnName("NAME");
            user.Property(x => x.Email).HasColumnName("EMAIL");
            user.Property(x => x.Password).HasColumnName("PASSWORD");
            user.Property(x => x.ConfirmPassword).HasColumnName("CONFIRM_PASSWORD");
            user.Property(x => x.Status).HasColumnName("STATUS");
            user.Property(x => x.Cpf).HasColumnName("CPF");
            user.Property(x => x.PerfilId).HasColumnName("PERFIL_ID");
            user.Property(x => x.Hash).HasColumnName("HASH");


        }
    }
}