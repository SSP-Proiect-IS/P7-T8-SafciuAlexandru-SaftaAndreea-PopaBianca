using CanvasHub.Repositories.Interfaces;

using CanvasHub.Models;

namespace CanvasHub.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        protected CanvasHubContext _applicationDbContext { get; set; }
        public RepositoryWrapper(CanvasHubContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
