package com.structures.hashset;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;

class ValueOccurences<TValue extends Comparable>{
    protected TValue _value;
    protected Integer _occur;
    public ValueOccurences(TValue value, int occur){
        _occur = occur;
        _value = value;
    }
}
public class HashSet<TValue extends Comparable> implements Iterable<TValue> {
    @Override
    public Iterator<TValue> iterator() {
        return new HashSetIterator();
    }
    private class HashSetIterator implements Iterator<TValue>{
        private ArrayList<Integer> _indexes;
        public HashSetIterator(){
            _indexes = new ArrayList<>();
            for (int i = 0; i < _pairs.length; i++) {
                if(_pairs[i]!=null)
                    _indexes.add(i);
            }
            _index = _indexes.size()!=0? _indexes.get(0) : -1;
        }
        private int _index;
        @Override
        public boolean hasNext() {
            return _indexes.size()!=_index;
        }
        @Override
        public TValue next() {
            var value = _pairs[_indexes.get(_index)];
            _index++;
            return (TValue) value._value;
        }
    }
    protected ValueOccurences[] getPairs(){
        return _pairs;
    }
    protected List<Comparable> sort(){
        Arrays.sort(_pairs);
        var sortedList = new ArrayList<Comparable>();
        for (var x:_pairs) {
            if(x!=null)
                for (int i = 0; i < x._occur; i++) {
                    sortedList.add(x._value);
                }
        }
        return sortedList;
    }
    private int _capacity;
    protected int capacity(){
        return _capacity;
    }
    private ValueOccurences[] _pairs;
    public HashSet(int capacity){
        _capacity = capacity;
        _pairs = (ValueOccurences[]) Array.newInstance(ValueOccurences.class, capacity);
    }
    public HashSet(){
        this(1);
    }
    private void resize(){
        if(_capacity == _length){
            var prevSize = _capacity;
            _capacity *= 10;
            _length=0;
            var copy = (ValueOccurences[]) Array.newInstance(ValueOccurences.class, _capacity);
            for (int i = 0; i < prevSize; i++) {
                var index = hashCode((TValue) _pairs[i]._value);
                while(copy[index]!=null)
                    index = (index+1) % _capacity;
                copy[index] = _pairs[i];
                _length++;
            }
            _pairs = copy;
        }
    }
    private int _length;
    public int length(){
        return _length;
    }
    public void add(TValue value){
        var index = hashCode(value);
        while(_pairs[index]!=null && _pairs[index]._value.compareTo(value)!=0)
            index = (index+1) % _capacity;
        if(_pairs[index]==null){
            _pairs[index] = new ValueOccurences(value, 1);
            _length++;
        }
        else{
            _pairs[index]._occur++;
        }
        resize();
    }
    private int hashCode(TValue key){
        return (key.hashCode() & 0x7fffffff) % _capacity;
    }
}