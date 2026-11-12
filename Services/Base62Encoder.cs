namespace URL_Shortner.Services
{
    public static class Base62Encoder
    {
        private const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string Encode(long number)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(number);

            if (number == 0) return Alphabet[0].ToString();

            Span<char> codeBuffer = stackalloc char[11]; // Can use char array but Span is good since it get cleaned after the method finish
            var position = codeBuffer.Length;

            while (number > 0)
            {
                codeBuffer[--position] = Alphabet[(int) (number % Alphabet.Length)]; // The conversion is from right to left that's why we start from the end
                number /= Alphabet.Length;
            }
            return new string(codeBuffer[position..]); // A cool way to wrtie Span.Slice(start, length)
        }
    }
}
