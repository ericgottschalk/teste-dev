using System;

namespace TesteVogal
{
    public class CharStream : IStream
    {
        private int _index;
        public string Data { get; private set; }

        public CharStream(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            Data = value;
        }

        public char GetNext() => Data[_index++];

        public bool HasNext() => _index < Data.Length;
    }
}
