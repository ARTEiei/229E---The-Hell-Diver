using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("��駤�ҡ��ͧ")]
    [SerializeField] private Transform target; // �ѵ�ط����ͧ�е�� (ö)
    [SerializeField] private float distance = 5.0f; // ������ҧ�ҡö
    [SerializeField] private float height = 7.0f; // �����٧�ͧ���ͧ
    [SerializeField] private float rotationDamping = 5.0f; // ����������㹡����ع
    [SerializeField] private float heightDamping = 4.0f; // ����������㹡�û�Ѻ�����٧

    void LateUpdate()
    {
        if (!target)
            return;

        // �ӹǳ�����е��˹觷����ͧ�������
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // ����� ��Ѻ�����Ф����٧���� Lerp
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // ���ҧ�����ع����
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // ��駤�ҵ��˹觡��ͧ
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance; // �����ѧ���������ҧ
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z); // ��Ѻ�����٧

        // ��ع���ͧ����ͧö
        transform.LookAt(target);
    }
}