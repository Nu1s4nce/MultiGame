public class HPBar
{
    private int _currentHealth;
    private int _maxHealth;
    
    public delegate void HpBarChanged();
    public event HpBarChanged HPChangeNotify;

    public void DealDamage(int damage)
    {
        _currentHealth -= damage;
        HPChangeNotify?.Invoke();
    }

    public void SetCurrentHealth(int health)
    {
        _currentHealth = health;
        HPChangeNotify?.Invoke();
    }
    public void SetMaxHealth(int health)
    {
        _maxHealth = health;
        HPChangeNotify?.Invoke();
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
    public int GetMaxHealth()
    {
        return _maxHealth;
    }
    
}
