using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
    public int scene;

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    [SerializeField] private Renderer rend;
    [SerializeField] private Color startColor;

    BuildManager buildManager;

    public bool lvl3 = false;

    public PointsDisplay[] PointsDisplayScript;
    public PhotonView _view;

    public bool isFree = true;
    public void Awake()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MultiPlayGame"))
        {
            _view = GetComponent<PhotonView>();
        }
    }
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;


        buildManager = BuildManager.instance;
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        if (!isFree)
        {
            Debug.Log(isFree);
            Debug.Log("zafateno");
            return;
        }
        //_view.RPC("BuildRPC", RpcTarget.All);
        BuildTurret(buildManager.GetTurretToBuild());
    }
    [PunRPC]
    public void BuildRPC()
    {
        isFree = false;
    }
    [PunRPC]
    public void BuildSellRPC()
    {
        isFree = true;
    }
    void BuildTurret(TurretBlueprint blueprint)
    {
        switch (scene)
        {
            case 3:
                if (PointsDisplay.pointsForBuild < blueprint.cost)
                {
                    Debug.Log("Not enough money to build that!");
                    return;
                }
                PointsDisplayScript = FindObjectsOfType<PointsDisplay>();
                for (int i = 0; i < PointsDisplayScript.Length; i++)
                {
                    PointsDisplayScript[i].currentPoints -= blueprint.cost;
                    PointsDisplayScript[i].pointsText.text = "Points: " + PointsDisplayScript[i].currentPoints;
                }

                GameObject _turret = (GameObject)PhotonNetwork.Instantiate(blueprint.prefab.name, GetBuildPosition(), Quaternion.identity);
                turret = _turret;

                turretBlueprint = blueprint;

                GameObject effect = (GameObject)PhotonNetwork.Instantiate(buildManager.buildEffect.name, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 5f);
                Debug.Log("Turret build!");
                break;
            case 1:
                if (PlayerStatus.pointsForBuild < blueprint.cost)
                {
                    Debug.Log("Not enough money to build that!");
                    return;
                }

                FindObjectOfType<PlayerStatus>().points -= blueprint.cost;

                GameObject _turrett = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
                turret = _turrett;

                turretBlueprint = blueprint;

                GameObject effectt = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
                Destroy(effectt, 5f);

                Debug.Log("Turret build!");
                break;
            default:
                print("Wait for level load");
                break;
        }
    }

    public void UpgradeTurret()
    {
        switch (lvl3)
        {
            case false:
                switch (scene)
                {
                    case 3:
                        if (PointsDisplay.pointsForBuild < turretBlueprint.upgradeCost)
                        {
                            Debug.Log("Not enough money to upgrade that!");
                            return;
                        }
                        PointsDisplayScript = FindObjectsOfType<PointsDisplay>();
                        for (int i = 0; i < PointsDisplayScript.Length; i++)
                        {
                            PointsDisplayScript[i].currentPoints -= turretBlueprint.upgradeCost;
                            PointsDisplayScript[i].pointsText.text = "Points: " + PointsDisplayScript[i].currentPoints;
                        }

                        //Get rid of the old turret
                        PhotonNetwork.Destroy(turret);
                        //Build a new one
                        GameObject _turret = (GameObject)PhotonNetwork.Instantiate(turretBlueprint.upgradedPrefab.name, GetBuildPosition(), Quaternion.identity);
                        turret = _turret;

                        GameObject effect = (GameObject)PhotonNetwork.Instantiate(buildManager.buildEffect.name, GetBuildPosition(), Quaternion.identity);

                        Destroy(effect, 5f);
                        //isUpgraded = true;
                        lvl3 = true;

                        Debug.Log("Turret upgraded!");
                        break;
                    case 1:
                        if (PlayerStatus.pointsForBuild < turretBlueprint.upgradeCost)
                        {
                            Debug.Log("Not enough money to upgrade that!");
                            return;
                        }
                        FindObjectOfType<PlayerStatus>().points -= turretBlueprint.upgradeCost;

                        //Get rid of the old turret
                        Destroy(turret);

                        //Build a new one
                        GameObject _turrett = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
                        turret = _turrett;

                        GameObject effectt = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
                        Destroy(effectt, 5f);

                        //isUpgraded = true;
                        lvl3 = true;

                        Debug.Log("Turret upgraded!");
                        break;
                    default:
                        print("Wait for level load");
                        break;
                }
                break;
            case true:
                switch (scene)
                {
                    case 3:
                        if (PointsDisplay.pointsForBuild < turretBlueprint.upgradeCostlvl3)
                        {
                            Debug.Log("Not enough money to upgrade that!");
                            return;
                        }
                        PointsDisplayScript = FindObjectsOfType<PointsDisplay>();
                        for (int i = 0; i < PointsDisplayScript.Length; i++)
                        {
                            PointsDisplayScript[i].currentPoints -= turretBlueprint.upgradeCostlvl3;
                            PointsDisplayScript[i].pointsText.text = "Points: " + PointsDisplayScript[i].currentPoints;
                        }

                        //Get rid of the old turret
                        PhotonNetwork.Destroy(turret);

                        //Build a new one
                        GameObject _turrettt = (GameObject)PhotonNetwork.Instantiate(turretBlueprint.upgradedPrefablvl3.name, GetBuildPosition(), Quaternion.identity);
                        turret = _turrettt;

                        GameObject effecttt = (GameObject)PhotonNetwork.Instantiate(buildManager.buildEffect.name, GetBuildPosition(), Quaternion.identity);
                        Destroy(effecttt, 5f);

                        isUpgraded = true;
                        break;
                    case 1:
                        if (PlayerStatus.pointsForBuild < turretBlueprint.upgradeCostlvl3)
                        {
                            Debug.Log("Not enough money to upgrade that!");
                            return;
                        }
                        FindObjectOfType<PlayerStatus>().points -= turretBlueprint.upgradeCostlvl3;

                        //Get rid of the old turret
                        Destroy(turret);

                        //Build a new one
                        GameObject _turretttt = (GameObject)Instantiate(turretBlueprint.upgradedPrefablvl3, GetBuildPosition(), Quaternion.identity);
                        turret = _turretttt;

                        GameObject effectttt = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
                        Destroy(effectttt, 5f);

                        isUpgraded = true;
                        break;
                    default:
                        print("Wait for level load");
                        break;
                }
                break;
        }

    }
    public void SellTurret()
    {
        switch (scene)
        {
            case 3:
                PointsDisplayScript = FindObjectsOfType<PointsDisplay>();
                for (int i = 0; i < PointsDisplayScript.Length; i++)
                {
                    PointsDisplayScript[i].currentPoints += turretBlueprint.GetSellAmount();
                    PointsDisplayScript[i].pointsText.text = "Points: " + PointsDisplayScript[i].currentPoints;
                }

                GameObject effect = (GameObject)PhotonNetwork.Instantiate(buildManager.sellEffect.name, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 5f);

                lvl3 = false;
                isUpgraded = false;
                _view.RPC("BuildSellRPC", RpcTarget.All);
                PhotonNetwork.Destroy(turret);

                turretBlueprint = null;
                break;
            case 1:
                FindObjectOfType<PlayerStatus>().points += turretBlueprint.GetSellAmount();

                GameObject effectt = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
                Destroy(effectt, 5f);
                lvl3 = false;
                isUpgraded = false;

                Destroy(turret);
                turretBlueprint = null;
                break;
            default:
                print("Wait for level load");
                break;
        }
    }
    void OnMouseEnter()
    {
        switch (scene)
        {
            case 3:
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                if (!buildManager.CanBuild)
                    return;

                if (buildManager.HaveMoney)
                {
                    rend.material.color = hoverColor;
                }
                else
                {
                    rend.material.color = notEnoughMoneyColor;
                }
                break;
            case 1:
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                if (!buildManager.CanBuild)
                    return;

                if (buildManager.HasMoney)
                {
                    rend.material.color = hoverColor;
                }
                else
                {
                    rend.material.color = notEnoughMoneyColor;
                }
                break;
            default:
                print("Wait for level load");
                break;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
