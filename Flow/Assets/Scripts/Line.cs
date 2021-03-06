﻿using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Line : MonoBehaviour
{
	public List<Square> squares;
	public List<LineRenderer> lines;

	public List<Square> ConnectedSquares
	{
		get { return squares; }
	}

	public List<LineRenderer> GetLine
	{
		get { return lines; }
	}

	void Start()
	{
		squares = new List<Square>();
		lines = new List<LineRenderer>();
	}

	public bool Empty()
	{
		if (squares.Count == 0 && lines.Count == 0)
			return true;
		else
			return false;
	}

	public void Clear()
	{
		for (int i = 0; i < lines.Count; i++)
			Destroy(lines[i].gameObject);
		lines.Clear();
		squares[0].GetComponent<FlowEnd>().Unconnect();
		squares.Clear();
	}

	public void CreateLine(Square _object_1, Square _object_2)
	{
#if UNITY_EDITOR
		GameObject lineprefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LinePrefab.prefab");
		lines.Add(Instantiate(lineprefab.GetComponent<LineRenderer>()));
#endif

		if (_object_1.transform.position.x == _object_2.transform.position.x)
		{
			lines[squares.Count - 2].SetPosition(0, _object_1.transform.position);// + Vector3.down * _object_1.GetComponent<RectTransform>().sizeDelta.x / 200);
			lines[squares.Count - 2].SetPosition(1, _object_2.transform.position);// + Vector3.down * _object_2.GetComponent<RectTransform>().sizeDelta.x / 200);
		}
		else if (_object_1.transform.position.y == _object_2.transform.position.y)
		{
			lines[squares.Count - 2].SetPosition(0, _object_1.transform.position);// + Vector3.left * _object_1.GetComponent<RectTransform>().sizeDelta.x / 200);
			lines[squares.Count - 2].SetPosition(1, _object_2.transform.position);// + Vector3.left * _object_2.GetComponent<RectTransform>().sizeDelta.x / 200);
		}

		lines[squares.Count - 2].SetWidth(_object_1.GetComponent<RectTransform>().sizeDelta.x / 100, _object_1.GetComponent<RectTransform>().sizeDelta.y / 100);
		lines[squares.Count - 2].SetColors(squares[0].GetComponent<FlowEnd>().GetSphere.GetComponent<MeshRenderer>().material.color, squares[0].GetComponent<FlowEnd>().GetSphere.GetComponent<MeshRenderer>().material.color);
	}
}