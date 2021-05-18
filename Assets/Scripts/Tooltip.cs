using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
	public static string text;
	public static bool isUI;

	public Color BGColor = Color.white;
	public Color textColor = Color.black;
	public enum ProjectMode { Tooltip3D = 0, Tooltip2D = 1 };
	public ProjectMode tooltipMode = ProjectMode.Tooltip3D;
	public int fontSize = 14; // размер шрифта
	public int maxWidth = 250; // максимальная ширина Tooltip
	public int border = 10; // ширина обводки
	public RectTransform box;
	public Text boxText;
	public Camera _camera;
	public float speed = 10; // скорость плавного затухания и проявления

	private Image[] img;
	private Color BGColorFade;
	private Color textColorFade;

	void Awake()
	{
		img = new Image[1];
		img[0] = box.GetComponent<Image>();
		box.sizeDelta = new Vector2(maxWidth, box.sizeDelta.y);
		BGColorFade = BGColor;
		BGColorFade.a = 0;
		textColorFade = textColor;
		textColorFade.a = 0;
		isUI = false;
		foreach (Image bg in img)
		{
			bg.color = BGColorFade;
		}
		boxText.color = textColorFade;
		boxText.alignment = TextAnchor.MiddleCenter;
	}

	void LateUpdate()
	{
		if (MainScript.Instance.state != GameState.City)
			return;

		bool show = false;
		boxText.fontSize = fontSize;

		if (tooltipMode == ProjectMode.Tooltip3D)
		{
			RaycastHit hit;
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.GetComponent<TooltipText>())
				{
					text = hit.transform.GetComponent<TooltipText>().text;
					show = true;
				}
			}
		}
		else
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			foreach (var h in hit)
			if (h.transform != null)
			{
				if (h.transform.GetComponent<TooltipText>())
				{
					if (h.transform.GetComponent<ActionButton>().IsActivated)
					{
						text = h.transform.GetComponent<TooltipText>().text;
						show = true;
					}
				}
			}
		}

		boxText.text = text;
		float width = maxWidth;
		if (boxText.preferredWidth <= maxWidth - border) width = boxText.preferredWidth + border;
		box.sizeDelta = new Vector2(width, boxText.preferredHeight + border);

		float arrowShift = width / 4; // сдвиг позиции стрелки по Х

		if (show || isUI)
		{

			float curY = Input.mousePosition.y + box.sizeDelta.y / 2;
			if (curY + box.sizeDelta.y / 2 > Screen.height) // если Tooltip выходит за рамки экрана, в данном случаи по высоте
			{
				curY = Input.mousePosition.y - box.sizeDelta.y / 2;
			}

			float curX = Input.mousePosition.x + arrowShift;
			if (curX + box.sizeDelta.x / 2 > Screen.width)
			{
				curX = Input.mousePosition.x - arrowShift;
			}

			box.position = new Vector2(curX, curY);


			foreach (Image bg in img)
			{
				bg.color = Color.Lerp(bg.color, BGColor, speed * Time.deltaTime);
			}
			boxText.color = Color.Lerp(boxText.color, textColor, speed * Time.deltaTime);
		}
		else
		{
			foreach (Image bg in img)
			{
				bg.color = Color.Lerp(bg.color, BGColorFade, speed * Time.deltaTime);
			}
			boxText.color = Color.Lerp(boxText.color, textColorFade, speed * Time.deltaTime);
		}
	}
}