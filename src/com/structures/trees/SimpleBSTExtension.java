package com.structures.trees;

import com.sun.source.tree.Tree;

public class SimpleBSTExtension {
    public static boolean IsBSTCorrect(SimpleBST tree){
        CheckBSTCorrectness(tree, tree.Root());
        return tree.IsBSTCorrect();
    }
    private static boolean IsLeftNodeAndParentCorrect(TreeNode left){
        if(left.GetParent()==null)
            return true;
        return left.GetValue().compareTo(left.GetParent().GetValue())<0;
    }
    private static boolean IsRightNodeAndParentCorrect(TreeNode parent){
        return parent.GetValue().compareTo(parent.GetRight().GetValue())<0;
    }
    private static void UpReference(TreeNode node, TreeNode newParent)
    {
        node.SetParent(newParent);
        newParent.SetLeft(node);
    }
    private static void CheckRight(SimpleBST tree,TreeNode root){
        if(tree.IsBSTCorrect() && root != null && root.GetRight()!=null)
        {
            if(!IsRightNodeAndParentCorrect(root)){
                tree.SetBSTCorrect(false);
                return;
            }
            var right = root.GetRight();
            if(root.GetParent()==null){
                right.SetParent(null);
                CheckBSTCorrectness(tree, right);
                return;
            }
            root.GetParent().SetLeft(right);
            right.SetParent(root.GetParent());
            CheckBSTCorrectness(tree, right);
        }
    }
    private static void CheckBSTCorrectness(SimpleBST tree,TreeNode root){
        if(!tree.IsBSTCorrect())
            return;
        if(root != null && root.GetLeft()!=null) {
            CheckBSTCorrectness(tree, root.GetLeft());
            if(!IsLeftNodeAndParentCorrect(root)){
                tree.SetBSTCorrect(false);
                return;
            }
        }
        CheckRight(tree, root);
        if(tree.IsBSTCorrect() && root != null && root.GetRight()==null && root.GetLeft()==null) {
            if(!IsLeftNodeAndParentCorrect(root)){
                tree.SetBSTCorrect(false);
                return;
            }
            if(root.GetParent()==null)
            {
                root=null;
                return;
            }
            root.GetParent().SetLeft(null);
            root.SetParent(null);
        }
    }
}