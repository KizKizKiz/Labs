package tasks;

//Содержит информацию о вхождении слова в сетку
public class WordInfo{
    public WordInfo(int row, int column, Direction direction)
    {
        _row = row;
        _column = column;
        _direction = direction;
    }
    private int _row;
    public int Row() { return _row; }

    private int _column;
    public int Column() { return _column; }

    private Direction _direction;
    public Direction Direction() { return _direction; }
}

//Направление слова
enum Direction{
    Left,
    LeftUp,
    Up,
    RightUp,
    Right,
    RightDown,
    Down,
    LeftDown
}
