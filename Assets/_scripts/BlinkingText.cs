using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{	
	Color lerpedColor;

	void Update()
	{
		gameObject.GetComponent<TextMeshProUGUI>().color = lerpedColor;
		lerpedColor = Color.Lerp(Color.red, Color.white, Mathf.PingPong(Time.time, 1));
	}
}
