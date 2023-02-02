using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    [SerializeField] private float maxDistance;

    private GameObject triggeringEnemy;

    public float damage;
    public float doubleDamage = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            if (FindObjectOfType<Shoot>().GetComponent<Shoot>().doubleDamageBool == false)
            {
                triggeringEnemy.GetComponent<Enemy>().health -= damage;
            }
            else
            {
                triggeringEnemy.GetComponent<Enemy>().health -= damage * doubleDamage;
            }
            Destroy(this.gameObject);
        }

    }
}
