namespace colectare_deseuri.Services
{
    public static class TspSolver
    {
        public static List<int> NearestNeighborPartial(int[,] matrix, int indexStart, int indexStop, List<int> intermediari)
        {
            var vizitat = new HashSet<int>();
            List<int> ordine = new();

            int current = indexStart;

            while (vizitat.Count < intermediari.Count)
            {
                int next = -1, min = int.MaxValue;

                foreach (var i in intermediari)
                {
                    if (!vizitat.Contains(i) && matrix[current, i] < min)
                    {
                        min = matrix[current, i];
                        next = i;
                    }
                }

                if (next != -1)
                {
                    ordine.Add(next);
                    vizitat.Add(next);
                    current = next;
                }
            }

            return ordine;
        }

        public static int CalculeazaDistantaTotala(int[,] matrix, List<int> ruta)
        {
            int total = 0;
            for (int i = 0; i < ruta.Count - 1; i++)
                total += matrix[ruta[i], ruta[i + 1]];
            return total;
        }
    }
}
