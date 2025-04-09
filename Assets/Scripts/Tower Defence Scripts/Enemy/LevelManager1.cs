using UnityEngine;

public class LevelManager1 : MonoBehaviour
{
    public static LevelManager1 main;

    public Transform startPoint;
    public Transform[] path;

    private void Awake()
    {
        main = this;
    }
}
