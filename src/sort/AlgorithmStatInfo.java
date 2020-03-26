package sort;

public abstract class AlgorithmStatInfo {
    private double _usageMemoryKB;
    private double _cpuUsagePercent;
    private double _execTime;
    private int _updateCounter = 0;
    public void UpdateMemAndCPU(double memKB, double cpuUsage){
        _usageMemoryKB += memKB;
        _cpuUsagePercent += cpuUsage;
        _updateCounter++;
    }
    public void UpdateExecTime(double time){
        _execTime = time;
    }
    public double GetExecedTime(){
        return _execTime;
    }
    public double GetUsagedMemoryKB(){
        return _updateCounter!=0?_usageMemoryKB/_updateCounter:0;
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