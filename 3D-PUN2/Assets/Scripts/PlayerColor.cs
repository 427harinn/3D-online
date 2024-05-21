using Es.InkPainter.Sample;
using Photon.Pun;
using UnityEngine;

public class PlayerColor : MonoBehaviourPunCallbacks
{
    private CollisionPainter collisionPainter;
    private MeshRenderer[] meshRenderers;

    // Start is called before the first frame update
    void Start()
    {
        collisionPainter = GetComponent<CollisionPainter>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        // プレイヤーの色を設定
        SetPlayerColor();
    }

    // プレイヤーの色を設定するメソッド
    private void SetPlayerColor()
    {
        Color playerColor;
        // プレイヤーが1Pなら青、それ以外は赤
        if (photonView.OwnerActorNr == 1)
        {
            playerColor = Color.blue;
        }
        else
        {
            playerColor = Color.red;
        }
        //ブラシの色を変更
        collisionPainter.brush.brushColor = playerColor;

        //子オブジェクトのマテリアルの色をブラシの色と統一
        foreach (var renderer in meshRenderers)
        {
            renderer.material.color = playerColor;
        }
    }
}
