namespace Darts.Games.State
{
    public class Store<T>
    {
        private Stack<T> stateHistory = new Stack<T>();

        public T State { get; private set; }

        public Store(T initialValue)
        { 
            State = initialValue;
            stateHistory.Push(initialValue);
        }
    }
}
