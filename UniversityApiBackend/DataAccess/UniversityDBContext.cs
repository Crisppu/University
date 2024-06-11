using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext:DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options, ILoggerFactory loggerFactory): base(options) {
            _loggerFactory = loggerFactory;
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories { get; set; }

        public DbSet<Chapter>? Chapters { get; set; }
        //variable se modificara esto se puso por defecto
        public DbSet<UniversityApiBackend.Models.DataModels.Student> Student { get; set; } = default!;

        /*
         * OnConfigurinf() : determinadas operaciones sobre el modelo
         * 
         */
        //todas las peticiones que tenga que ver con nuestra base de datos desde entity Franwork
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger =  _loggerFactory.CreateLogger<UniversityDBContext>();
            /*ejemplo cada vez que se hace una lectura de 1000 estudiantes(cualquier tabla) se aria un console por cada una*/
            /*Persisitir absolutamento todo*/
            /*estas instrucciones lo que nos permite es que cuando se modifique calquier tabla o midficacion de la base de datos siempre percistira osea cuardara informacion del cambio*/
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Information,d, new[] {DbLoggerCategory.Database.Name })); //*donde queremos registrar nuestro contenido, donde se presistira el contenido*/
            optionsBuilder.EnableSensitiveDataLogging();            /*habilita a la app informacion, datos, menssages , excepciones*/

            /*Configuracion personalizada - filtrar solo los que son de nivel infomation*/

            /*esta configuracion es para Guardar los cambios, modificaciones */
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors(); //nos seniala los errores

        }
    }
}
