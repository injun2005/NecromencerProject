using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCharacter : MonoBehaviour
{
    Rigidbody rigid;
    // public int MonsterCount;
    public int speed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //MonsterCount = 0;
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveH  = transform.right * x;
        Vector3 moveV = transform.up * z;

        Vector3 _velocity = (moveH + moveV).normalized * speed;

        rigid.MovePosition(transform.position + _velocity * Time.deltaTime);

    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {

    }
}
