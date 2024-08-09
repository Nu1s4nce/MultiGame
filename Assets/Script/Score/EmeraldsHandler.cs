public class EmeraldsHandler
{
    public delegate void RemoteScoreNotify();
    public event RemoteScoreNotify remoteNotify;
    
    private int _emeralds;
    
    private int _multiplier;
    
    public void AddEmeralds(int emeraldsNum)
    {
        _emeralds += emeraldsNum;
        remoteNotify?.Invoke();
    }
    public int GetEmeralds()
    {
        return _emeralds;
    }
    public void SetEmeralds(int emeraldsNum)
    {
        _emeralds = emeraldsNum;
    }
    
}
