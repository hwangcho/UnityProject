using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Player : MonoBehaviourPunCallbacks
{
    private float h, v, r;
    private Transform tr;
    private Animator anim;
    public float speed = 10.0f;
    public float rotSpeed = 60.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();

        if (photonView.IsMine)
            Camera.main.GetComponent<FollowCam>().target = tr;
    }

    void Update()
    {
        //네트워크는 자기뿐만아니라 다른사람까지 건들일수있다.
        if (photonView.IsMine)
        { //자기자신일때
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
            Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
            tr.Translate(moveDir * Time.deltaTime * speed);

            r = Input.GetAxis("Mouse X");
            tr.Rotate(Vector3.up * Time.deltaTime * r * rotSpeed);

            if (Input.GetKeyDown(KeyCode.Space))
                anim.SetTrigger("Attack");
        }
    }
}
