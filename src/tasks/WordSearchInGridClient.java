package tasks;

import java.util.Scanner;

public class WordSearchInGridClient{

    //Ручное заполнение сетки
    private static void ManualGenGrid(){
        System.out.print("Enter grid (N x N) size: ");
        var N = new Scanner(System.in).nextInt();
        var chars = new char[N][N];
        var rowIndex = 0;
        while(rowIndex!=N){
            System.out.print(String.format("Enter string which length equals %d:", N));
            var row = new Scanner(System.in).nextLine();
            if(row.length()!=N){
                System.out.println(String.format("Error: string length must equals %d", N));
                continue;
            }
            chars[rowIndex] = row.toUpperCase().toCharArray();
            rowIndex++;
        }
        Test(chars);
    }

    //Реализация теста
    private static void Test(char[][]chars){
        CharGrid.printGrid(chars, chars.length);
        do {
            System.out.print("Enter word (length >= 2) or quit(q):");
            var word = new Scanner(System.in).nextLine();

            if(word.toLowerCase().equals("q"))
                break;
            if(word.length()<2) {
                System.out.println("Error: length less than 2");
                continue;
            }

            var paths = Searcher.GetWords(word.toUpperCase(), chars, chars.length);
            if(paths.size()==0)
                System.out.println("Word NOT FOUND");
            else
                for (var path:paths) {
                    System.out.println(String.format("Word \"%s\" FOUND\n\tRow: %d\n\tColumn: %d\n\tDirection: %s",
                            word, path.Row()+1, path.Column()+1, path.Direction().toString()));
                }
        } while(true);
    }

    //Автогенерируемая сетка
    private static void AutoGenGrid(){
        System.out.print("Enter grid (N x N) size: ");
        var N = new Scanner(System.in).nextInt();
        Test(CharGrid.generate(N));
    }
    public static void main(String[] args) {
        ManualGenGrid();
        AutoGenGrid();
    }
}