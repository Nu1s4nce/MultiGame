using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HPBarUI : MonoBehaviour
{
    private HPBar _hpBar;
    
    private int _currentHealth;
    private bool _canNewEnemy;
    
    [SerializeField] private Slider _hpFillSlider;
    [SerializeField] private TMP_Text _hpText;
    
    [Inject]
    private void Init(HPBar hpBar)
    {
        _hpBar = hpBar;
    }
    void Awake()
    {
        _hpBar.HPChangeNotify += UpdateHpBar;
        SetUpNewHp(100);
    }

    private void UpdateHpBar()
    {
        _currentHealth = _hpBar.GetCurrentHealth();
        
        _hpFillSlider.value = _currentHealth;
        _hpText.text = _currentHealth.ToString();
    }

    private void SetUpNewHp(int hp)
    {
        _canNewEnemy = false;
        _hpBar.SetMaxHealth(hp);
        _hpFillSlider.maxValue = _hpBar.GetMaxHealth();
        _hpBar.SetCurrentHealth(_hpBar.GetMaxHealth());
        _canNewEnemy = true;
    }

    public void OnValueChanged()
    {
        if (_hpBar.GetCurrentHealth() <= 0 && _canNewEnemy)
        {
            SetUpNewHp(500);
        }
    }
}
