using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraFollow : MonoBehaviour
{
    //Variables
    public Transform player;
    public float smooth = 0.3f;
    public float hight;
    public float cameraSpeed = 42;
    public float far = 10;
    public float near = 2;
    public Camera camera;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MultiPlayGame"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z;
        pos.y = player.position.y + hight;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && camera.orthographicSize < far)
        {
            camera.orthographicSize += 1; //Change values according to your requirements
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && camera.orthographicSize > near)
        {
            camera.orthographicSize -= 1;
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(Vector3.up * cameraSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("q"))
        {
            transform.Rotate(Vector3.down * cameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("p"))
        {
            Quaternion rotation = Quaternion.Euler(30.0f, 45.0f, 0);
            camera.transform.rotation = rotation;
        }
    }
}
