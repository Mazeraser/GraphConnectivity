namespace GraphConnectivity
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Отношение связности и достижимости ===");
            Console.Write("Введите размер матрицы (n): ");
            int n = int.Parse(Console.ReadLine());

            var adj = new BooleanMatrix(n);
            Console.WriteLine("Введите матрицу смежности (0 или 1):");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Строка {i}: ");
                var parts = Console.ReadLine().Split();
                for (int j = 0; j < n; j++)
                    adj[i, j] = int.Parse(parts[j]);
            }

            var reach = BooleanMatrix.ReachabilityMatrix(adj);
            var strong = BooleanMatrix.StrongConnectivityMatrix(reach);
            var components = BooleanMatrix.FindComponents(strong);

            adj.Print("Матрица смежности:");
            reach.Print("Матрица достижимости:");
            strong.Print("Матрица сильной связности:");

            Console.WriteLine("Компоненты сильной связности:");
            for (int i = 0; i < components.Count; i++)
                Console.WriteLine($"Компонента {i + 1}: {{{string.Join(", ", components[i])}}}");
        }
    }
}