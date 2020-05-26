package tasks;
import java.util.ArrayList;
import java.util.List;
public class Searcher {
    private interface IPostProcess{
        void PostProcess(int[] arr);
    }
    private Searcher(){ }
    //Возвращает true если индексы находятся в границах массива, иначе false
    private static boolean CheckBoundary(char[][] chars, int row, int col) {
        var len = chars.length;
        return row < len && col < len && row >= 0 && col >= 0? true : false;
    }
    private static WordInfo GetWordGeneric(String word, char[][]chars, WordInfo wordInfo, IPostProcess process) {
        var charIndex = 0;
        var pair = new int[]{ wordInfo.Row(), wordInfo.Column() };
        while (CheckBoundary(chars, pair[0], pair[1])
                && word.length() > charIndex
                && word.charAt(charIndex) == chars[pair[0]][pair[1]]) {
            charIndex++;
            process.PostProcess(pair);
        }
        return charIndex == word.length() ? wordInfo : null;
    }
    //Возвращает массив типа WordInfo, который содержит информацию о каждом вхождении "word" в данной позиции
    private static List<WordInfo> GetWordsByPos(String word, char[][]chars, int row, int col){
        var paths = new ArrayList<WordInfo>();
        var left = GetWordGeneric(word, chars, new WordInfo(row,col,Direction.Left), (pair)-> --pair[1]);
        if(left!=null) paths.add(left);
        var leftup = GetWordGeneric(word, chars, new WordInfo(row,col,Direction.LeftUp), (pair)-> {--pair[1]; --pair[0]; });
        if(leftup!=null) paths.add(leftup);
        var up = GetWordGeneric(word, chars, new WordInfo(row,col,Direction.Up), (pair)-> --pair[0] );
        if(up!=null) paths.add(up);
        var rightup = GetWordGeneric(word, chars, new WordInfo(row,col,Direction.RightUp), (pair)-> { --pair[0]; ++pair[1]; });
        if(rightup!=null) paths.add(rightup);
        var right = GetWordGeneric(word, chars, new WordInfo(row,col,Direction.Right), (pair)-> ++pair[1]);
        if(right!=null) paths.add(right);
        var rightdown = GetWordGeneric(word, chars, new WordInfo(row,col,Direction.RightDown), (pair)-> { ++pair[0]; ++pair[1];});
        if(rightdown!=null) paths.add(rightdown);
        var down = GetWordGeneric(word, chars, new WordInfo(row,col,Direction.Down), (pair)-> ++pair[0]);
        if(down!=null) paths.add(down);
        var leftdown = GetWordGeneric(word, chars, new WordInfo(row,col,Direction.LeftDown), (pair)-> { ++pair[0]; --pair[1]; });
        if(leftdown!=null) paths.add(down);
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