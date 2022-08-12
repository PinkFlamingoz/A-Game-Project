using Photon.Pun;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    public GameObject InstanciatePrefab;
    public Vector3 position = new Vector3(0f, 0f, 0f);
    void Awake()
    {
        // napraj case za drugi sceni
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
        PhotonNetwork.Instantiate(InstanciatePrefab.name, position, Quaternion.identity);
    }
    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } } // tva za multi play
    public bool HasMoney { get { return PlayerStatus.pointsForBuild >= turretToBuild.cost; } }
    public bool HaveMoney { get { return PointsDisplay.pointsForBuild >= turretToBuild.cost; } }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
