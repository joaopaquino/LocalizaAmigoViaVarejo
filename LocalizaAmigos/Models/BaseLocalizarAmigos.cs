using System.Data.Entity;

namespace LocalizaAmigos.Models
{
    class BaseLocalizarAmigos: DbContext
    {
        public BaseLocalizarAmigos()
            : base("DBLocalizarAmigos")
        {
        }

        public DbSet<Amigo> Amigos { get; set;}
    }
}
