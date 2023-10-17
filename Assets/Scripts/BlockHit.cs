using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    

    public int maxHits = -1;
    public GameObject items;
    public Sprite emtyBlock;

    private bool animating;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if(collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.enabled = true;
        maxHits--;

        if(maxHits == 0)
        {
            sp.sprite = emtyBlock;
        }

        if(items != null)
        {
            Instantiate(items, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());

    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPos = transform.localPosition;
        Vector3 animatedPos = restingPos + Vector3.up * 0.5f;

        yield return Move(restingPos , animatedPos);
        yield return Move(animatedPos , restingPos);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;

        }
        transform.localPosition = to;

    }

}
