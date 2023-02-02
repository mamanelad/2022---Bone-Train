using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalsManager : MonoBehaviour
{
    [SerializeField] private Animator goodSoulsParticles;
    [SerializeField] private Animator badSoulsParticles;

    public void PlayGoodSoulsInAnim()
    {
        goodSoulsParticles.SetTrigger("Good Souls In");
    }
    
    public void PlayGoodSoulsOutAnim()
    {
        goodSoulsParticles.SetTrigger("Good Souls Out");
    }
    
    public void PlayBadSoulsInAnim()
    {
        badSoulsParticles.SetTrigger("Bad Souls In");
    }
    
    public void PlayBadSoulsOutAnim()
    {
        badSoulsParticles.SetTrigger("Bad Souls Out");
    }
}
