using System;

namespace Common.Libraries.Objects
{
    public class EnumPair<T> where T: struct
    {
        public EnumPair(string text, T value)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public T Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
