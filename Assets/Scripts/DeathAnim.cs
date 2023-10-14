using System.Collections;
using UnityEngine;

public class DeathAnim : MonoBehaviour
{
    public SpriteRenderer sp;
    public Sprite deadSprite;

    private void Reset()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        sp.enabled = true;
        sp.sortingOrder = 10;

        if(deadSprite != null )
        {
            sp.sprite = deadSprite;

        }
    }
    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach(Collider2D collider in colliders) 
        { 
            collider.enabled = false; 
        }
        GetComponent<Rigidbody2D>().isKinematic = true;

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if(playerMovement != null )
        {
            playerMovement.enabled = false;
        }
        if(entityMovement != null)
        {
            entityMovement.enabled = false;
        }

    }
    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;
        while(elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    
}
