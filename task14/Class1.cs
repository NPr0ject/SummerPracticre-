namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double result = 0.0;
        double integralLength = b - a;

        int n = (int)(integralLength / step);
        if (n == 0) n = 1;

        double h = integralLength / n;

        using (var countdown = new CountdownEvent(threadsnumber))
        {
            int asd = n / threadsnumber;
            int dsa = n % threadsnumber;

            for (int i = 0; i < threadsnumber; ++i)
            {
                int start = i * asd;

                int end = (i == threadsnumber - 1) ? start + asd + dsa : start + asd;

                ThreadPool.QueueUserWorkItem(f =>
                {
                    double localres = 0.0;

                    for (int j = start; j < end; ++j)
                    {
                        double x1 = a + j * h;
                        double x2 = a + (j + 1) * h;

                        localres += (function(x1) + function(x2)) * h / 2;
                    }

                    Interlocked.Exchange(ref result, result + localres);

                    countdown.Signal();
                });
            }
            countdown.Wait();
        }

        return result;
    }
}
