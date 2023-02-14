using UnityEngine;

public class Record : MonoBehaviour
{
    public int HighestRecord { get; private set; }

    public void SetNewRecord(int level)
    {
        if (level > HighestRecord)
            HighestRecord = level;
    }
}
