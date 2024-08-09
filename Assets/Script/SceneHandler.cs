using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneHandler : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }
    
}
