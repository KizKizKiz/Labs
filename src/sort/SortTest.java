package sort;
import java.lang.management.ManagementFactory;
import java.time.Duration;
import java.time.Instant;
import java.util.*;
import com.sun.management.OperatingSystemMXBean;

interface IArrayInitializer{
    Comparable[] GetInitializedArray(int length);
}
public class SortTest {
    private static Random m_rand = new Random();
    public static void ShowAvgInfoAboutSort(int length, IArrayInitializer initializer, AbstractSort sort, int millsPeriod)
            throws InterruptedException{
        int ITERATIONS=10;
        var stats = new LinkedList<SortStatInfo>();
        for(int i=1;i<=ITERATIONS;i++) {
            System.out.println(String.format("PROCESSING %d/%d...",i,ITERATIONS));
            stats.add(GetAlgorithmStat(length, initializer, sort, millsPeriod));
            System.out.println("DONE");
            //FORCE TO COLLECT PREV TEST OBJECTS
            System.gc();
        }
        System.out.println(String.format("STAT INFO -> %s",sort.GetSortName()));
        var cpuUseAvg=0.0;
        var memAvgUseMB=0.0;
        var execAvgTime = 0.0;
        for (var stat:stats) {
            cpuUseAvg += stat.GetCPUUsagePercent();
            memAvgUseMB += stat.GetUsagedMemoryMB();
            execAvgTime += stat.GetExecedTime();
        }
        cpuUseAvg /= ITERATIONS;
        memAvgUseMB /= ITERATIONS;
        execAvgTime /= ITERATIONS;
        System.out.printf("ARRAY LENGTH %d\n", stats.get(0).GetSourceLenght());
        System.out.printf("AVG EXEC TIME:%.2f\nAVG CPU USAGE(%%):%.2f\nAVG MEM USAGE (MB):%.2f\n",
                execAvgTime,
                cpuUseAvg,
                memAvgUseMB/1000
        );
    }
    private static SortStatInfo GetAlgorithmStat(int length, IArrayInitializer initializer, AbstractSort sort, int millsPeriod){
        if(initializer==null) throw new NullPointerException("Initializer null");
        if(sort==null) throw new NullPointerException("Sort null");

        var statInfo = new SortStatInfo();
        var timer = new Timer();
        timer.scheduleAtFixedRate(new TimerTask() {
            OperatingSystemMXBean operatingSystemMXBean = (OperatingSystemMXBean)ManagementFactory.getOperatingSystemMXBean();
            @Override
            public void run() {
                var cpu = operatingSystemMXBean.getProcessCpuLoad()*100;
                var ramKb = (Runtime.getRuntime().totalMemory()-Runtime.getRuntime().freeMemory())/Math.pow(10,3);
                statInfo.UpdateMemAndCPU(ramKb, cpu);
            }
        }, 0, millsPeriod);
        var array = initializer.GetInitializedArray(length);
        var sortThread = new Thread(new Runnable() {
            @Override
            public void run() {
                sort.Sort(array);
            }
        });
        var startTime = Instant.now();
        sortThread.start();
        while(sortThread.isAlive());
        statInfo.UpdateExecTime(Duration.between(startTime,Instant.now()).toMillis());
        timer.cancel();
        statInfo.SetSourceLenght(array.length);
        return statInfo;
    }
    public static void main(String[] args) throws InterruptedException {
        for(int len=10;len<=Math.pow(10,6);len*=10) {
            int finalLen = len;
            ShowAvgInfoAboutSort(finalLen,(i1) -> {
                int MAX= finalLen;
                var src = new Integer[MAX];
                for(int i = 0; i <MAX; i++){
                    src[i] = m_rand.nextInt(MAX);
                }
                return src;
            }, new MergeSort(), len/10);
        }
        for(int len=100;len<=Math.pow(10,5);len*=10) {
            int finalLen = len;
            ShowAvgInfoAboutSort(finalLen,(i1) -> {
                int MAX= finalLen;
                var src = new Integer[MAX];
                for(int i = 0; i <MAX; i++){
                    src[i] = m_rand.nextInt(MAX);
                }
                return src;
            }, new InsertionSort(), len/10);
        }
        for(int len=100;len<=Math.pow(10,5);len*=10) {
            int finalLen = len;
            ShowAvgInfoAboutSort(finalLen,(i1) -> {
                int MAX= finalLen;
                var src = new Integer[MAX];
                for(int i = 0; i <MAX; i++){
                    src[i] = m_rand.nextInt(MAX);
                }
                return src;
            }, new SelectionSort(), len/10);
        }
        for(int len=100;len<=Math.pow(10,6);len*=10) {
            int finalLen = len;
            ShowAvgInfoAboutSort(finalLen,(i1) -> {
                int MAX= finalLen;
                var src = new Integer[MAX];
                for(int i = 0; i <MAX; i++){
                    src[i] = m_rand.nextInt(MAX);
                }
                return src;
            }, new ShellSort(), len/10);
        }
    }
}
abstract class AlgorithmStatInfo {
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
        return _execTime;
    }
    public double GetUsagedMemoryMB(){
        return _updateCounter!=0?_usageMemoryMB/_updateCounter:0;
    }
    public double GetCPUUsagePercent(){ return _updateCounter!=0?_cpuUsagePercent/_updateCounter:0;}
}
class SortStatInfo extends AlgorithmStatInfo{
    private int _srcLength;
    public SortStatInfo(int length){
        _srcLength = length;
    }
    public SortStatInfo() {
        this(0);
    }
    public void SetSourceLenght(int length) { _srcLength = length; }
    public int GetSourceLenght(){ return _srcLength; }
}
