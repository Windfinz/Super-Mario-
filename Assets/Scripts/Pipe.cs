using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDir = Vector3.down;
    public Vector3 exitDir = Vector3.zero;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(enterKeyCode))
            {
                StartCoroutine(Enter(other.transform));
            }
        }        
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;

        Vector3 enteredPos = transform.position + enterDir;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return Move(player, enteredPos, enteredScale);
        yield return new WaitForSeconds(1f);

        bool underground = connection.position.y < 0f;
        Camera.main.GetComponent<SideScrolling>().SetUnderGround(underground);


        if(exitDir != Vector3.zero)
        {
            player.position = connection.position - exitDir;
            yield return Move(player, connection.position + exitDir, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlayerMovement>().enabled = true; 

    }

    private IEnumerator Move(Transform player, Vector3 endPos, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPos = player.position;
        Vector3 startScale = player.localScale;

        while(elapsed < duration)
        {
            float t = elapsed / duration;
            player.position = Vector3.Lerp(startPos, endPos, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;

            yield return null;

        }

        player.position = endPos;
        player.localScale = endScale;

    }



}
