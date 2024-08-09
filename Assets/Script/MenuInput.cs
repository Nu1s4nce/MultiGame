using TMPro;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    private string _ipText;
    private string _name;
    [SerializeField] private TMP_InputField ipField;
    [SerializeField] private TMP_InputField nameField;

    void Start()
    {
        SetIpFromInput();
    }

    public void SetIpFromInput()
    {
        _ipText = ipField.GetComponent<TMP_InputField>().text;
    }

    public void SetNameFromInput()
    {
        _name = nameField.GetComponent<TMP_InputField>().text;
    }

    public string GetIpFromForm()
    {
        return _ipText;
    }

    public string GetNameFromForm()
    {
        return _name;
    }
}