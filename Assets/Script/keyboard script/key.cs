using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class key : MonoBehaviour
{
    public SpriteAtlas atlas;
    public Image keyImage;
    public KeyCode code;
    public Image background;

    public void SetData(KeyCode _code)
    {
        code = _code;
        keyImage.sprite = atlas.GetSprite($"{code}key");
        SetSuccese(false);
    }

    public void SetSuccese(bool isSuccess)
    {
        keyImage.color = (isSuccess) ? Color.gray : Color.white;
        background.color = (isSuccess) ? Color.yellow : Color.white;
    }
}
