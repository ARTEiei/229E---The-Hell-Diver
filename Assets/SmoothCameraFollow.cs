using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("ตั้งค่ากล้อง")]
    [SerializeField] private Transform target; // วัตถุที่กล้องจะตาม (รถ)
    [SerializeField] private float distance = 5.0f; // ระยะห่างจากรถ
    [SerializeField] private float height = 7.0f; // ความสูงของกล้อง
    [SerializeField] private float rotationDamping = 5.0f; // ความลื่นไหลในการหมุน
    [SerializeField] private float heightDamping = 4.0f; // ความลื่นไหลในการปรับความสูง

    void LateUpdate()
    {
        if (!target)
            return;

        // คำนวณมุมและตำแหน่งที่กล้องควรอยู่
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // ค่อยๆ ปรับมุมและความสูงด้วย Lerp
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // สร้างการหมุนใหม่
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // ตั้งค่าตำแหน่งกล้อง
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance; // ถอยหลังตามระยะห่าง
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z); // ปรับความสูง

        // หมุนกล้องให้มองรถ
        transform.LookAt(target);
    }
}