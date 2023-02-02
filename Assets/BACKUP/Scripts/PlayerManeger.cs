using UnityEngine;

public class PlayerManeger : MonoBehaviour
{
    #region Singleton

    public static PlayerManeger instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
