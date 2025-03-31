using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCarController : MonoBehaviour
{
    // การตั้งค่าเบื้องต้น
    [Header("Settings")]
    [SerializeField] private float motorForce = 50000f; // แรงเครื่องยนต์ (ยิ่งมากรถยิ่งเร็ว)
    [SerializeField] private float maxSteerAngle = 50f; // มุมเลี้ยวสูงสุด (หน่วยเป็นองศา)
    [SerializeField] private Vector3 centerOfMass = new Vector3(0, -0.5f, 0); // จุดศูนย์กลางมวล (ป้องกันรถพลิก)

    // อ้างอิง Wheel Collider
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider; // ล้อหน้าซ้าย
    [SerializeField] private WheelCollider frontRightWheelCollider; // ล้อหน้าขวา
    [SerializeField] private WheelCollider rearLeftWheelCollider; // ล้อหลังซ้าย
    [SerializeField] private WheelCollider rearRightWheelCollider; // ล้อหลังขวา

    // อ้างอิง Transform ของโมเดลล้อ
    [Header("Transforms")]
    [SerializeField] private Transform frontLeftWheelTransform; // โมเดลล้อหน้าซ้าย
    [SerializeField] private Transform frontRightWheelTransform; // โมเดลล้อหน้าขวา
    [SerializeField] private Transform rearLeftWheelTransform; // โมเดลล้อหลังซ้าย
    [SerializeField] private Transform rearRightWheelTransform; // โมเดลล้อหลังขวา

    private Rigidbody rb;
    private float horizontalInput; // ค่าป้อนเข้าแนวแกน X (การเลี้ยว)
    private float verticalInput; // ค่าป้อนเข้าแนวแกน Y (การเร่ง/เบรก)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass; // ตั้งค่าจุดศูนย์กลางมวล
    }

    void Update()
    {
        GetInput(); // รับค่าจากผู้เล่น
        UpdateWheelPoses(); // อัพเดทตำแหน่งล้อ
    }

    void FixedUpdate()
    {
        HandleMotor(); // ควบคุมเครื่องยนต์
        HandleSteering(); // ควบคุมพวงมาลัย
        HandleBraking(); // ควบคุมเบรก
    }

    // รับค่าป้อนจากคีย์บอร์ด
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // A/D หรือ ลูกศรซ้าย/ขวา
        verticalInput = Input.GetAxis("Vertical"); // W/S หรือ ลูกศรขึ้น/ลง
    }

    // ระบบขับเคลื่อน
    private void HandleMotor()
    {
        // ใส่แรงให้ล้อหลัง (ระบบขับเคลื่อนล้อหลัง)
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    // ระบบพวงมาลัย
    private void HandleSteering()
    {
        float steerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle; // ตั้งมุมล้อหน้า
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    //เบรก
    private void HandleBraking()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // เพิ่มแรงเบรก
            rearLeftWheelCollider.brakeTorque = motorForce; // ใช้วิธีการเบรก
            rearRightWheelCollider.brakeTorque = motorForce;
        }
        else
        {
            // ออกจากแรงเบรกเมื่อไม่กด Space
            rearLeftWheelCollider.brakeTorque = 0;
            rearRightWheelCollider.brakeTorque = 0;
        }
    }

    // อัพเดทการหมุนล้อ
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPose(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPose(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPose(rearRightWheelCollider, rearRightWheelTransform);
    }

    // ซิงค์ตำแหน่ง Wheel Collider กับโมเดล 3D
    private void UpdateWheelPose(WheelCollider collider, Transform wheelTransform)
    {
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheelTransform.position = position; // อัพเดทตำแหน่ง
        wheelTransform.rotation = rotation; // อัพเดทการหมุน
    }
}