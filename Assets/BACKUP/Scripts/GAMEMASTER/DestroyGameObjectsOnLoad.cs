using UnityEngine;

public class DestroyGameObjectsOnLoad : MonoBehaviour
{
    public void destroyEnemy()
    {
        var clones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }
}
