using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    [Header("UI References")]
    public Text turnText;
    public Text feedbackText;
    public GameObject fullCarImage;
    public GameObject carOutline;

    [Header("Pieces")]
    // תיקון: הוספת 
    public List<GameObject> player1Pieces;
    public List<GameObject> player2Pieces;

    private int currentPlayer = 1;
    private bool gameActive = false;
    private int totalPiecesToSolve;
    private int currentSolvedCount = 0;

    void Start()
    {
        StartCoroutine(StartLevelSequence());
        RestoreSolvedPieces();
        CountNecessaryPieces();
    }

    void CountNecessaryPieces()
    {
        totalPiecesToSolve = 0;
        foreach (var p in player1Pieces)
        {
            if (p != null && !p.GetComponent<DraggablePiece>().isDistractor) totalPiecesToSolve++;
        }
        foreach (var p in player2Pieces)
        {
            if (p != null && !p.GetComponent<DraggablePiece>().isDistractor) totalPiecesToSolve++;
        }
    }

    IEnumerator StartLevelSequence()
    {
        gameActive = false;
        if (feedbackText) feedbackText.text = "";

        if (fullCarImage) fullCarImage.SetActive(true);
        if (carOutline) carOutline.SetActive(false);
        SetPiecesVisibility(false);

        if (GameManager.Instance != null && GameManager.Instance.solvedPieces.Count == 0)
        {
            yield return new WaitForSeconds(4f);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        if (fullCarImage) fullCarImage.SetActive(false);
        if (carOutline) carOutline.SetActive(true);
        gameActive = true;

        UpdateTurnUI();
    }

    void UpdateTurnUI()
    {
        string pName = "שחקן";
        if (GameManager.Instance != null)
            pName = (currentPlayer == 1) ? GameManager.Instance.player1Name : GameManager.Instance.player2Name;

        if (turnText) turnText.text = "תור: " + pName;

        ShowPlayerPieces(currentPlayer);
    }

    void ShowPlayerPieces(int player)
    {
        SetPiecesVisibility(false);

        List<GameObject> currentList = (player == 1) ? player1Pieces : player2Pieces;

        foreach (GameObject piece in currentList)
        {
            if (piece != null && GameManager.Instance != null && !GameManager.Instance.IsPieceSolved(piece.name))
            {
                piece.SetActive(true);
            }
        }
    }

    void SetPiecesVisibility(bool visible)
    {
        if (GameManager.Instance == null) return;

        foreach (var p in player1Pieces)
            if (p != null && !GameManager.Instance.IsPieceSolved(p.name)) p.SetActive(visible);

        foreach (var p in player2Pieces)
            if (p != null && !GameManager.Instance.IsPieceSolved(p.name)) p.SetActive(visible);
    }

    public void OnPiecePlaced(string pieceName)
    {
        if (GameManager.Instance) GameManager.Instance.AddSolvedPiece(pieceName);
        currentSolvedCount++;

        if (CheckWinCondition())
        {
            StartCoroutine(WinSequence());
        }
        else
        {
            SwitchTurn();
        }
    }

    bool CheckWinCondition()
    {
        if (GameManager.Instance == null) return false;
        int solved = GameManager.Instance.solvedPieces.Count;
        return solved >= totalPiecesToSolve;
    }

    IEnumerator WinSequence()
    {
        gameActive = false;
        if (feedbackText) feedbackText.text = "כל הכבוד! סיימתם את השלב!";
        yield return new WaitForSeconds(1f);
        if (fullCarImage) fullCarImage.SetActive(true);
        if (carOutline) carOutline.SetActive(false);
        SetPiecesVisibility(false);
    }

    public void ShowFeedback(string message, bool switchTurnAfter)
    {
        StartCoroutine(FeedbackRoutine(message, switchTurnAfter));
    }

    IEnumerator FeedbackRoutine(string message, bool switchTurnAfter)
    {
        if (feedbackText) feedbackText.text = message;
        yield return new WaitForSeconds(2f);
        if (feedbackText) feedbackText.text = "";

        if (switchTurnAfter && gameActive)
        {
            SwitchTurn();
        }
    }

    void SwitchTurn()
    {
        currentPlayer = (currentPlayer == 1) ? 2 : 1;
        UpdateTurnUI();
    }

    public bool CanInteract()
    {
        return gameActive && feedbackText != null && string.IsNullOrEmpty(feedbackText.text);
    }

    void RestoreSolvedPieces()
    {
        List<GameObject> allPieces = new List<GameObject>();
        allPieces.AddRange(player1Pieces);
        allPieces.AddRange(player2Pieces);

        foreach (var piece in allPieces)
        {
            if (piece == null) continue;

            DraggablePiece dp = piece.GetComponent<DraggablePiece>();
            if (GameManager.Instance != null && GameManager.Instance.IsPieceSolved(piece.name))
            {
                GameObject slot = GameObject.Find(dp.targetSlotName);
                if (slot != null)
                {
                    piece.transform.position = slot.transform.position;
                    // תיקון: הוספת 
                    if (piece.GetComponent<Collider2D>()) piece.GetComponent<Collider2D>().enabled = false;
                    piece.SetActive(true);
                }
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}