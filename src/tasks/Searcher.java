package tasks;

import java.util.ArrayList;
import java.util.List;

public class Searcher {
    private Searcher(){ }

    //Возвращает true если индексы находятся в границах массива, иначе false
    private static boolean CheckBoundary(char[][] chars, int row, int col)
    {
        var len = chars.length;
        return row < len && col < len && row >= 0 && col >= 0? true : false;
    }

    //Возвращает массив типа WordInfo, который содержит информацию о каждом вхождении "word" в данной позиции
    private static List<WordInfo> GetWordsByPos(String word, char[][]chars, int row, int col){
        var paths = new ArrayList<WordInfo>();
        //LEFT
        if(CheckBoundary(chars, row, col-1) && word.charAt(1)==chars[row][col-1]){
            var prev = col;
            prev--;
            var charIndex = 2;
            while(CheckBoundary(chars, row, --prev)
                    && word.length() > charIndex
                    && word.charAt(charIndex)==chars[row][prev])
                charIndex++;

            if (charIndex == word.length()){
                paths.add(new WordInfo(row, col, Direction.Left));
            }
        }
        //LEFT-UP
        if(CheckBoundary(chars, row-1, col-1) && word.charAt(1)==chars[row-1][col-1]){
            var prevCol = col;
            var prevRow = row;
            prevCol--;
            prevRow--;
            var charIndex = 2;
            while(CheckBoundary(chars, --prevRow, --prevCol)
                    && word.length() > charIndex
                    && word.charAt(charIndex)==chars[prevRow][prevCol])
                charIndex++;

            if (charIndex == word.length()){
                paths.add(new WordInfo(row, col, Direction.LeftUp));
            }
        }
        //UP
        if(CheckBoundary(chars, row-1, col) && word.charAt(1)==chars[row-1][col]){
            var prevRow = row;
            prevRow--;
            var charIndex = 2;
            while(CheckBoundary(chars, --prevRow, col)
                    && word.length() > charIndex
                    && word.charAt(charIndex)==chars[prevRow][col])
                charIndex++;

            if (charIndex == word.length()){
                paths.add(new WordInfo(row, col, Direction.Up));
            }
        }
        //Right-up
        if(CheckBoundary(chars, row-1, col+1) && word.charAt(1)==chars[row-1][col+1]){
            var prevCol = col;
            var prevRow = row;
            prevCol++;
            prevRow--;
            var charIndex = 2;
            while(CheckBoundary(chars, --prevRow, ++prevCol)
                    && word.length() > charIndex
                    && word.charAt(charIndex)==chars[prevRow][prevCol])
                charIndex++;

            if (charIndex == word.length()){
                paths.add(new WordInfo(row, col, Direction.RightUp));
            }
        }
        //Right
        if(CheckBoundary(chars, row, col+1) && word.charAt(1)==chars[row][col+1]){
            var prevCol = col;
            prevCol++;
            var charIndex = 2;
            while(CheckBoundary(chars, row, ++prevCol)
                    && word.length() > charIndex
                    && word.charAt(charIndex)==chars[row][prevCol])
                charIndex++;

            if (charIndex == word.length()){
                paths.add(new WordInfo(row, col, Direction.Right));
            }
        }
        //Right-Down
        if(CheckBoundary(chars, row+1, col+1) && word.charAt(1)==chars[row+1][col+1]){
            var prevCol = col;
            var prevRow = row;
            prevCol++;
            prevRow++;
            var charIndex = 2;
            while(CheckBoundary(chars, ++prevRow, ++prevCol)
                    && word.length() > charIndex
                    && word.charAt(charIndex)==chars[prevRow][prevCol])
                charIndex++;

            if (charIndex == word.length()){
                paths.add(new WordInfo(row, col, Direction.RightDown));
            }
        }
        //Down
        if(CheckBoundary(chars, row+1, col) && word.charAt(1)==chars[row+1][col]){
            var prevRow = row;
            prevRow++;
            var charIndex = 2;
            while(CheckBoundary(chars, ++prevRow, col)
                    && word.length() > charIndex
                    && word.charAt(charIndex)==chars[prevRow][col])
                charIndex++;

            if (charIndex == word.length()){
                paths.add(new WordInfo(row, col, Direction.Down));
            }
        }
        //Left-Down
        if(CheckBoundary(chars, row+1, col-1) && word.charAt(1)==chars[row+1][col-1]){
            var prevCol = col;
            var prevRow = row;
            prevCol--;
            prevRow++;
            var charIndex = 2;
            while(CheckBoundary(chars, ++prevRow, --prevCol)
                    && word.length() > charIndex
                    && word.charAt(charIndex)==chars[prevRow][prevCol])
                charIndex++;

            if (charIndex == word.length()){
                paths.add(new WordInfo(row, col, Direction.LeftDown));
            }
        }
        return paths;
    }

    //Возвращает массив типа WordInfo, который содержит информацию о каждом вхождении "word"
    public static List<WordInfo> GetWords(String word, char[][] chars, int N){
        var wordsInfo = new ArrayList<WordInfo>();
        for (int row = 0; row < N; row++){
            for (int col = 0; col < N; col++){
                if(chars[row][col]==word.charAt(0)){
                    wordsInfo.addAll(GetWordsByPos(word, chars, row, col));
                }
            }
        }
        return wordsInfo;
    }
}
