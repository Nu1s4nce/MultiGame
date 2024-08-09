using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

public class StoreUI : MonoBehaviour
{
    private Animator _storeAnim;
    private bool _isOpened;

    private PointsManager _pointsManager;

    [Inject]
    private void Init(PointsManager pointsManager)
    {
        _pointsManager = pointsManager;
    }
    void Awake()
    {
        _isOpened = false;
        _storeAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void OnOpenButtonClick()
    {
        if (!_isOpened)
        {
            _storeAnim.Play("OpenAnim");
            _isOpened = true;
        }
        else
        {
            _storeAnim.Play("CloseAnim");
            _isOpened = false;
        }
    }

    public void OnFirstItemClick()
    {
        _pointsManager.AddClickStrength(1);
    }
    public void OnSecondItemClick()
    {
        _pointsManager.AddEmeraldsMultiplier(1);
    }
    public void OnThirdItemClick()
    {
        _pointsManager.AddChanceToCritical(1);
    }
    public void OnFourthItemClick()
    {
        //
    }
}
