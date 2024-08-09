using TMPro;
using UnityEngine;
using Zenject;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreContainer;
    [SerializeField]
    private ButtonClicker buttonClicker;

    private EmeraldsHandler _emeralds;

    [Inject]
    private void Init(EmeraldsHandler emeralds)
    {
        _emeralds = emeralds;
    }
    
    private void Awake()
    {
        _emeralds.SetEmeralds(0);
        UpdateScore();
        buttonClicker.Notify += UpdateScore;
        _emeralds.remoteNotify += UpdateScore;
    }

    private void UpdateScore()
    {
        scoreContainer.text = _emeralds.GetEmeralds().ToString();
    }
    
}
