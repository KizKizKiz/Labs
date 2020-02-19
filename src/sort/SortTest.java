package sort;
import java.lang.management.ManagementFactory;
import java.time.Duration;
import java.time.Instant;
import java.util.*;
import com.sun.management.OperatingSystemMXBean;

interface IArrayInitializer{
    Comparable[] GetInitializedArray();
}
public class SortTest {
    private static Random m_rand = new Random();
    public static void ShowAvgInfoAboutSort(IArrayInitializer initializer, AbstractSort sort, int millsPeriod)
            throws InterruptedException{
        int ITERATIONS=5;
        var stats = new LinkedList<AlgorithmStatInfo>();
        for(int i=1;i<=ITERATIONS;i++) {
            System.out.println(String.format("PROCESSING %d/%d...",i,ITERATIONS));
            stats.add(GetAlgorithmStat(initializer, sort, millsPeriod));
            System.out.println("DONE");
        }
        sort.GetSortName();
    }
    private static AlgorithmStatInfo GetAlgorithmStat(IArrayInitializer initializer, AbstractSort sort, int millsPeriod){
        if(initializer==null) throw new NullPointerException("Initializer null");
        if(sort==null) throw new NullPointerException("Sort null");

        var statInfo = new AlgorithmStatInfo();
        var timer = new Timer();
        timer.scheduleAtFixedRate(new TimerTask() {
            OperatingSystemMXBean operatingSystemMXBean = (OperatingSystemMXBean)ManagementFactory.getOperatingSystemMXBean();
            @Override
            public void run() {
                var cpu = operatingSystemMXBean.getProcessCpuLoad()*100;
                var ramKb = Math.round((Runtime.getRuntime().totalMemory()-Runtime.getRuntime().freeMemory())/Math.pow(10,6));
                statInfo.UpdateMemAndCPU(ramKb, cpu);
            }
        }, 0, millsPeriod);
        var sortThread = new Thread(new Runnable() {
            @Override
            public void run() {
                var array = initializer.GetInitializedArray();
                sort.Sort(array);
            }
        });
        var startTime = Instant.now();
        sortThread.start();
        while(sortThread.isAlive());
        statInfo.UpdateExecTime(Duration.between(startTime,Instant.now()).toMillis());
        timer.cancel();
        return statInfo;
    }
    public static void main(String[] args) throws InterruptedException {
        ShowAvgInfoAboutSort(new IArrayInitializer() {
            @Override
            public Comparable[] GetInitializedArray() {
                int MAX=1000000;
                var src = new Integer[MAX];
                for(int i = 0;i<MAX;i++){
                    src[i] = m_rand.nextInt(MAX);
                }
                return src;
            }
        }, new MergeSort(), 100);
    }
}
class AlgorithmStatInfo {
    private double _usageMemoryMB;
    private double _cpuUsagePercent;
    private double _execTime;
    private int _updateCounter = 0;
    public void UpdateMemAndCPU(double memMB, double cpuUsage){
        _usageMemoryMB += memMB;
        _cpuUsagePercent += cpuUsage;
        _updateCounter++;
    }
    public void UpdateExecTime(double time){
        _execTime = time;
    }
    public double GetExecedTime(){
        return _updateCounter!=0?_execTime/_updateCounter:0;
    }
    public double GetUsagedMemoryMB(){
        return _updateCounter!=0?_usageMemoryMB/_updateCounter:0;
    }
    public double GetCPUUsagePercent(){
        return _updateCounter!=0?_cpuUsagePercent/_updateCounter:0;
    }
}