package com.structures.linkedlist;

public class SortedLinkedList<T extends Comparable> implements Iterable<T>{
    protected class Node<T>{
        public Node(T value){
            _value = value;
        }
        private T _value;
        public T Get(){ return _value; }
        private Node<T> _left;
        public Node<T> Left() { return _left; }
        private Node<T> _right;
        public Node<T> Right(){ return _right; }
    }
    private int _length;
    public int Length() { return _length; }
    protected int _leftCount;
    protected int _rightCount;
    private Node<T> _root;
    protected Node<T> _median;
    public void Add(T value){
        InternalAdd(value);
    }
    private void InternalAdd(T value){
        var node = new Node<T>(value);
        if(_root==null){
            _root = node;
            _median=_root;
            _length++;
            return;
        }
        var insertPtr = _root;
        while(insertPtr._right != null
                && (insertPtr._right._value.compareTo(value)==-1
                || insertPtr._right._value.compareTo(value)==0))
        {
            insertPtr = insertPtr._right;
        }
        if(insertPtr==_root && insertPtr._value.compareTo(value)==1){
            _root._left=node;
            node._right=_root;
            _root=node;
        }
        else if(insertPtr._right==null){
            node._left=insertPtr;
            insertPtr._right=node;
        }
        else{
            node._right=insertPtr._right;
            insertPtr._right._left=node;
            insertPtr._right = node;
            node._left = insertPtr;
        }
        ++_length;
        RestoreMedian(value);
    }
    private void RestoreMedian(T value){
        if (value.compareTo(_median._value)==0 || value.compareTo(_median._value) == 1)
            _rightCount++;
        else _leftCount++;

        if(_leftCount-_rightCount==2)
        {
            _median = _median._left;
            _rightCount++;
            _leftCount--;
        }
        else if(_leftCount-_rightCount==-2)
        {
            _median = _median._right;
            _rightCount--;
            _leftCount++;
        }
    }
    @Override
    public Iterator iterator() {
        return new Iterator();
    }
    private class Iterator implements java.util.Iterator<T> {
        public Iterator(){
            _localRoot = _root;
        }
        private Node<T> _localRoot;
        @Override
        public boolean hasNext() {
            return _localRoot!=null;
        }
        @Override
        public T next() {
            var value = _localRoot._value;
            _localRoot = _localRoot._right;
            return value;
        }
    }
}
