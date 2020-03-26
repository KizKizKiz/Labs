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
        for(int i=1;i<=ITERATIONS;i++)
            stats.add(GetAlgorithmStat(length, initializer, sort, millsPeriod));
        System.out.println(String.format("STAT INFO -> %s",sort.GetSortName()));
        var cpuUseAvg=0.0;
        var memAvgUseMB=0.0;
        var execAvgTime = 0.0;
        for (var stat:stats) {
            cpuUseAvg += stat.GetCPUUsagePercent();
            memAvgUseMB += stat.GetUsagedMemoryKB();
            execAvgTime += stat.GetExecedTime();
        }
        cpuUseAvg /= ITERATIONS;
        memAvgUseMB /= ITERATIONS;
        execAvgTime /= ITERATIONS;
        System.out.printf("ARRAY LENGTH %d\n", stats.get(0).GetSourceLenght());
        System.out.printf("AVG EXEC TIME:%.2f\nAVG CPU USAGE(%%):%.2f\nAVG MEM USAGE (KB):%.2f\n",
                execAvgTime, cpuUseAvg, memAvgUseMB);
    }
    private static SortStatInfo GetAlgorithmStat(int length, IArrayInitializer initializer,
                                                 AbstractSort sort, int millsPeriod){
        if(initializer==null) throw new NullPointerException("Initializer null");
        if(sort==null) throw new NullPointerException("Sort null");

        var statInfo = new SortStatInfo();
        var timer = new Timer();
        timer.scheduleAtFixedRate(new TimerTask() {
            OperatingSystemMXBean operatingSystemMXBean = (OperatingSystemMXBean)
                    ManagementFactory.getOperatingSystemMXBean();
            @Override
            public void run() {
                var cpu = operatingSystemMXBean.getProcessCpuLoad()*100;
                var ramKb = (Runtime.getRuntime().totalMemory()-Runtime.getRuntime().freeMemory())/Math.pow(10,3);
                statInfo.UpdateMemAndCPU(ramKb, cpu);
            }
        }, 0, millsPeriod);
        var array = initializer.GetInitializedArray(length);
        var sortThread = new Thread(() -> sort.Sort(array));
        var startTime = Instant.now();
        System.gc();
        sortThread.start();
        while(sortThread.isAlive());
        statInfo.UpdateExecTime(Duration.between(startTime,Instant.now()).toMillis());
        timer.cancel();
        statInfo.SetSourceLenght(array.length);
        return statInfo;
    }
    public static void main(String[] args) throws InterruptedException {
        for(int len=10;len<=Math.pow(10,5);len*=10) {
            int finalLen = len;
            ShowAvgInfoAboutSort(finalLen,(i1) -> {
                int MAX= finalLen;
                var src = new Integer[MAX];
                for(int i = 0; i <MAX; i++) src[i] = m_rand.nextInt(MAX);
                return src;
            }, new MergeSort(), 10);
        }
        for(int len=10;len<=Math.pow(10,5);len*=10) {
            int finalLen = len;
            ShowAvgInfoAboutSort(finalLen,(i1) -> {
                int MAX= finalLen;
                var src = new Integer[MAX];
                for(int i = 0; i <MAX; i++) src[i] = m_rand.nextInt(MAX);
                return src;
            }, new InsertionSort(), 10);
        }
        for(int len=10;len<=Math.pow(10,5);len*=10) {
            int finalLen = len;
            ShowAvgInfoAboutSort(finalLen,(i1) -> {
                int MAX= finalLen;
                var src = new Integer[MAX];
                for(int i = 0; i <MAX; i++) src[i] = m_rand.nextInt(MAX);
                return src;
            }, new SelectionSort(), 10);
        }
        for(int len=10;len<=Math.pow(10,5);len*=10) {
            int finalLen = len;
            ShowAvgInfoAboutSort(finalLen,(i1) -> {
                int MAX= finalLen;
                var src = new Integer[MAX];
                for(int i = 0; i <MAX; i++) src[i] = m_rand.nextInt(MAX);
                return src;
            }, new ShellSort(), 10);
        }
    }
}
