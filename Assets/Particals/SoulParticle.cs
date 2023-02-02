using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoulParticle : MonoBehaviour
{
    [SerializeField] private float timeToLive;
    [SerializeField] private float maxMove;
    [SerializeField] private float minMove;
    public float lerpAmount = 0.5f;
    
    private int counter;
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
        
        var plusX = Random.Range(-maxMove, maxMove);
        var plusY = Random.Range(-maxMove, maxMove);
        var plusZ = Random.Range(-maxMove, maxMove);
        
        plusX *= Random.Range(-maxMove, maxMove);
        plusY *= Random.Range(-maxMove, maxMove);
        plusZ *= Random.Range(-maxMove, maxMove);

        plusX += MathF.Sin(plusY);
        plusY += MathF.Cos(plusX);
        plusZ += MathF.Tan(plusX);
        //
        
        
        
        var newPos = transform.position;
        newPos += new Vector3(plusX, plusY, plusZ);

        var x = Vector3.Lerp(transform.position, newPos, lerpAmount);

        
        transform.position = x;
    }
}
