package com.structures.hashset;

import java.util.ArrayList;
import java.util.Comparator;

//Cannot create object, instead use static functions
public class HashSetEx {
    private HashSetEx(){ }
    public static void FrequencySort(Comparable[] source){
        var hashSet = new HashSet<>();
        for (int i = 0; i < source.length; i++) {
            hashSet.add(source[i]);
        }
        var pairs = hashSet.getPairs();
        var arr = new ArrayList<ValueOccurences>();
        for (int i = 0; i < hashSet.capacity(); i++) {
            if(pairs[i]!=null)
                arr.add(pairs[i]);
        }
        arr.sort(new ValueOccurComparison());
        var index = 0;
        for (var x:arr) {
            for (int i = 0; i < x._occur; i++) {
                source[index] = x._value;
                index++;
            }
        }
    }
    private static class ValueOccurComparison implements Comparator<ValueOccurences> {
        @Override
        public int compare(ValueOccurences o1, ValueOccurences o2) {
            if(o1._occur == o2._occur){
                return o1._value.compareTo(o2._value);
            }
            return o1._occur.compareTo(o2._occur);
        }
    }
}
