using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 inputVec;
    public Rigidbody2D rigid;
    public float speed;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed  * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);   
    }
}
