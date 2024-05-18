using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject result;
    [SerializeField] TMP_Text clearTimeText;
    int goalPlayerNum;
    float time;
    bool isEnd = false;

    private void Update()
    {
        if (isEnd) { return; }

        // �o�ߎ��Ԃ����߂�
        time = unchecked(ServerTime.GetServerTime()) / 1000f;

        if (goalPlayerNum == 4)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("NetworkPlayer");
            foreach (GameObject player in players)
            {
                player.GetComponent<BasicPlayerController>().IsStop = true;
                clearTimeText.text = $"�N���A���Ԃ�{time}�b�ł��B";
                result.SetActive(true);
            }
            isEnd = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        goalPlayerNum++;
    }

    private void OnTriggerExit(Collider other)
    {
        goalPlayerNum--;
    }

    public void EndButtonClicked()
    {
        GameObject.FindGameObjectWithTag("NetworkController").GetComponent<NetworkDisconnect>().RpcEndGame();
    }
}
