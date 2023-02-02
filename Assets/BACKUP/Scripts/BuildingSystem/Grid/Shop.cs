using UnityEngine;

public class Shop : MonoBehaviour
{

    public TurretBlueprint damageTurret;
    public TurretBlueprint buffTurret;
    public TurretBlueprint wall;
    public TurretBlueprint barrierDamage;
    public TurretBlueprint barrierBlock;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void DamageTurret()
    {
        Debug.Log("Damage Turret Selected");
        buildManager.SelectTurretToBuild(damageTurret);
    }

    public void BuffTurret()
    {
        Debug.Log("Buff Turret Selected");
        buildManager.SelectTurretToBuild(buffTurret);
    }

    public void WallObject()
    {
        Debug.Log("Wall Selected");
        buildManager.SelectTurretToBuild(wall);
    }
    public void BarrierDamage()
    {
        Debug.Log("Barrier Damage Selected");
        buildManager.SelectTurretToBuild(barrierDamage);
    }
    public void BarrierBlock()
    {
        Debug.Log("Barrier Block Selected");
        buildManager.SelectTurretToBuild(barrierBlock);
    }

}
