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



    public float speed = 10.0f;
    public float radius = 5.0f;
    private float angle;
    private Vector3 direction;

    [SerializeField] private float minusScaleValue = 0.001f;
    private Vector3 oldScale;

    private void Start()
    {
        oldScale = gameObject.transform.localScale;
    }

    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
        
        var plusX = Random.Range(minMove, maxMove);
        var plusY = Random.Range(minMove, maxMove);
        var plusZ = Random.Range(minMove, maxMove);
        //
        // plusX *= Random.Range(-maxMove, maxMove);
        // plusY *= Random.Range(-maxMove, maxMove);
        // plusZ *= Random.Range(-maxMove, maxMove);
        //
        // plusX += MathF.Sin(plusY);
        // plusY += MathF.Cos(plusX);
        // plusZ += MathF.Tan(plusX);
        // //
        //
        //
        //
        var direction = new Vector3(plusX, plusY, plusZ);

        //
        // var x = Vector3.Lerp(transform.position, newPos, lerpAmount);
        //
        //
        transform.position += direction * speed * Time.deltaTime;
        var newScale = oldScale - new Vector3(minusScaleValue, minusScaleValue, minusScaleValue);
        if (newScale.x <= 0)
        {
            Destroy(gameObject);
        }
        oldScale = newScale;
        transform.localScale = newScale;
        
    }
}
