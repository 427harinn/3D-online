using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    float time;
    bool isEnd = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd) { return; }

        // 経過時間を求める
        time = unchecked(ServerTime.GetServerTime()) / 1000f;
        //Debug.Log(time);
    }
}
