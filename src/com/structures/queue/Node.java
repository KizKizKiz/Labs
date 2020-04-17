package com.structures.queue;

public class Node<T>
{
    private T m_value;
    private Node<T> m_next;
    private Node<T> m_prev;
    public Node(T value)
    {
        m_value = value;
    }
    public void SetNext(Node<T> node){
        this.m_next = node;
    }
    public Node<T> GetNext(){
        return m_next;
    }
    public Node<T> GetPrev(){
        return m_prev;
    }
    public void SetPrev(Node<T> prev){
        this.m_prev = prev;
    }
    public T GetValue(){
        return m_value;
    }
}
