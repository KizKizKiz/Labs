package sort.string;

import java.util.ArrayList;
import java.util.Arrays;

public class LSD {
    private LSD(){ }
    //Сортирует массив строк с помощью алгоритма поразрядной сортировки LSD
    public static void Sort(String[] source, int W){
        var R = 256;
        var N = source.length;
        var temp = new String[source.length];
        for (int D = W-1; D>=0; D--){
            var countArr = new int[R+1];
            for (int i = 0; i < N; i++){
                countArr[source[i].charAt(D)+1]++;
            }
            for (int i = 0; i < R; i++){
                countArr[i+1] += countArr[i];
            }
            for (int i = 0; i < N; i++){
                temp[countArr[source[i].charAt(D)]++] = source[i];
            }
            for (int i = 0; i < N;i++){
                source[i] = temp[i];
            }
        }
    }
}
class Transpositor{
    private Transpositor() { }
    //Возвращает массив перестановок для данной строки
    public static String[] getTranspositions(String source){
        var transps = new ArrayList<String>();
        var chars = source.toCharArray();
        Arrays.sort(chars);
        var builder = new StringBuilder(String.valueOf(chars));
        transps.add(builder.toString());
        builder.toString();
        while (getSet(builder, source.length())) {
            transps.add(builder.toString());
        }
        return transps.toArray(new String[0]);
    }
    //Свапает два значения в строке
    private static void swap(StringBuilder builder, int i, int j)
    {
        var tmp = builder.charAt(j);
        builder.setCharAt(j, builder.charAt(i));
        builder.setCharAt(i, tmp);
    }
    //Генератор следующей перестановки
    private static boolean getSet(StringBuilder chars, int n)
    {
        var t = n - 2;
        while (t != -1 && chars.charAt(t) >= chars.charAt(t + 1))
            t--;
        if (t == -1)
            return false;
        int k = n - 1;
        while (chars.charAt(t) >= chars.charAt(k))
            k--;
            swap(chars, t, k);
        var left = t + 1;
        var right = n - 1;
        while (left<right)
            swap(chars, left++, right--);
        return true;
    }
}
