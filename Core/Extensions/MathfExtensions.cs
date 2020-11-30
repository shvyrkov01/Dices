namespace Extensions.Mathf
{
    public static class MathfExtensions
    {
        /// <summary>
        /// Округляет число, кратное 10 в большую сторону
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int RoundTenDigit(int number)
        {
            while (number % 10 != 0)
            {
                number++;
            }

            return number;
        }
    }
}