using System.Collections.Generic;
using UnityEngine;

public class ColorCounter : MonoBehaviour
{
    public Renderer floorRenderer;
    private Texture2D floorTexture;

    void Start()
    {
        // Renderer�R���|�[�l���g�̏�����
        if (floorRenderer == null)
        {
            floorRenderer = GetComponent<Renderer>();
        }

        // �}�e���A���̃��C���e�N�X�`�����擾
        if (floorRenderer != null)
        {
            Texture mainTexture = floorRenderer.material.mainTexture;

            // ���C���e�N�X�`����Texture2D�̏ꍇ�̂ݎ擾
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

        // �e�N�X�`���̃s�N�Z�����擾
        Color32[] pixels = texture.GetPixels32();
        Dictionary<Color32, int> colorCount = new Dictionary<Color32, int>();

        // �e�s�N�Z���̐F���J�E���g
        foreach (Color32 pixel in pixels)
        {
            if (pixel.r == 255 && pixel.g == 255 && pixel.b == 255 && pixel.a == 255)
            {
                continue; // ���F�𖳎�
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

        // ���ʂ�\��
        foreach (KeyValuePair<Color32, int> entry in colorCount)
        {
            Debug.Log("Color: " + entry.Key + " Count: " + entry.Value);
        }
    }

    public void onClicked_finish()
    {
        // Renderer�R���|�[�l���g�̍ď�����
        if (floorRenderer == null)
        {
            floorRenderer = GetComponent<Renderer>();
        }

        // �}�e���A���̃��C���e�N�X�`�����擾
        if (floorRenderer != null)
        {
            Texture mainTexture = floorRenderer.material.mainTexture;

            // ���C���e�N�X�`����Texture2D�̏ꍇ
            if (mainTexture is Texture2D)
            {
                floorTexture = (Texture2D)mainTexture;
                CountColors(floorTexture);
            }
            // ���C���e�N�X�`����RenderTexture�̏ꍇ�ATexture2D�ɕϊ�
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
