package com.linkedlist;

public class SortedLinkedListEx extends SortedLinkedList<Double>{
    public double Median() {
        if (_median == null)
            return 0;
        if (_leftCount == _rightCount && Length() % 2 != 0)
            return _median.Get();
        if (Length() % 2 == 0 && _leftCount > _rightCount) {
            return (_median.Get() + _median.Left().Get()) / 2;
        }
        else
            return (_median.Get() + _median.Right().Get()) / 2;
    }
}
