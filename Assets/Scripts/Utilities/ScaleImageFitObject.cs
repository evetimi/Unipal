using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ScaleImageFitObject : MonoBehaviour
{
    public RectTransform basedTransform;
    public Image image;

    private void Update() {
        if (basedTransform == null || image == null || image.sprite == null) {
            return;
        }

        RectTransform rectTransform = GetComponent<RectTransform>();

        //rectTransform.sizeDelta

        Sprite sprite = image.sprite;
        Vector2 texture = new Vector2(sprite.textureRect.width, sprite.textureRect.height);
        Vector2 based = new Vector2(basedTransform.rect.width, basedTransform.rect.height);
        //Debug.Log(sprite.textureRect.width + " " + sprite.textureRect.height);
        //Debug.Log(basedTransform.rect.width + " " +  basedTransform.rect.height);

        Vector2 difference = based - texture;

        float ratio;
        if (difference.x > difference.y) {
            ratio = based.x / texture.x;
        } else {
            ratio = based.y / texture.y;
        }

        rectTransform.sizeDelta = texture * ratio;
    }
}
