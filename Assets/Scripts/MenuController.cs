using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(2); // טוען את סצנת המשחק
    }

    public void OpenSetup()
    {
        SceneManager.LoadScene(1); // טוען את סצנת השמות
    }
}