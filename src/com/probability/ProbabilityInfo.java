package com.probability;

import java.lang.reflect.Array;
import java.util.AbstractList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.stream.Collectors;

public class ProbabilityInfo {
    public static void OutInfo(AbstractList<ProbabilityViewer> viewers){
        viewers.sort(ProbabilityViewer::compareTo);
        viewers.stream().forEach(c-> System.out.println(c));
    }
}