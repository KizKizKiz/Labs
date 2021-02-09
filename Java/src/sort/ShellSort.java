package sort;

public class ShellSort extends AbstractSort{
    public void Sort(Comparable[] source)
    {
        int step = 1;
        while (step < source.length / 3) step = step * 3 + 1;
        while (step>=1)
        {
            for (int i = step; i < source.length; i++)
            {
                for (int j = i; j >=step && less(source[j],source[j-step]); j-=step)
                {
                    exch(source, j, j-step);
                }
            }
            step /= 3;
        }
    }

    @Override
    public String GetSortName() {
        return "Shell sort";
    }
}
