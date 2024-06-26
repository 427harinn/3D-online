using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public List<GameObject> PlayerList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}