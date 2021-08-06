using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target = null;
    public float dist = 10;
    public float height = 5;
    public float smoothRotate = 5;

    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        //부드러운 회전을 위함 Mathf.LerpAngle
        float currYAngle = Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, smoothRotate * Time.deltaTime);
        //오일러 타입을 쿼터니언으로 바꾸기
        Quaternion rot = Quaternion.Euler(0, currYAngle, 0);
        //카메라 위치를 타겟 회전각도만큼 회전 후 dist만큼 띄우고, 높이 올리기
        tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        //타겟을 바라보기
        tr.LookAt(target);
    }
}
