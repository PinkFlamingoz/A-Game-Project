using System.Collections;
using UnityEngine;



public enum PowerUpsAvailable
{
    Grow,
    Speed
}

public class PowerUps : MonoBehaviour
{
    public GameObject pickUpEffect;
    public GameObject FindMe;
    public ParticleSystem spawnEffect;
    public PowerUpsAvailable powerUpType;
    public GameObject Check;
    public float multiplier = 1.4f;
    public float duration = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PickUp(other));
        }
    }

    IEnumerator PickUp(Collider player)
    {
        switch (powerUpType)
        {
            case PowerUpsAvailable.Grow:
                spawnEffect.Emit(1);
                Instantiate(pickUpEffect, transform.position, transform.rotation);
                player.GetComponent<Shoot>().doubleDamageBool = true;
                // player.transform.localScale *= multiplier;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                Destroy(FindMe);
                Destroy(Check);
                // Destroy(pickUpEffect);
                yield return new WaitForSeconds(duration);
                player.GetComponent<Shoot>().doubleDamageBool = false;
                //  player.transform.localScale /= multiplier;
                Destroy(gameObject);
                break;
            case PowerUpsAvailable.Speed:
                spawnEffect.Emit(1);
                Instantiate(pickUpEffect, transform.position, transform.rotation);
                player.GetComponent<TopDownCharacterMover>().MovementSpeed = player.GetComponent<TopDownCharacterMover>().MovementSpeed * multiplier;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                Destroy(FindMe);
                Destroy(Check);
                //Destroy(pickUpEffect);
                yield return new WaitForSeconds(duration);
                player.GetComponent<TopDownCharacterMover>().MovementSpeed = player.GetComponent<TopDownCharacterMover>().defaultMovementSpeed;
                Destroy(gameObject);
                break;
            default:
                Debug.Log("The type is invalid");
                break;
        }
    }
}
