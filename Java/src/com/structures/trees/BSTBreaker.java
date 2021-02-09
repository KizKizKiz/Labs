package com.structures.trees;

import java.rmi.NoSuchObjectException;

public class BSTBreaker<T extends Comparable<T>> extends SimpleBST<T>{
    public void Replace(T find, T replace) throws NoSuchObjectException {
        if(find == null || replace==null)
            throw new NullPointerException("find or replace is null");
        if(Root()==null)
            throw new NullPointerException("root is null");

        var node = Find(Root(), find);
        if(node==null)
            throw new NoSuchObjectException("Could not find "+find+" object");
        node.SetValue(replace);
    }
    private TreeNode Find(TreeNode root, T find){
        if(root==null)
            return null;
        if(root.GetValue().compareTo(find)==0) return root;
        if(root.GetValue().compareTo(find)>0){
            return Find(root.GetLeft(),find);
        }
        else
            return Find(root.GetRight(),find);
    }
}
