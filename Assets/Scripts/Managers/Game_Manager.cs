using UnityEngine;

public class Game_Manager:MonoBehaviour
{
    public PlayerController Player;
    public int val = 3;
    private static Game_Manager _instance;

    public static Game_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game manager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}