package com.heap;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public class BinaryHeap implements Iterable<Integer>{
    private List<Integer> _list = new ArrayList<Integer>();
    public int HeapSize(){ return _list.size(); }
    public void Add(Integer value){
        _list.add(value);
        int i = HeapSize()-1;
        int parent = (i-1)/2;
        while(i>0 && _list.get(parent) > _list.get(i))
        {
            var tmp = _list.get(parent);
            _list.set(parent, _list.get(i));
            _list.set(i, tmp);
            i=parent;
            parent = (i-1)/2;
        }
    }
    private Integer MinDescendatsIndex(int rootIndex){
        if(_list.size()-1 < rootIndex*2+1)
            return -1;
        if(_list.get(rootIndex*2+1)==-1) {
            if (rootIndex * 2 + 2 > _list.size() - 1)
                return -1;
            return _list.get(rootIndex*2+2)==-1? -1 : rootIndex*2+2;
        }
        if(_list.size()-1 < rootIndex*2+2) {
            return rootIndex*2+1;
        }
        if(_list.get(rootIndex*2+2)==-1)
            return rootIndex*2+1;
        return _list.get(rootIndex*2+1)>_list.get(rootIndex*2+2)?
                rootIndex*2+2 : rootIndex*2+1;
    }
    private boolean IsEmpty()
    { return _list.get(0)==-1; }

    private void Remove(int index)
    { _list.set(index, -1); }
    private void Swap(int index1, int index2){
        var temp = _list.get(index1);
        _list.set(index1, _list.get(index2));
        _list.set(index2, temp);
    }
    public Integer FindMinCost() {
        var cost = 0;
        while (!IsEmpty()) {
            var min1 = _list.get(0);
            var minIndex2 = MinDescendatsIndex(0);
            if(minIndex2==-1) {
                break;
            }
            var summ = min1 + _list.get(minIndex2);
            Remove(minIndex2);
            _list.set(0, summ);
            RestoreHeap(minIndex2, false);
            RestoreHeap(0, true);
            cost += summ;
        }
        return cost;
    }
    private void RestoreHeap(int rootIndex, boolean max){
        var indexMin = MinDescendatsIndex(rootIndex);
        if(indexMin==-1)
            return;
        if(max){
            if(_list.get(indexMin)<_list.get(rootIndex))
            {
                Swap(rootIndex, indexMin);
                RestoreHeap(indexMin, max);
            }
        }
        else {
            Swap(rootIndex, indexMin);
            RestoreHeap(indexMin, max);
        }
    }
    @Override
    public Iterator<Integer> iterator() {
        return _list.listIterator();
    }
}