package com.structures.hashset;
import java.lang.reflect.Array;
import java.time.Duration;
import java.time.Instant;
import java.util.Random;
import java.util.Scanner;

public class HashSetClient {
    private static String getRandomStr(){
        var rand = new Random();
        var chars = "qwertyuiopasdfghjklzxcvbnnm";
        var strFast = new StringBuilder();
        for (int i = 0; i < chars.length()*2; i++) {
            strFast.append(chars.charAt(rand.nextInt(chars.length())));
        }
        return strFast.toString();
    }
    private static void AutoTest(){
        var rnd = new Random();
        for (int max = 10; max <= Math.pow(10,7); max*=10) {
            System.out.println("LENGTH: "+max);
            var arr = new Comparable[max];
            var start = Instant.now();
            for (int i = 0; i < max; i++) {
                var num = rnd.nextInt(max);
                arr[i] = num;
            }
            start = Instant.now();
            HashSetEx.FrequencySort(arr);
            System.out.println("FrequencySort TIME: "+ Duration.between(start, Instant.now()).toMillis());
        }
    }
    interface IScannerFetcher<T>{
        T getData(Scanner scan);
    }
    public static <T extends Comparable> void ManualTest(IScannerFetcher fetcher){
        var scan = new Scanner(System.in);
        System.out.print("Enter length:");
        var len = scan.nextInt();
        var arr = new Comparable[len];
        scan.nextLine();
        for (int i = 0; i < len; i++) {
            System.out.print("Enter element:");
            arr[i] = (T)fetcher.getData(scan);
        }
        HashSetEx.FrequencySort(arr);
        for (var x:arr) {
            System.out.print("["+x+"]"+" ");
        }
        System.out.println();
    }
    public static void main(String[] args) {
        ManualTest((IScannerFetcher<Integer>) (s) -> s.nextInt());
        ManualTest((IScannerFetcher<String>) (s) -> s.nextLine());
        AutoTest();
    }
}