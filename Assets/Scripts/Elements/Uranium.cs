using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Uranium : MonoBehaviour
{
       public float speed;
    public void StartCoin()
    {
        transform.DOScale(1.2f, .2f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * speed;
    }
}
