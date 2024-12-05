using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UraniumManager : MonoBehaviour
{
  
    public int collectedUraniumCount;

    public void IncreaseCoinCount(int count)
    {
        collectedUraniumCount += count;
    }
}
