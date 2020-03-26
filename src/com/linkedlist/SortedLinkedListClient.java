package com.linkedlist;
import java.util.Random;

public class SortedLinkedListClient {
    public static void TestMedianOddLength(){
        var linkedList = new SortedLinkedListEx();
        var rnd = new Random();
        for (int i = 0; i < 15; i++) {
            linkedList.Add(rnd.nextInt(50) * 1.0);
        }
        for (var x: linkedList)
            System.out.print(x+" ");
        System.out.println("\nMEDIAN: "+linkedList.Median());
    }
    public static void TestMedianEvenLength(){
        var linkedList = new SortedLinkedListEx();
        var rnd = new Random();
        for (int i = 0; i < 16; i++) {
            linkedList.Add(rnd.nextInt(50) * 1.0);
        }
        for (var x: linkedList)
            System.out.print(x+" ");
        System.out.println("\nMEDIAN: "+linkedList.Median());
    }
    public static void main(String[] args) {
        TestMedianEvenLength();
        TestMedianOddLength();
    }
}