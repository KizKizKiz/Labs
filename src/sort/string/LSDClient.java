package sort.string;
import java.time.Duration;
import java.time.Instant;
import java.util.Random;
import java.util.Scanner;

public class LSDClient {
    private static Random _rnd = new Random();

    //Возвращает рандомную строку шириной Width
    private static String getRandomString(int width){
        var template = "qwertyuiopasdfghjklzxcvbnm";
        var str = new StringBuilder();
        for (int i = 0; i < width; i++){
            str.append(template.charAt(_rnd.nextInt(template.length())));
        }
        return str.toString();
    }

    //Реализация теста с замером
    private static void Test(String source){
        var start = Instant.now();
        var strings = Transpositor.getTranspositions(source);
        var time = Duration.between(start, Instant.now()).toMillis();
        System.out.println("Get transpositions: "+time+" ms");
        start = Instant.now();
        LSD.Sort(strings, source.length());
        time = Duration.between(start, Instant.now()).toMillis();
        System.out.println("LSD Sort: "+time+" ms");
        for (int i = 0; i< strings.length; i++)
        {
            System.out.println((i+1)+": "+strings[i]);
        }
    }
    //Ручной тест. Необходимо ввести строку
    private static void ManualTest(){
        System.out.print("Enter any string:");
        Test(new Scanner(System.in).nextLine());
    }
    //Автотест. Требует длины строки
    private static void AutoTest(){
        System.out.print("Enter width string:");
        var width = new Scanner(System.in).nextInt();
        Test(getRandomString(width));
    }
    public static void main(String[] args) {
        AutoTest();
        ManualTest();
    }
}
