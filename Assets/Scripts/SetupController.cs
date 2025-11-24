using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetupController : MonoBehaviour
{
    public InputField player1Input;
    public InputField player2Input;

    public void SaveNamesAndReturn()
    {
        if (GameManager.Instance != null)
        {
            if (!string.IsNullOrEmpty(player1Input.text))
                GameManager.Instance.player1Name = player1Input.text;

            if (!string.IsNullOrEmpty(player2Input.text))
                GameManager.Instance.player2Name = player2Input.text;
        }

        SceneManager.LoadScene(0); // חזרה לתפריט
    }
}