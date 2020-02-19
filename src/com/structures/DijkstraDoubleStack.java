package com.structures;

import java.util.Stack;
import java.util.StringTokenizer;

public class DijkstraDoubleStack {
    String[] ops = {"+","-","/","*","sqrt"};
    public static Double eval(String expr)
    {
        var ops = new Stack<String>();
        var vals = new Stack<Double>();
        StringTokenizer str= new StringTokenizer(expr);
        while(str.hasMoreElements())
        {
            var current = str.nextToken();
            if(current.equals("("));
            else if(current.equals("+")
                    || current.equals("-")
                    || current.equals("*")
                    || current.equals("/")
                    || current.equals("sqrt")
            ) ops.push(current);
            else if(current.equals(")"))
            {
                var op = ops.pop();
                var v = vals.pop();
                if(op.equals("+")) v = vals.pop()+v;
                else if (op.equals("-")) v = vals.pop()-v;
                else if (op.equals("*")) v = vals.pop()*v;
                else if (op.equals("/")) v = vals.pop()/v;
                else if (op.equals("sqrt")) v = Math.sqrt(v);
                vals.push(v);
            }
            else vals.push(Double.parseDouble(current));
        }
        return vals.pop();
    }
}
