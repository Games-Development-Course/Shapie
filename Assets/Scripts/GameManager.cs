// מנהל את המידע הגלובלי של המשחק שנשמר בין סצנות
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string player1Name = "שחקן 1";
    public string player2Name = "שחקן 2";

    // תיקון: הוספת  שלא הופיע קודם
    public List<string> solvedPieces = new List<string>();

    void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddSolvedPiece(string pieceName)
    {
        if (!solvedPieces.Contains(pieceName))
        {
            solvedPieces.Add(pieceName);
        }
    }

    public bool IsPieceSolved(string pieceName)
    {
        return solvedPieces.Contains(pieceName);
    }

    public void ResetGame()
    {
        solvedPieces.Clear();
    }
}