package com.probability;

import java.util.LinkedList;
import java.util.List;
import java.util.Random;

public class ProbabilityClient {
    private static final Random _rand = new Random();
    public static void GenDiscreteTable(){
        var viewers = new LinkedList<ProbabilityViewer>();
        for(int i = 0;i<25;i++){
            var subjects = new LinkedList<Subject>();
            subjects.add(new Subject("Russian lng.", _rand.nextInt(101) ));
            subjects.add(new Subject("Math.", _rand.nextInt(101) ));
            subjects.add(new Subject("Lovely TOI.", _rand.nextInt(101) ));
            viewers.add(new ProbabilityViewer(subjects,i+1));
        }
        ProbabilityInfo.OutInfo(viewers);
    }
    public static void main(String[] args){
        GenDiscreteTable();
    }
}