using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Game game;
    public float power;
    public Rigidbody rb;
    Vector3 dir = Vector3.zero;
    bool isMove;

    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            rb.AddForce(Vector3.left * power, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            rb.AddForce(Vector3.right * power, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            rb.AddForce(Vector3.forward * power, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            rb.AddForce(Vector3.back * power, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        if (!isMove) return;
        //rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Break"))
        {
            Destroy(collision.gameObject);
            game.point += 100;
            game.TextSetting();
        }
        if(collision.transform.CompareTag("Destroy"))
        {
            Destroy(this.gameObject);
        }
    }

}
