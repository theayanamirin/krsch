using System.Data.Entity;

namespace MvcApplication1.Models
{
    public class SiteContext : DbContext
    {
        // В этот файл можно добавить пользовательский код. Изменения не будут перезаписаны.
        // 
        // Если требуется, чтобы платформа Entity Framework автоматически удаляла и формировала заново базу данных
        // при каждой смене схемы модели, добавьте следующий
        // код к методу Application_Start в файле Global.asax.
        // Примечание: в этом случае при каждой смене модели ваша база данных будет удалена и создана заново.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MvcApplication1.Models.SiteContext>());

        public SiteContext() : base("name=SiteContext")
        {
        }

        public DbSet<Clients> Clients { get; set; }
        public DbSet<Comment> Comments { get; set; }
       
    }
}
