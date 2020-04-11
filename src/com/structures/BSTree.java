package com.structures;

public class BSTree<TValue extends Comparable<TValue>> {
    private class Node{
        private Node _parent, _left, _right;
        private TValue _value;
        public Node(TValue value, int size)
        {
            this(value, null, size);
        }
        public Node(TValue value, Node parent, int size){
            _value = value;
            _parent = parent;
            _size = size;
        }
        private boolean _isCorrect;
        private int _size;
    }
    private Node _root;
    public int Length(){ return _root==null? 0 : _root._size; }
    public boolean IsEmpty() {
        return _root==null;
    }
    public void Add(TValue value){
        _root = AddInternal(_root, _root, value);
    }
    private int Size(Node node){
        return node==null? 0:node._size;
    }
    private Node AddInternal(Node localRoot, Node localParent, TValue value) {
        if(localRoot==null)
            return new Node(value, localParent, 1);

        int cmp = localRoot._value.compareTo(value);
        if (cmp < 0) localRoot._right = AddInternal(localRoot._right, localRoot, value);
        else if (cmp > 0) localRoot._left = AddInternal(localRoot._left, localRoot, value);
        else localRoot._value = value;
        localRoot._size = Size(localRoot._left) + Size(localRoot._right)+1;
        return localRoot;
    }
    private boolean IsCorrect(Node node){ return node==null? true : node._isCorrect; }
    public void ConvertToBinHeap(){
        ConvertToBinHeapInternal(_root, _root);
    }
    private void UpReference(Node node, Node newParent){
        if(newParent==null) {
            _root = node;
            _root._parent = null;
            return;
        }
        node._parent = newParent;
        newParent._left = node;
    }
    private void RefreshSize(Node node){
        node._size = Size(node._left) + Size(node._right)+1;
    }
    private void DownBranch(Node node){
        var newParent = SearchNewBranch(node._left, node);
        if(node._parent!=null && node._parent._right==node){
            node._parent._right=null;
        }
        node._parent = newParent;
        var oldLeft = node._left;
        node._left = null;
        if(newParent._left==null)
            newParent._left = node;
        else
            newParent._right = node;

        while(node != oldLeft){
            RefreshSize(node);
            node = node._parent;
        }
        RefreshSize(node);
    }
    private Node SearchNewBranch(Node node, Node parent){
        if(node == null) return parent;
        if(node._left==null && node._right==null){
            return node;
        }
        else if(Size(node._left)>Size(node._right)){
            return SearchNewBranch(node._right, node);
        }
        else if(Size(node._left)<Size(node._right)){
            return SearchNewBranch(node._left, node);
        }
        else
            return SearchNewBranch(node._left, node);
    }
    private void ConvertToBinHeapInternal(Node localRoot, Node localParent){
        if(localRoot._left != null){
            ConvertToBinHeapInternal(localRoot._left, localRoot);
            UpReference(localRoot._left, localRoot._parent);
            DownBranch(localRoot);
            localRoot._isCorrect = true;
            ConvertToBinHeapInternal(localRoot, localRoot._parent);
        }
        else if(localRoot._right != null) {
            ConvertToBinHeapInternal(localRoot._right, localRoot);
            if(IsCorrect(localRoot._left) && IsCorrect(localRoot._right)){
                localRoot._isCorrect = true;
            }
        }
        else
            localRoot._isCorrect = true;
    }
}