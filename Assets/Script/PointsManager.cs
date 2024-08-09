using UnityEngine;
using Zenject;

public class PointsManager 
{
    private EmeraldsHandler _emeraldsHandler;
    private HPBar _hpBar;
    
    //Все бафы и валюта
    private int _clickStrength = 1;
    private int _chance = 0;
    private int _emeralds;
    private int _emeraldsMultiplier;
    private int _criticalMultiplier = 4;
    
    [Inject]
    private void Init(EmeraldsHandler emeraldsHandler, HPBar hpBar)
    {
        _emeraldsHandler = emeraldsHandler;
        _hpBar = hpBar;
    }

    public void AddClickStrength(int amount)
    {
        _clickStrength += amount;
    }

    public void HandleClick()
    {
        _emeraldsHandler.AddEmeralds(_emeraldsMultiplier);
        int randomValue = Random.Range(0, 100);
        if (randomValue < _chance)
        {
            _hpBar.DealDamage(_clickStrength * _criticalMultiplier);
        }
        else
        {
            _hpBar.DealDamage(_clickStrength);
        }
    }

    public void AddEmeraldsMultiplier(int amount)
    {
        _emeraldsMultiplier += amount;
    }

    public void AddChanceToCritical(int chance)
    {
        _chance += chance;
    }
}
