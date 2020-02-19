package sort;
import java.time.Duration;
import java.time.Instant;
import java.util.*;
import java.util.Date;

public class SortTest {
    public static void testSort(String sortName, int MAX, ISort sort){
        System.out.println(sortName);
        System.out.println("*********GENERATE INPUT...");
        java.util.Date a[] = new java.util.Date[MAX];
        for(int i = 0;i<MAX;i++){
            a[i] = new Date();
        }
        var start = Instant.now();
        sort.Sort(a);
        System.out.println("ELAPSED TIME:"+Duration.between(start, Instant.now()).toMillis());
    }
    public static void main(String[] args) {
        testSort("Сортировка выбором", 10000, new SelectionSort());
        testSort("Сортировка вставками", 10000, new InsertionSort());
    }
}
