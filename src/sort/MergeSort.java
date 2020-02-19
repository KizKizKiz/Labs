package sort;

public class MergeSort extends AbstractSort
{
        public void Sort(Comparable[] source)
        {
                var m_new = new Comparable[source.length];
                Sort(m_new , source, 0, source.length - 1);

                Merge(m_new, source, 0, 0+source.length-1/2, source.length-1);
                source = m_new;
        }
        private void Sort(Comparable []arr, Comparable[] source, int low, int high)
        {
                if (high <= low) return;
                int mid = low + (high - low) / 2;
                Sort(arr, source, low, mid);
                Sort(arr, source, mid + 1, high);
                Merge(arr, source, low, mid, high);
        }
        private void Merge(Comparable []arr, Comparable []source, int low, int mid, int high)
        {
                int i = low, j=mid+1;
                for (int k = low; k <= high; k++) arr[k]=source[k];
                for (int k = low; k <= high; k++)
                {
                        if (i > mid) source[k] = arr[j++];
                        else if (j > high) source[k] = arr[i++];
                        else if (arr[j].compareTo(arr[i]) == -1) source[k] = arr[j++];
                        else source[k] = arr[i++];
                }
        }

        @Override
        public String GetSortName() {
                return "Merge sort";
        }
}
