using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
	[SerializeField] Image Abutton;
	[SerializeField] Image Bbutton;
	[SerializeField] Image Ybutton;
	[SerializeField] Image Xbutton;
	[SerializeField] Image Runbutton;
	[SerializeField] Image Sneakbutton;

	Color pressedColorA;
	Color pressedColorB;
	Color pressedColorY;
	Color pressedColorX;
	Color pressedColorRun;
	Color pressedColorSneak;

	void Start()
	{
		pressedColorA = Abutton.GetComponent<Image>().color;
		pressedColorB = Bbutton.GetComponent<Image>().color;
		pressedColorY = Ybutton.GetComponent<Image>().color;
		pressedColorX = Xbutton.GetComponent<Image>().color;
		pressedColorRun = Runbutton.GetComponent<Image>().color;
		pressedColorSneak = Sneakbutton.GetComponent<Image>().color;
	}
	void Update()
	{
		if (Input.GetButtonDown("PickUp"))
		{
			pressedColorA.a = 0.5f;
			Abutton.GetComponent<Image>().color = pressedColorA;
		}
		else if (Input.GetButtonUp("PickUp"))
		{
			pressedColorA.a = 1f;
			Abutton.GetComponent<Image>().color = pressedColorA;
		}
		if (Input.GetButtonDown("Drop"))
		{
			pressedColorB.a = 0.5f;
			Bbutton.GetComponent<Image>().color = pressedColorB;
		}
		else if (Input.GetButtonUp("Drop"))
		{
			pressedColorB.a = 1f;
			Bbutton.GetComponent<Image>().color = pressedColorB;
		}
		if (Input.GetButtonDown("Eat"))
		{
			pressedColorY.a = 0.5f;
			Ybutton.GetComponent<Image>().color = pressedColorY;
		}
		else if (Input.GetButtonUp("Eat"))
		{
			pressedColorY.a = 1f;
			Ybutton.GetComponent<Image>().color = pressedColorA;
		}
		if (Input.GetButtonDown("Attack"))
		{
			pressedColorX.a = 0.5f;
			Xbutton.GetComponent<Image>().color = pressedColorX;
		}
		else if (Input.GetButtonUp("Attack"))
		{
			pressedColorX.a = 1f;
			Xbutton.GetComponent<Image>().color = pressedColorX;
		}
		if (Input.GetAxis("Run") > 0.1f)
		{
			pressedColorRun.a = 0.5f;
			Runbutton.GetComponent<Image>().color = pressedColorRun;
		}
		else if (Input.GetAxis("Run") < 0.1f)
		{
			pressedColorRun.a = 1f;
			Runbutton.GetComponent<Image>().color = pressedColorRun;
		}
		if (Input.GetAxis("Sneak") > 0.1f)
		{
			pressedColorSneak.a = 0.5f;
			Sneakbutton.GetComponent<Image>().color = pressedColorSneak;
		}
		else if (Input.GetAxis("Sneak") < 0.1f)
		{
			pressedColorSneak.a = 1f;
			Sneakbutton.GetComponent<Image>().color = pressedColorSneak;
		}
	}
}
