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

        // �v���C���[�̐F��ݒ�
        SetPlayerColor();
    }

    // �v���C���[�̐F��ݒ肷�郁�\�b�h
    private void SetPlayerColor()
    {
        Color playerColor;
        // �v���C���[��1P�Ȃ�A����ȊO�͐�
        if (photonView.OwnerActorNr == 1)
        {
            playerColor = Color.blue;
        }
        else
        {
            playerColor = Color.red;
        }
        //�u���V�̐F��ύX
        collisionPainter.brush.brushColor = playerColor;

        //�q�I�u�W�F�N�g�̃}�e���A���̐F���u���V�̐F�Ɠ���
        foreach (var renderer in meshRenderers)
        {
            renderer.material.color = playerColor;
        }
    }
}
