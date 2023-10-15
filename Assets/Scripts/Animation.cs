using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public SpriteRenderer sp {  get; private set; }
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimationSprites run;
    private void OnEnable()
    {
        sp.enabled = true;
    }

    private void OnDisable()
    {
        sp.enabled = false;
        run.enabled = false;
    }

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        run.enabled = movement.running;
        if (movement.jumping)
        {
            sp.sprite = jump;
        }
        else if (movement.sliding)
        {
            sp.sprite = slide;
        }
        else if(!movement.running)
        {
            sp.sprite = idle;
        }
    }
}
