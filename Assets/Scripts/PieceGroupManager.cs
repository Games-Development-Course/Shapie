using UnityEngine;

public class PieceGroupManager : MonoBehaviour
{
    public static PieceGroupManager Instance;

    // 0 = עדיין לא הונח שום חלק
    // 1 = קבוצת Part1+Part2
    // 2 = קבוצת Part3+Part4
    private int lastGroupPlaced = 0;

    void Awake()
    {
        Instance = this;
    }

    public bool CanPlace(int groupID)
    {
        // אם זו ההנחה הראשונה – תמיד מותר
        if (lastGroupPlaced == 0)
            return true;

        // מותר רק אם הקבוצה שונה מהקבוצה הקודמת
        return groupID != lastGroupPlaced;
    }

    public void RegisterPlacement(int groupID)
    {
        lastGroupPlaced = groupID;
    }
}