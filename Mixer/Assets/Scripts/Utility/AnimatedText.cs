using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// AnimatedText objects can be used to spawn moving text that fades
/// over time. Example: +(points) indicators that move up from the
/// player's score and fade over time when the player is awarded points.
/// 
/// USE: Instantiate a prefab that implements this script, then call
/// generateText() to start the animation. When the animation 
/// is complete, this object will automatically be destroyed.
/// </summary>
public class AnimatedText : MonoBehaviour
{
    private bool initialized = false;
    public float speed;             // the speed at which the text moves
    public Vector3 direction;       // the direction at which this text moves
    public float fadeTime;          // the speed at which the text will fade
    private float startAlpha;


    // assigns the proper values to the AnimatedText object, then begins the fadeout animation
    public void generateText(string text, Color color)
    {
        if (!initialized)
        {
            this.initialized = true;
            this.gameObject.GetComponent<Text>().text = text;
            this.gameObject.GetComponent<Text>().color = color;
            this.startAlpha = color.a;
            StartCoroutine(animate());
        }
    }


    // assigns the proper values to the AnimatedText object, then begins the fadeout animation
    // this option allows overriding the values set in the Unity Editor
    public void generateText(string text, Color color, float speed, Vector3 direction, float fadeTime)
    {
        if (!initialized)
        {
            this.initialized = true;
            this.speed = speed;
            this.direction = direction;
            this.fadeTime = fadeTime;
            this.gameObject.GetComponent<Text>().text = text;
            this.gameObject.GetComponent<Text>().color = color;
            this.startAlpha = color.a;
            StartCoroutine(animate());
        }
    }


    // should be ran as a coroutine. creates the fade effect, then destroys the object
    private IEnumerator animate()
    {
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;

        while (progress < 1.0)
        {
            // fade step
            Color currentColor = GetComponent<Text>().color;
            this.gameObject.GetComponent<Text>().color = 
                new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(startAlpha, 0, progress));
            progress += rate * Time.deltaTime;

            // movement step
            float translation = speed * Time.deltaTime;
            this.gameObject.transform.Translate(direction * translation);

            yield return null;
        }
        Destroy(this.gameObject);
    }
}
