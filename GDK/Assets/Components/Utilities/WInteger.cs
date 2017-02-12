
namespace GDK.Utilities
{
    /// <summary>
    /// Class to represent an integer which is wrapped between a minimum and maximum value.
    /// </summary>
    public class WInteger
    {
        private int minimum;
        private int maximum;

        public int Value { get; private set; }

        public WInteger(int value, int minimum, int maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
            Value = Wrap(value);
        }

        public void Add(int value)
        {
            Value += value;
            Value = Wrap(Value);
        }

        public void Subtract(int value)
        {
            Value -= value;
            Value = Wrap(Value);
        }

        public int Wrap(int value)
        {
            while (value < minimum)
            {
                value += (maximum - minimum);
            }
            while (value >= maximum)
            {
                value -= (maximum - minimum);
            }

            return value;
        }
    }
}