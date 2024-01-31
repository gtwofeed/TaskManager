namespace TaskManager.Common.Models
{
    public static class InvokeExtensions
    {
        public static bool ToDo(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch { return false; }
        }
        public static bool ToDo(Func<int> func, ref int id)
        {
            try
            {
                id = func.Invoke();
                return true;
            }
            catch { return false; }
        }
    }
}
