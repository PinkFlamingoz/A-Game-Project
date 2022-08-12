using UnityEngine;
//Not Done///////////////////////////////////////////////////////////////////////////////////////////
public enum HitSurface
{
    Ground,
    Trees
}
public class Hit : MonoBehaviour
{
    public ParticleSystem ground;
    public ParticleSystem trees;
    public HitSurface surface;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            switch (surface)
            {
                case HitSurface.Ground:
                    ground.Emit(1);
                    break;
                case HitSurface.Trees:
                    trees.Emit(1);
                    break;
                default:
                    Debug.Log("The type is invalid");
                    break;
            }
        }
    }
}
