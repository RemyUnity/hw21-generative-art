using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorsManager : MonoBehaviour
{
    private static RotatorsManager m_instance;

    public static RotatorsManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<RotatorsManager>();

            if (m_instance == null)
                m_instance = new GameObject("Rotators Manager").AddComponent<RotatorsManager>();

            return m_instance;
        }
    }

    private List<Rotator> m_rotators;
    private List<Rotator> rotators
    {
        get
        {
            if (m_rotators == null)
                m_rotators = new List<Rotator>();

            return m_rotators;
        }
    }


    public void RegisterRotator(Rotator r)
    {
        rotators.Add(r);
    }

    public void UnRegisterRotator(Rotator r)
    {
        rotators.Remove(r);
    }

    private void Update()
    {
        foreach(var r in rotators)
        {
            r.transform.Rotate(Vector3.up, r.speed * Time.deltaTime, Space.Self);
        }
    }
}
