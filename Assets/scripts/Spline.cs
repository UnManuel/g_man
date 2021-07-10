using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Spline : MonoBehaviour
{
	public CatmullRom spline;

	public Transform[] controlPoints;

	public Color color = Color.white;

	[Range(2, 25)]
	public int resolution;
	public bool closedLoop;

	[Range(0, 20)]
	public float normalExtrusion;

	[Range(0, 20)]
	public float tangentExtrusion;

	public bool drawNormal, drawTangent;

	void Start()
	{
		if(spline == null)
		{
			spline = new CatmullRom(controlPoints, resolution, closedLoop);
		}
	}

	void Update()
	{
		if(spline != null)
		{
			spline.Update(controlPoints);
			spline.Update(resolution, closedLoop);
			spline.DrawSpline(color);

			if(drawNormal)
				spline.DrawNormals(normalExtrusion, Color.red);

			if(drawTangent)
				spline.DrawTangents(tangentExtrusion, Color.cyan);
		}
		else
		{
			spline = new CatmullRom(controlPoints, resolution, closedLoop);
		}
	}
}