using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
	public RectTransform topLayer;

	[SerializeField] private float _maxWidth;

	void Start()
	{
		_maxWidth = topLayer.rect.width;
	}

	public void SetBar(float current, float Max)
	{
		float percent = current / Max;

		topLayer.sizeDelta = new Vector2(percent * _maxWidth , topLayer.sizeDelta.y);
	}
}
