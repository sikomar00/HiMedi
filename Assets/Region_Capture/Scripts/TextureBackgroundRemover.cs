using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureBackgroundRemover : MonoBehaviour
{
    public Texture2D _textureHolder;
    private Color _colorHolder;

    void Start()
    {
        // 텍스처가 이미 할당되어 있는지 확인합니다.
        if (_textureHolder != null)
        {
            removeTextureBackground();
        }
    }

    void removeTextureBackground()
    {
        _colorHolder = _textureHolder.GetPixel(0, 0);
        floodFill(0, 0);
        floodFill(0, _textureHolder.height - 1);
        floodFill(_textureHolder.width - 1, 0);
        floodFill(_textureHolder.width - 1, _textureHolder.height - 1);
        _textureHolder.Apply();
    }

    void floodFill(int x, int y)
    {
        if (x < 0 || y < 0 || x > _textureHolder.width || y > _textureHolder.height)
            return;

        Color color = _textureHolder.GetPixel(x, y);
        if (isWhite(color) && color.a > 0)
        {
            color.a = 0;
            _textureHolder.SetPixel(x, y, color);
            floodFill(x - 1, y);
            floodFill(x + 1, y);
            floodFill(x, y - 1);
            floodFill(x, y + 1);
        }
    }

    bool isWhite(Color color)
    {
        float threshold = 1f;
        bool r = Mathf.Abs(color.r - _colorHolder.r) < threshold;
        bool g = Mathf.Abs(color.g - _colorHolder.g) < threshold;
        bool b = Mathf.Abs(color.b - _colorHolder.b) < threshold;
        return r && g && b;
    }
}