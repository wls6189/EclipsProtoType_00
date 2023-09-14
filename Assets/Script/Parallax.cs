using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cam: 시차 효과에 사용하려는 카메라에 대한 참조입니다.
//subject: 따라갈 목표 위치(예: 플레이어의 위치).
//startPosition 및 startZ: GameObject의 초기 위치와 z 좌표를 저장합니다.
//travel: 카메라의 현재 위치와 초기 위치의 차이를 계산합니다.
//distanceFromSubject: GameObject의 Z 좌표와 대상의 Z 좌표 간의 차이를 계산합니다.
//clippingPlane: 피사체와의 거리가 양수인지 음수인지에 따라 클리핑 평면을 계산합니다.
//parallaxFactor: 피사체와의 거리와 클리핑 평면을 기준으로 시차 인자를 계산합니다

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform subject; // Target position to follow

    private Vector2 startPosition;
    private float startZ;

    private Vector2 travel => (Vector2)cam.transform.position - startPosition;
    private float distanceFromSubject => transform.position.z - subject.position.z;
    private float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    private float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}



