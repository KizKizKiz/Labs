package com.probability;
import java.util.AbstractList;

class Subject{
    public Subject(String subjName, double rate){
        _name = subjName;
        _rate = rate;
    }
    private String _name;
    public String GetName(){
        return _name;
    }
    private double _rate;
    public double GetRate(){
        return _rate;
    }
}
public class ProbabilityViewer implements Comparable<ProbabilityViewer>{
    private AbstractList<Subject> _subjects;
    private int _id;
    public int GetId(){
        return _id;
    }
    public ProbabilityViewer(AbstractList<Subject> subjects, int id){
        _subjects = subjects;
        _id = id;
    }
    public double GetAvgRate(){
        var avgRate = _subjects.stream().mapToDouble(Subject::GetRate).sum();
        return avgRate /= _subjects.size();
    }
    public double GetExpectedValue(){
        var expectedValue = _subjects.stream().mapToDouble(Subject::GetRate).sum();
        return expectedValue * (1.0/_subjects.size());
    }
    public double GetDispersion(){
        return _subjects.stream().mapToDouble(c->c.GetRate()*c.GetRate()).sum()*(1.0/_subjects.size())-Math.pow(GetExpectedValue(),2);
    }
    @Override
    public String toString() {
        var str = new StringBuilder();
        str.append("**************\n");
        _subjects.stream().forEach(c->str.append(String.format("\tSubj: %s\tRate: %.2f\t", c.GetName(),c.GetRate())));
        str.append(String.format("\nID:%d\tAvg rate: %.2f\tM(X): %.2f\tD(X): %.2f",GetId(),GetAvgRate(),GetExpectedValue(),GetDispersion()));
        return str.toString();
    }
    @Override
    public int compareTo(ProbabilityViewer o) {
        if(GetAvgRate()>o.GetAvgRate()) return 1;
        if(GetAvgRate()<o.GetAvgRate()) return -1;
        return 0;
    }
}
