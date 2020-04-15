package com.structures.trees;

import java.time.Duration;
import java.time.Instant;
import java.util.Random;
import java.util.Scanner;
//Тестовый клиент на больших данных
//Среднее время ~2сек. для 10^7 элементов
public class BSTreeClient {
    private static Random _rnd = new Random();
    private static void ManualTest() {
        var tree = new BSTree<Integer>();
        var scanner = new Scanner(System.in);
        int capacity = 0;
        System.out.print("Enter size:");
        capacity = scanner.nextInt();
        while (capacity!=0)
        {
            System.out.print("Enter number:");
            tree.Add(scanner.nextInt());
            capacity--;
        }
        var start = Instant.now();
        tree.ConvertToBinHeap();
        System.out.println("***Length: "+tree.Length()+"\n"
                +"TIME: "+ Duration.between(start, Instant.now()).toMillis()+"ms");
    }
    private static void AutoTest(){
        var tree = new BSTree<Integer>();
        for (int i = 10; i < Math.pow(10,7); i*=10) {
            for (int j = 0; j < i; j++) {
                tree.Add(_rnd.nextInt(i*5));
            }
            var start = Instant.now();
            tree.ConvertToBinHeap();
            System.out.println("***Length: "+tree.Length()+"\n"
                    +"TIME: "+ Duration.between(start, Instant.now()).toMillis()+"ms");
        }
    }
    public static void main(String[] args) {
        AutoTest ();
        ManualTest();
    }
}