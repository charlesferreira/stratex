using UnityEngine;
using System.Collections;

public class FlashSprite : MonoBehaviour {

    public SpriteRenderer sprite;

    [Range(0.01f, 10f)] public float frequence = .2f;
    [Range(0, 1)] public float alphaMax = 1;
    [Range(0, 1)] public float alphaMin = 0;

    bool flash = false;

    bool up = false;

	void Update () {

        if (!flash)
            return;

        var range = alphaMax - alphaMin;

        if (up)
        {
            var alpha = sprite.color.a + (frequence / range) * Time.deltaTime;
            if (alpha > alphaMax)
            {
                alpha = alphaMax - (alpha - alphaMax);
                up = false;
            }
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        }
        else
        {
            var alpha = sprite.color.a - (frequence / range) * Time.deltaTime;
            if (alpha < alphaMin)
            {
                alpha = alphaMin + (alphaMin - alpha);
                up = true;
            }
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        }
	}

    public void StartFlash()
    {
        flash = true;
    }
    public void StopFlash()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        flash = false;
    }
}
