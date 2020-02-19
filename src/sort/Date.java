package sort;

import java.util.Random;

public class Date implements Comparable<Date> {
    private int day;
    private int month;
    private int year;
    public Date(int day, int month, int year){
        int rules[]={0,31,28,31,30,31,30,31,31,30,31,30,31};
        if(year%4==0) rules[2]++;
        if(month>12 || month<1 || day<1 || day>rules[month])
            throw new IllegalArgumentException("Invalid!");
        this.day=day;
        this.month=month;
        this.year=year;
    }
    private Random m_rand = new Random();
    public Date(){
        year = m_rand.nextInt(2970-1970+1)+1970;
        int rules[]={0,31,28,31,30,31,30,31,31,30,31,30,31};
        if(year%4==0) rules[2]++;
        month = m_rand.nextInt(12-1+1)+1;
        day = m_rand.nextInt(rules[month])+1;
    }
    @Override
    public String toString(){
        String names[]={
                "нулября",
                "января",
                "февраля",
                "марта",
                "апреля",
                "мая",
                "июня",
                "июля",
                "августа",
                "сентября",
                "октября",
                "ноября",
                "декабря"
        };
        return ""+day+" " +names[month] +" "+year+"г.";
    }
    public Date(String rusDate){
    }
    public int getDay(){
        return day;
    }
    public int getYear(){
        return year;
    }
    public int getMonth(){
        return month;
    }
    @Override
    public int compareTo(Date o) {
        if(this.year>o.year) return 1;
        if(this.year<o.year) return -1;
        if(this.month>o.month) return 1;
        if(this.month<o.month) return -1;
        if(this.day>o.day) return 1;
        if(this.day<o.day) return -1;
        return 0;
    }
}
