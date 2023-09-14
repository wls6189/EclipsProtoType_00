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
        //1.ī�޶� ��鸲 ���� ========================================================================================
        //initialPosition = new Vector3(0f, 0f, -10f); //���� ī�޶� POS���� 0,0,-10�� �����Ǿ�����
        //=============================================================================================================
        swordplayerTransform = GameObject.Find("Player").GetComponent<Transform>();
        height = Camera.main.orthographicSize; //Camera.main.orthographicSize;: ī�޶� ������ ���ߴ� �������� �߾�~y��
        width = height * Screen.width / Screen.height; //width: ī�޶� ������ ���ߴ� ������ ���� ���� 
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
           // Debug.Log("ī�޶� ���󰡱�");
        }
    }
    void LimitCameraArea()
    {
        if (swordplayerTransform)
        {
            transform.position = Vector3.Lerp(transform.position, swordplayerTransform.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);           
        }

        //Lerp(Vector3 a,Vector3 b,float t); //a��ġ�ӿ��� b��ġ�� t������ŭ ��ȯ�Ѵ�.ex)t�� 0�̶�� a�� ��ȯ , 1�̶�� b��ȯ 
        //a+(a-b)*t , t�� 0.5��� a���� ����Ͽ� b���� 0.5���� ��ȯ�ϰ� t�� 0.7�̶�� a���� ����Ͽ� b���� 0.7������ ��ȯ�Ѵ�.
        // => �� , t�� ���� ���� õõ�� ,Ŭ���� ������ b�� �����Ѵ�. 

        //ī�޶� ������Ʈ�� Size�� ���� : ī�޶� �߾ӿ��� y�� �� ���������� �Ÿ��� �ǹ���.size�� 5�� ��� transform.position.y�� ���� 
        //-5~5 ���̿� �����ϴ� ������Ʈ�� ����ȭ�鿡�� ���� �� �� ����

        float lx = mapSize.x - width; //mapSize.x: ī�޶� ���� �� �ִ� ���� ���α���, width: ī�޶� ������ ���ߴ� ������ ���� ����
                                      //ī�޶� ������ �̵��� �� �ִ� ������ ���� ���������� width��ŭ �������� ������ �Ÿ��� ���̴�. �װͺ��� �ָ� �̵��ϰ� �Ǹ� ī�޶� ���� �ڽ��� �Ѿ �� �ٱ��� ���߰� �ȴ�.
                                      //���� ī�޶� ���η� �̵��� �� �ִ� ������ �߾ӿ������� mapSize.x - width��ŭ ������ �������� ������ �� �ִ�

        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
        //Mathf.Lerp(float value, float min, float max); //value��� ������ mim ~ max �� ����� ���ϴ� ������ ������ִ� ��
        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);


    }

    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.1f;

    private Vector3 initialPosition;

    // ȣ���ϸ� ī�޶� ���� �ϴ� �Լ�
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
        while (elapsed < shakeDuration) //shakeDuration: ���� ���� �����ϰ� ���� �ϴ� ����
        {

            // �ʱ� ��ġ���� ������ ��ġ�� ī�޶� ����
            randomPoint = initialPosition + Random.insideUnitSphere * shakeAmount;
            randomPoint.y = 0.0f;
            transform.position = randomPoint;

            elapsed += Time.deltaTime;
            yield return null;
            
        }

        // ���Ⱑ ���� �Ŀ� �ʱ� ��ġ�� ī�޶� ��ġ�� ����
        transform.position = initialPosition;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
