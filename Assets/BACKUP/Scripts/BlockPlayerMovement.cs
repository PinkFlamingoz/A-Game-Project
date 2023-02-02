using UnityEngine;

public class BlockPlayerMovement : MonoBehaviour
{
    public GameObject goHere;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 direction = other.transform.position - goHere.transform.position;
            direction.Normalize();
            var magnitude = 100;
            other.GetComponent<Rigidbody>().AddForce(-direction * magnitude);
        }
    }
}
