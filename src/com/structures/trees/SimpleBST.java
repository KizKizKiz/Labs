package com.structures.trees;

public class SimpleBST<TValue extends Comparable<TValue>> {
    private TreeNode _root;
    protected TreeNode Root(){
        return _root;
    }
    private boolean _isBSTCorrect = true;
    protected boolean IsBSTCorrect(){
        return _isBSTCorrect;
    }
    protected void SetBSTCorrect(boolean value){
        _isBSTCorrect = value;
    }
    public int Length(){ return _root==null? 0 : _root.GetSize(); }
    public boolean IsEmpty() {
        return _root==null;
    }
    public void Add(TValue value){
        _root = AddInternal(_root, null, value);
    }
    protected int Size(TreeNode node){
        return node==null? 0:node.GetSize();
    }
    private TreeNode AddInternal(TreeNode localRoot, TreeNode parent, TValue value) {
        if(localRoot==null)
            return new TreeNode(value, parent, 1);

        int cmp = localRoot.GetValue().compareTo(value);
        if (cmp < 0) localRoot.SetRight(AddInternal(localRoot.GetRight(), localRoot, value));
        else if (cmp > 0) localRoot.SetLeft(AddInternal(localRoot.GetLeft(), localRoot, value));
        else localRoot.SetValue(value);
        localRoot.SetSize(Size(localRoot.GetLeft()) + Size(localRoot.GetRight())+1);
        return localRoot;
    }
}