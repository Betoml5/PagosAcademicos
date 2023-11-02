using PagosAcademicos.Models.Entities;

namespace PagosAcademicos.Repositories
{
    public class Repository<T> where T : class
    {

        private readonly PagosAcademicosContext ctx;
        public Repository(PagosAcademicosContext ctx)
        {
         this.ctx = ctx;
        }

        
    }
}
