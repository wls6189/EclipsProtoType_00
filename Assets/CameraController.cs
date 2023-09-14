using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform swordplayerTransform;
    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;


    private void Start()
    {
        //1.카메라 흔들림 관련 ========================================================================================
        //initialPosition = new Vector3(0f, 0f, -10f); //현재 카메라 POS값이 0,0,-10로 설정되어있음
        //=============================================================================================================
        swordplayerTransform = GameObject.Find("Player").GetComponent<Transform>();
        height = Camera.main.orthographicSize; //Camera.main.orthographicSize;: 카메라가 실제로 비추는 영역에서 중앙~y축
        width = height * Screen.width / Screen.height; //width: 카메라가 실제로 비추는 영역의 가로 길이 
        //initialPosition = transform.position;
    }

   
    private void Update() 
    {

    }
    private void FixedUpdate()
    {
        if (FindAnyObjectByType<PlayerController>().isRun == true)
        {
            LimitCameraArea();
           // Debug.Log("카메라 따라가기");
        }
    }
    void LimitCameraArea()
    {
        if (swordplayerTransform)
        {
            transform.position = Vector3.Lerp(transform.position, swordplayerTransform.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);           
        }

        //Lerp(Vector3 a,Vector3 b,float t); //a위치ㅣ에서 b위치로 t비율만큼 반환한다.ex)t가 0이라면 a를 반환 , 1이라면 b반환 
        //a+(a-b)*t , t가 0.5라면 a에서 출발하여 b까지 0.5지점 반환하고 t가 0.7이라면 a에서 출발하여 b까지 0.7지점을 반환한다.
        // => 즉 , t가 작을 수록 천천히 ,클수록 빠르게 b에 도달한다. 

        //카메라 컴포넌트의 Size의 개념 : 카메라 중앙에서 y축 끝 지점까지의 거리를 의미함.size가 5일 경우 transform.position.y의 범위 
        //-5~5 사이에 존재하는 오브젝트만 실제화면에서 관측 할 수 있음

        float lx = mapSize.x - width; //mapSize.x: 카메라가 비출 수 있는 맵의 가로길이, width: 카메라가 실제로 비추는 영역의 가로 길이
                                      //카메라가 실제로 이동할 수 있는 영역은 맵의 끝지점에서 width만큼 안쪽으로 떨어진 거리일 것이다. 그것보다 멀리 이동하게 되면 카메라가 빨간 박스를 넘어서 맵 바깥을 비추게 된다.
                                      //따라서 카메라가 가로로 이동할 수 있는 영역은 중앙에서부터 mapSize.x - width만큼 떨어진 영역으로 결정할 수 있다

        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
        //Mathf.Lerp(float value, float min, float max); //value라는 변수를 mim ~ max 를 벗어나지 못하는 범위로 만들어주는 것
        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);


    }

    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.1f;

    private Vector3 initialPosition;

    // 호출하면 카메라를 흔들게 하는 함수
    public void ShakeCamera()
    {
        
        initialPosition = transform.position;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;
        Vector3 randomPoint = Vector3.zero;
        Debug.Log(initialPosition);
        while (elapsed < shakeDuration) //shakeDuration: 몇초 동안 진동하게 할지 하는 변수
        {

            // 초기 위치에서 랜덤한 위치로 카메라를 흔들기
            randomPoint = initialPosition + Random.insideUnitSphere * shakeAmount;
            randomPoint.y = 0.0f;
            transform.position = randomPoint;

            elapsed += Time.deltaTime;
            yield return null;
            
        }

        // 흔들기가 끝난 후에 초기 위치로 카메라 위치를 복원
        transform.position = initialPosition;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
