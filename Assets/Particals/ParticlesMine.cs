using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesMine : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private float ToPopParticletime = 1f;
    [SerializeField] private float ToPopParticletimer;

    [SerializeField] private Canvas _canvas;

    [SerializeField] private float lerpAmount = 0.5f;


    [SerializeField] private GameObject posToInstantiate0;
    [SerializeField] private GameObject posToInstantiate1;
    [SerializeField] private GameObject posToInstantiate2;

    [SerializeField] private float toBeAliveTime = 2f;
    [SerializeField] private float toBeAliveTimer;
    public bool isOn;

    public enum SoulsKind
    {
        Bad,
        Good
    }

    public SoulsKind myKind;

    // Start is called before the first frame update
    void Start()
    {
        toBeAliveTimer = toBeAliveTime;
        ToPopParticletimer = ToPopParticletime;

        // switch (myKind)
        // {
        //     case SoulsKind.Good:
        //         GameManager.Shared.blueParticle = gameObject.GetComponent<ParticlesMine>();
        //         break;
        //     
        //     case SoulsKind.Bad:
        //         GameManager.Shared.redParticle = gameObject.GetComponent<ParticlesMine>();
        //         break;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn) return;

        ToPopParticletimer -= Time.deltaTime;
        if (ToPopParticletimer <= 0)
        {
            ToPopParticletimer = ToPopParticletime;
            var particle0 = Instantiate(particlePrefab, posToInstantiate0.transform.position, Quaternion.identity,
                _canvas.transform);
            var particle1 = Instantiate(particlePrefab, posToInstantiate1.transform.position, Quaternion.identity,
                _canvas.transform);
            var particle2 = Instantiate(particlePrefab, posToInstantiate2.transform.position, Quaternion.identity,
                _canvas.transform);
        }

        toBeAliveTimer -= Time.deltaTime;
        if (toBeAliveTimer <= 0)
        {
            toBeAliveTimer = toBeAliveTime;
            isOn = false;
        }
    }

    public void TurnOn()
    {
        isOn = true;
        toBeAliveTimer = toBeAliveTime;
    }
}