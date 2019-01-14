using UnityEngine;

public class ContollPanel : MonoBehaviour
{
	[SerializeField] GameObject panel;


	public void PanelControll()
	{
		if (panel.activeSelf) { panel.SetActive(false); }
		else { panel.SetActive(true); }
	}
}
