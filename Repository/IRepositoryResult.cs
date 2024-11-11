using ServiMun.Shared;

namespace ServiMun.Repository
{

    public interface IRepositoryResult<T>
    {
        public Task<Result<T>> AddItem(T item);
        public Task<Result<T>> UpdateItem(T item);
        public Task<Result<T>> DeleteItem(int id);
        public Task<Result<T>> GetById(int id);
        public Task<IEnumerable<T>> GetAll();
    }

}
