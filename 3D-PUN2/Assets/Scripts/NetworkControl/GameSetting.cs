using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSetting")]
public class GameSetting : ScriptableObject
{
    [Header("移動するシーン")] public string SceneName;
}
