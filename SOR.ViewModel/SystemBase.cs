namespace SOR.ViewModel
{
    public class SystemBase<T>
    {
        public bool CheckNullValue(T value)
        {
            if (value == null ||
              string.IsNullOrEmpty(value.ToString()) ||
              string.IsNullOrWhiteSpace(value.ToString()) ||
              value.ToString()?.Length == 0
             )
            {
                return false;
            }

            return true;
        }
    }
}
