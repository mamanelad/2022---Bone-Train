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
    // Start is called before the first frame update
    void Start()
    {
        ToPopParticletimer = ToPopParticletime;
    }

    // Update is called once per frame
    void Update()
    {
        ToPopParticletimer -= Time.deltaTime;
        if (ToPopParticletimer <= 0)
        {
            ToPopParticletimer = ToPopParticletime;
            var particle = Instantiate(particlePrefab, _canvas.transform);
            particle.GetComponent<SoulParticle>().lerpAmount = lerpAmount;
        }
        
        
    }
}
