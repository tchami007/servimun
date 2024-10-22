namespace ServiMun.Shared
{
    public class Result<T>
    {
        // Atributos
        public T _value { get; }
        public bool _succes { get; }
        public string _errorMessage { get; }

        // Constructores
        private Result (T value)
        {
            _value = value;
            _succes = true;
        }

        private Result (string message)
        {
            _succes = false;
            _errorMessage = message;
        }

        //Metodos estaticos
        public  static Result<T> Success (T value) => new Result<T> (value);
        public  static Result<T> Failure (string message) => new Result<T> (message);
    }
}
