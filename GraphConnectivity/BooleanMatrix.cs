namespace GraphConnectivity
{
    public class BooleanMatrix
    {
        private int[,] data;
        public int Size { get; private set; }

        public BooleanMatrix(int n)
        {
            Size = n;
            data = new int[n, n];
        }

        public int this[int i, int j]
        {
            get => data[i, j];
            set => data[i, j] = value == 0 ? 0 : 1;
        }

        public static BooleanMatrix Or(BooleanMatrix a, BooleanMatrix b)
        {
            var res = new BooleanMatrix(a.Size);
            for (int i = 0; i < a.Size; i++)
            for (int j = 0; j < a.Size; j++)
                res[i, j] = a[i, j] | b[i, j];
            return res;
        }

        public static BooleanMatrix Multiply(BooleanMatrix a, BooleanMatrix b)
        {
            var res = new BooleanMatrix(a.Size);
            for (int i = 0; i < a.Size; i++)
            for (int j = 0; j < a.Size; j++)
            for (int k = 0; k < a.Size; k++)
                if (a[i, k] == 1 && b[k, j] == 1)
                {
                    res[i, j] = 1;
                    break;
                }

            return res;
        }

        public BooleanMatrix Transpose()
        {
            var res = new BooleanMatrix(Size);
            for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                res[i, j] = data[j, i];
            return res;
        }

        public static BooleanMatrix Power(BooleanMatrix m, int power)
        {
            var result = m;
            for (int p = 1; p < power; p++)
                result = Multiply(result, m);
            return result;
        }

        public static BooleanMatrix ReachabilityMatrix(BooleanMatrix adj)
        {
            int n = adj.Size;
            var sum = new BooleanMatrix(n);
            for (int i = 0; i < n; i++) sum[i, i] = 1;

            var power = adj;
            for (int k = 1; k < n; k++)
            {
                sum = Or(sum, power);
                power = Multiply(power, adj);
            }

            return sum;
        }

        public static BooleanMatrix StrongConnectivityMatrix(BooleanMatrix reach)
        {
            var trans = reach.Transpose();
            var strong = new BooleanMatrix(reach.Size);
            for (int i = 0; i < reach.Size; i++)
            for (int j = 0; j < reach.Size; j++)
                strong[i, j] = reach[i, j] & trans[i, j];
            return strong;
        }

        public static List<List<int>> FindComponents(BooleanMatrix strong)
        {
            int n = strong.Size;
            bool[] used = new bool[n];
            var components = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                if (used[i]) continue;
                var comp = new List<int>();
                for (int j = 0; j < n; j++)
                    if (strong[i, j] == 1)
                    {
                        comp.Add(j);
                        used[j] = true;
                    }

                components.Add(comp);
            }

            return components;
        }

        public void Print(string title = "")
        {
            if (!string.IsNullOrEmpty(title))
                Console.WriteLine(title);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                    Console.Write(data[i, j] + " ");
                Console.WriteLine();
            }
        }
    }
}