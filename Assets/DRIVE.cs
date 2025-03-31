using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCarController : MonoBehaviour
{
    // ��õ�駤�����ͧ��
    [Header("Settings")]
    [SerializeField] private float motorForce = 50000f; // �ç����ͧ¹�� (����ҡö�������)
    [SerializeField] private float maxSteerAngle = 50f; // ����������٧�ش (˹�����ͧ��)
    [SerializeField] private Vector3 centerOfMass = new Vector3(0, -0.5f, 0); // �ش�ٹ���ҧ��� (��ͧ�ѹö��ԡ)

    // ��ҧ�ԧ Wheel Collider
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider; // ���˹�ҫ���
    [SerializeField] private WheelCollider frontRightWheelCollider; // ���˹�Ң��
    [SerializeField] private WheelCollider rearLeftWheelCollider; // �����ѧ����
    [SerializeField] private WheelCollider rearRightWheelCollider; // �����ѧ���

    // ��ҧ�ԧ Transform �ͧ�������
    [Header("Transforms")]
    [SerializeField] private Transform frontLeftWheelTransform; // �������˹�ҫ���
    [SerializeField] private Transform frontRightWheelTransform; // �������˹�Ң��
    [SerializeField] private Transform rearLeftWheelTransform; // ���������ѧ����
    [SerializeField] private Transform rearRightWheelTransform; // ���������ѧ���

    private Rigidbody rb;
    private float horizontalInput; // ��һ�͹�����᡹ X (���������)
    private float verticalInput; // ��һ�͹�����᡹ Y (������/�á)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass; // ��駤�Ҩش�ٹ���ҧ���
    }

    void Update()
    {
        GetInput(); // �Ѻ��Ҩҡ������
        UpdateWheelPoses(); // �Ѿഷ���˹����
    }

    void FixedUpdate()
    {
        HandleMotor(); // �Ǻ�������ͧ¹��
        HandleSteering(); // �Ǻ����ǧ�����
        HandleBraking(); // �Ǻ����á
    }

    // �Ѻ��һ�͹�ҡ�������
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // A/D ���� �١�ë���/���
        verticalInput = Input.GetAxis("Vertical"); // W/S ���� �١�â��/ŧ
    }

    // �к��Ѻ����͹
    private void HandleMotor()
    {
        // ����ç��������ѧ (�к��Ѻ����͹�����ѧ)
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    // �к��ǧ�����
    private void HandleSteering()
    {
        float steerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle; // ���������˹��
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    //�á
    private void HandleBraking()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // �����ç�á
            rearLeftWheelCollider.brakeTorque = motorForce; // ���Ըա���á
            rearRightWheelCollider.brakeTorque = motorForce;
        }
        else
        {
            // �͡�ҡ�ç�á�������衴 Space
            rearLeftWheelCollider.brakeTorque = 0;
            rearRightWheelCollider.brakeTorque = 0;
        }
    }

    // �Ѿഷ�����ع���
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPose(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPose(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPose(rearRightWheelCollider, rearRightWheelTransform);
    }

    // �ԧ����˹� Wheel Collider �Ѻ���� 3D
    private void UpdateWheelPose(WheelCollider collider, Transform wheelTransform)
    {
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheelTransform.position = position; // �Ѿഷ���˹�
        wheelTransform.rotation = rotation; // �Ѿഷ�����ع
    }
}