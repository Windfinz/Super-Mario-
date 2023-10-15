using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animation smallRenderer;
    public Animation bigRenderer;
    private Animation activeRenderer;

    private CapsuleCollider2D capsuleCollider;
    private DeathAnim deathAnim;
    
    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool death => deathAnim.enabled;

    public bool starPower { get; private set; }

    private void Awake()
    {
        deathAnim = GetComponent<DeathAnim>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }
    public void Hit()
    {
        if (!starPower && !death)
        {
            if (big)
            {
                Shrink();
            }else
            {
                Death();
            }

        }

    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnim.enabled = true;

        GameManager.Instance.ResetStage(3f);
    }

    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());

    }
    private void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0.4f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if(Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !bigRenderer.enabled;
            }
            yield return null;
        }
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void StarPower(float duration = 10f)
    {
        StartCoroutine(StarPowerAnimation(duration));

    }

    private IEnumerator StarPowerAnimation(float duration)
    {
        starPower = true;

        float elapsed = 0f;
            
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if(Time.frameCount % 4 == 0)
            {
                activeRenderer.sp.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
        }
        activeRenderer.sp.color = Color.white;
        starPower = false;

    }


}
