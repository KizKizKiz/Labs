package com.structures.trees;

public class TreeNode<TValue extends Comparable<TValue>>{
    private TreeNode _left, _parent, _right;
    public void SetParent(TreeNode parent){
        _parent = parent;
    }
    public TreeNode GetParent(){
        return _parent;
    }
    public void SetLeft(TreeNode left){
        _left = left;
    }
    public TreeNode GetLeft(){
        return _left;
    }
    public void SetRight(TreeNode right){ _right = right; }
    public TreeNode GetRight(){ return _right; }
    public TreeNode(TValue value, TreeNode parent, int size){
        SetValue(value);
        SetParent(parent);
        SetSize(size);
    }
    private TValue _value;
    public void SetValue(TValue value){
        _value = value;
    }
    public TValue GetValue(){
        return _value;
    }
    private int _size;
    public void SetSize(int size){
        _size = size;
    }
    public int GetSize(){
        return _size;
    }
}