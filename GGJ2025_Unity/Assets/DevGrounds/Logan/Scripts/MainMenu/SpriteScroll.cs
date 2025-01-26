using System;
using UnityEngine;
using UnityEngine.UI;

public class SpriteScroll : MonoBehaviour
{
    public enum Direction { Left, Right, PingPong, ScaleInOut}

    public Direction scrollDirection;
    public float scrollSpeed;
    public float offscreenSpacing;
    private RectTransform imageTransform;
    float screenWidth, screenHeight;

    float spriteWidth, spriteHeight;
    bool curDirLeft;

    private void Awake()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        imageTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        spriteWidth = imageTransform.rect.width;
        spriteHeight = imageTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        switch(scrollDirection)
        {
            case Direction.Left:
                imageTransform.position = imageTransform.position + new Vector3(-scrollSpeed, 0, 0);
                if(imageTransform.position.x <= -offscreenSpacing)
                {
                    imageTransform.position = new Vector3(screenWidth + offscreenSpacing, imageTransform.position.y, imageTransform.position.z);
                }
                break;

            case Direction.Right:
                imageTransform.position = imageTransform.position + new Vector3(scrollSpeed, 0, 0);
                if (imageTransform.position.x >= screenWidth + offscreenSpacing)
                {
                    imageTransform.position = new Vector3(-offscreenSpacing, imageTransform.position.y, imageTransform.position.z);
                }
                break;

            case Direction.PingPong:

                imageTransform.position = curDirLeft ? imageTransform.position + new Vector3(-scrollSpeed, 0, 0) : imageTransform.position + new Vector3(scrollSpeed, 0, 0);
                if (imageTransform.position.x >= screenWidth + offscreenSpacing || imageTransform.position.x < -25 - offscreenSpacing)
                {
                    imageTransform.position = new Vector3(curDirLeft ? 0 : screenWidth, imageTransform.position.y, imageTransform.position.z);
                    curDirLeft = !curDirLeft;
                }
                break;

            case Direction.ScaleInOut:

                float scaleValue = curDirLeft ? 0.75f : 1.25f;
                float curScale = imageTransform.localScale.x;
                float scaleAdjust = scrollSpeed / 100f;
                if(curDirLeft) { scaleAdjust = -scaleAdjust; }

                imageTransform.localScale.Set(curScale + scaleAdjust, curScale + scaleAdjust, curScale + scaleAdjust);

                if (imageTransform.localScale.x < 0.75 || imageTransform.localScale.x > 1.25)
                {
                    imageTransform.localScale.Set(scaleValue, scaleValue, scaleValue);
                    curDirLeft = !curDirLeft;
                }

                break;

            default:
                break;
        }
    }
}
