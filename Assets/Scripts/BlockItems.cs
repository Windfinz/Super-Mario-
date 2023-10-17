using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItems : MonoBehaviour
{
    public AudioSource itemAppearSound;

    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        itemAppearSound.Play();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = rb.GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = rb.GetComponent<BoxCollider2D>();
        SpriteRenderer sp = rb.GetComponent<SpriteRenderer>();

        rb.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        sp.enabled = false;

        yield return new WaitForSeconds(0.25f);
        sp.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + Vector3.up;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startPos, endPos,t);
            elapsed += Time.deltaTime;
            yield return null;

        }
        transform.localPosition = endPos;

        rb.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;

    }


}
