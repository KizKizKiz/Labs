package com.structures;

import java.util.Iterator;

//Реализация шаблонной очереди в виде двусвязного списка
public class Queue<T> implements Iterable<T>{
    private Node<T> m_tail;
    private Node<T> m_head;
    //Возвращает True если массив пуст, иначе false
    public boolean IsEmpty(){
        return m_head == null;
    }
    //Возвращает и удаляет из очереди элемент типа Т
    public T Dequeue(){
        if(m_head==null){
            return null;
        }
        var value = m_head.GetValue();
        if(m_head==m_tail)
        {
            m_head = null;
            m_tail = null;
            return value;
        }
        m_tail.SetNext(m_head.GetNext());
        m_head.GetNext().SetPrev(m_tail);
        m_head=m_head.GetNext();
        return value;
    }
    //Добавляет новый элемент типа T в очередь
    public void Enqueue(T value){
        var node = new Node<T>(value);
        node.SetPrev(m_tail);
        node.SetNext(m_head);
        if(m_head==null){
            m_head = node;
            m_tail = node;
            m_tail.SetNext(m_head);
            m_head.SetPrev(m_tail);
        }
        else {
            m_tail.SetNext(node);
            m_tail = node;
        }
    }
    //Возвращает элемент из очереди типа Т
    public T Peek(){
        if(!IsEmpty()) return m_head.GetValue();
        return null;
    }
    @Override
    public Iterator<T> iterator() { return new QueueIterator(this); }
    //Реализация итератора
    private static class QueueIterator<T> implements Iterator<T>{
        private Queue<T> m_queue;
        public QueueIterator(Queue<T> queue){ m_queue = queue; }
        @Override
        public boolean hasNext() { return !m_queue.IsEmpty(); }
        @Override
        public T next() { return m_queue.Dequeue(); }
    }
}