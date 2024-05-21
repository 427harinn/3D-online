using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class ReplacePlayerAvatar : MonoBehaviour
{
    //リストの順にプレイヤー１、プレイヤー２・・・と置き換わります。
    [SerializeField] List<GameObject> PlayerAvatarList;

    [SerializeField] GameObject Camera;
    // Start is called before the first frame update

    private void Start()
    {
        ReplaceMyAvater();
    }

    //プレイヤーのネットワークオブジェクトを生成しダミーアバターと置き換える
    void ReplaceMyAvater()
    {
        GameObject myAvatar = PhotonNetwork.Instantiate("GamePlayerAvatar", Vector3.zero, Quaternion.identity);
        myAvatar.transform.position = PlayerAvatarList[Resources.Load<PlayerInfo_s>("PlayerInfo").playerID].transform.position;
        Debug.Log(Resources.Load<PlayerInfo_s>("PlayerInfo").playerID);
        //カメラをセット
        myAvatar.GetComponent<PlayerController>().SetMyCamera(Camera.transform.GetChild(0).gameObject.GetComponent<Camera>());
        Camera.transform.parent = myAvatar.transform;
        Camera.transform.localPosition = new Vector3(0, 2.4f, 0);

        GManager.instance.PlayerList.Add(myAvatar);
        //リストの中身を削除
        foreach (GameObject obj in PlayerAvatarList)
        {
            Destroy(obj);
        }
        PlayerAvatarList.Clear();
    }

    public List<GameObject> GetPlayerAvatarList()
    {
        return PlayerAvatarList;
    }

    public GameObject GetCameraObj()
    {
        return Camera;
    }
}
