using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(InputHandler))]
public class TopDownCharacterMover : MonoBehaviour
{
    public int scene;
    private InputHandler _input;
    PhotonView _view;
    [SerializeField] private bool RotateTowardMouse;

    [SerializeField] public float MovementSpeed;
    [SerializeField] public float defaultMovementSpeed = 6;

    [SerializeField] private float RotationSpeed;

    [SerializeField] private Camera Camera;

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] [Range(1, 10)] private float jumpVelocity;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    private void Awake()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MultiPlayGame"))
        {
            _view = GetComponent<PhotonView>();
        }
        _input = GetComponent<InputHandler>();
        rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (scene)
        {
            case 3:
                if (_view.IsMine)
                {
                    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
                    Camera = FindObjectOfType<Camera>();
                    //Debug.Log(_input.MousePosition);
                    var targetVectorr = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
                    var movementVectorr = MoveTowardTarget(targetVectorr);

                    if (!RotateTowardMouse)
                    {
                        RotateTowardMovementVector(movementVectorr);
                    }
                    if (RotateTowardMouse)
                    {
                        RotateFromMouseVector();
                    }
                    if (Input.GetButtonDown("Jump") && isGrounded)
                    {
                        rigidbody.velocity = Vector3.up * jumpVelocity;
                    }
                }
                break;
            case 1:
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
                //Debug.Log(_input.MousePosition);
                var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
                var movementVector = MoveTowardTarget(targetVector);

                if (!RotateTowardMouse)
                {
                    RotateTowardMovementVector(movementVector);
                }
                if (RotateTowardMouse)
                {
                    RotateFromMouseVector();
                }
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    rigidbody.velocity = Vector3.up * jumpVelocity;
                }
                break;
            default:
                print("Wait for level load");
                break;
        }

    }
    private void RotateFromMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }
}