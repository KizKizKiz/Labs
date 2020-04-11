package com.heap;

import java.util.Random;

public class BinaryHeapClient {
    public static void main(String[] args) {
        for(int i=0;i<3; i++) {
            var heap = new BinaryHeap();
            var rnd = new Random();
            for (int j = 0; j < 10; j++)
                heap.Add(rnd.nextInt(100) + 1);
            for (var x : heap) {
                System.out.print("[" + x + "]");
            }
            System.out.println();
            System.out.println("Minimal cost: " + heap.FindMinCost());
        }
    }
}