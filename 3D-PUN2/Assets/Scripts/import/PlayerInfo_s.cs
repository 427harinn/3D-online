using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player")]
public class PlayerInfo_s : ScriptableObject
{
    [Header("�v���C���[��")] public string playerName;
    [Header("�v���C���[ID")] public int playerID;
}
