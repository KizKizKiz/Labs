package com.structures;
import java.util.*;
import java.util.stream.IntStream;
public class QueueClientTest {
    private static Random m_rand = new Random();
    private final static String m_str = "ABCDEFGHIKOPQRS";
    private static String GetRandomString(){
        var result = new StringBuilder();
        for(int i = 0;i<10;i++){
            result.append(m_str.charAt(m_rand.nextInt(15)));
        }
        return result.toString();
    }
    public static <T> void Test(Queue<T> queue)
    {
        System.out.println("****QUEUE****");
        for (var node:queue) {
            System.out.println(node);
        }
        if(queue.IsEmpty())
            System.out.println("****QUEUE IS EMPTY****");
    }
    private static void TestQueue() {
        var strQueue = new Queue<String>();
        IntStream.range(0,10).forEach(c->strQueue.Enqueue(GetRandomString()));
        var intQueue = new Queue<Integer>();
        IntStream.range(0,10).forEach(intQueue::Enqueue);
        Test(strQueue);
        Test(intQueue);
    }

    public static void main(String[] args) {
        TestQueue();
    }
}

