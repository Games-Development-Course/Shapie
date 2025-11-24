using UnityEngine;

public class StencilManager : MonoBehaviour
{
    [Header("Stencil Sprite")]
    public Sprite stencilSprite;
    private SpriteRenderer sr;

    [Header("Puzzle Slots")]
    [Tooltip("כל סלוט הוא Empty שפשוט מציין מיקום נכון לחתיכה")]
    public Transform[] slots;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (stencilSprite != null)
            sr.sprite = stencilSprite;
    }
}