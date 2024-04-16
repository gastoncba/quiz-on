namespace quizon.Utils
{
    public static class RandomUtils
    {
        private static Random random = new Random();

        public static int[] GenerateRandomNumbers(int seed, int x, int count)
        {
            random = new Random(seed); 

            // Crear una lista con todos los números en el rango
            int[] numbers = new int[x];
            for (int i = 0; i < x; i++)
            {
                numbers[i] = i;
            }

            // Permutar la lista usando el algoritmo Fisher-Yates
            for (int i = x - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }

            // Seleccionar los primeros 'count' elementos de la lista permutada
            int[] result = new int[count];
            Array.Copy(numbers, result, count);
            return result;
        }
    }
}
