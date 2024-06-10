using System.Collections.Generic;
using UnityEngine;

public class ColorCounter : MonoBehaviour
{
    public Renderer floorRenderer;
    private Texture2D floorTexture;

    void Start()
    {
        // Rendererコンポーネントの初期化
        if (floorRenderer == null)
        {
            floorRenderer = GetComponent<Renderer>();
        }

        // マテリアルのメインテクスチャを取得
        if (floorRenderer != null)
        {
            Texture mainTexture = floorRenderer.material.mainTexture;

            // メインテクスチャがTexture2Dの場合のみ取得
            if (mainTexture is Texture2D)
            {
                floorTexture = (Texture2D)mainTexture;
                Debug.Log("Floor texture successfully obtained in Start method.");
            }
            else
            {
                Debug.LogError("The main texture in Start is not a Texture2D!");
            }
        }
        else
        {
            Debug.LogError("Renderer component is missing in Start!");
        }
    }

    void CountColors(Texture2D texture)
    {
        if (texture == null)
        {
            Debug.LogError("Texture is null!");
            return;
        }

        if (!texture.isReadable)
        {
            Debug.LogError("Texture is not readable. Please enable Read/Write in the texture import settings.");
            return;
        }

        // テクスチャのピクセルを取得
        Color32[] pixels = texture.GetPixels32();
        Dictionary<Color32, int> colorCount = new Dictionary<Color32, int>();

        // 各ピクセルの色をカウント
        foreach (Color32 pixel in pixels)
        {
            if (pixel.r == 255 && pixel.g == 255 && pixel.b == 255 && pixel.a == 255)
            {
                continue; // 白色を無視
            }

            if (colorCount.ContainsKey(pixel))
            {
                colorCount[pixel]++;
            }
            else
            {
                colorCount[pixel] = 1;
            }
        }

        // 結果を表示
        foreach (KeyValuePair<Color32, int> entry in colorCount)
        {
            Debug.Log("Color: " + entry.Key + " Count: " + entry.Value);
        }
    }

    public void onClicked_finish()
    {
        // Rendererコンポーネントの再初期化
        if (floorRenderer == null)
        {
            floorRenderer = GetComponent<Renderer>();
        }

        // マテリアルのメインテクスチャを取得
        if (floorRenderer != null)
        {
            Texture mainTexture = floorRenderer.material.mainTexture;

            // メインテクスチャがTexture2Dの場合
            if (mainTexture is Texture2D)
            {
                floorTexture = (Texture2D)mainTexture;
                CountColors(floorTexture);
            }
            // メインテクスチャがRenderTextureの場合、Texture2Dに変換
            else if (mainTexture is RenderTexture)
            {
                RenderTexture renderTexture = (RenderTexture)mainTexture;
                RenderTexture.active = renderTexture;
                Texture2D tempTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
                tempTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                tempTexture.Apply();
                RenderTexture.active = null;
                CountColors(tempTexture);
            }
            else
            {
                Debug.LogError("The main texture in onClicked_finish is not a Texture2D or RenderTexture! It is: " + mainTexture.GetType());
            }
        }
        else
        {
            Debug.LogError("Renderer component is missing in onClicked_finish!");
        }
    }
}
