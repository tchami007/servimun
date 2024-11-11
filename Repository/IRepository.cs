namespace ServiMun.Repository
{
    public interface IRepository<T>
    {
        public Task<T> AddItem(T item);
        public Task<bool> UpdateItem(T item);
        public Task<bool> DeleteItem(int id);
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetAll();
    }
}
