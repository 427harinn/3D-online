using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    GameObject chatPanel;
    GameObject roomInfoPanel;
    int state = 1;
    // Start is called before the first frame update
    void Start()
    {
        //取得
        chatPanel = GameObject.FindGameObjectWithTag("ChatPanel").transform.GetChild(0).gameObject;
        roomInfoPanel = GameObject.FindGameObjectWithTag("RoomInfoPanel").transform.GetChild(0).gameObject;

        //非表示にする
        chatPanel.SetActive(false);
        roomInfoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //"e"で表示を切り替える
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (state)
            {
                case 0:
                    chatPanel.SetActive(false);
                    roomInfoPanel.SetActive(false);
                    state++;
                    break;
                case 1:
                    chatPanel.SetActive(false);
                    roomInfoPanel.SetActive(true);
                    state++;
                    break;
                case 2:
                    chatPanel.SetActive(true);
                    roomInfoPanel.SetActive(false);
                    state = 0;
                    break;
                default:
                    break;
            }
            
        }
    }
}
