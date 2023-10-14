using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animation smallRenderer;
    public Animation bigRenderer;

    private DeathAnim deathAnim;
    
    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool death => deathAnim.enabled;


    private void Awake()
    {
        deathAnim = GetComponent<DeathAnim>();
    }
    public void Hit()
    {
        if (big)
        {
            Shrink();
        }else
        {
            Death();
        }
    }

    private void Shrink()
    {

    }
    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnim.enabled = true;

        GameManager.Instance.ResetStage(3f);
    }

}
