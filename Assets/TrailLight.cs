using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class TrailLight : MonoBehaviour
{
    public HDAdditionalLightData tubeLightReference;
    public int count = 6;
    private int m_count;
    public float length = 0.01f;

    public Color color = Color.white;
    public AnimationCurve intensityFade = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

    private List<HDAdditionalLightData> lights;
    private List<Vector3> points;
    private Quaternion fixRotation = Quaternion.Euler(0f, 90f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        m_count = count;
        lights = new List<HDAdditionalLightData>();
        points = new List<Vector3>();

        for (int i=0; i < m_count; i++)
        {
            var l = Instantiate<GameObject>(tubeLightReference.gameObject).GetComponent<HDAdditionalLightData>();
            l.transform.parent = transform;
            lights.Add(l);
            points.Add(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var sqrLength = length * length;
        for (int i = 0; i < m_count; i++)
        {
            var p0 = (i == 0) ? transform.position : points[i - 1];
            var delta = points[i] - p0;
            var distance = delta.sqrMagnitude;
            if (distance > sqrLength)
            {
                distance = length;
                points[i] = p0 + delta.normalized * length;
            }
            else
            {
                distance = Mathf.Sqrt(distance);
            }

            lights[i].transform.position = 0.5f * (p0 + points[i]);
            if(distance > 0.00001f)
                lights[i].transform.rotation = Quaternion.LookRotation(delta) * fixRotation;
            lights[i].shapeWidth = distance;
            lights[i].color = color;
            lights[i].intensity = tubeLightReference.intensity * intensityFade.Evaluate(i * 1.0f / count);
        }
    }
}
