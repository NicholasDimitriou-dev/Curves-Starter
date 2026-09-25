using UnityEngine;

/*
 * QuadraticBezierCurve owns the three control-point Transforms in the Demo
 * scene. This is where their world positions connect to quadratic math,
 * Scene-view gizmos, and the Play Mode line.
 */

public class QuadraticBezierCurve : MonoBehaviour
{
    [Header("Bezier Points")]
    public Transform p0;
    public Transform p1;
    public Transform p2;

    public int numSamples = 10;

    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numSamples;
        for (int i = 0; i < numSamples; i++)
        {
            float t = i / (float)(numSamples -1);
            Vector3 sample = SamplePoint(t);
            lineRenderer.SetPosition(i,sample);
        }
        // Space them evenly in t and include both endpoints.
        // Check: restart Play Mode. The line matches the Scene-view gizmos.
        // Next: Slice 2.1 in FollowCurve.cs. </> end of Slice 1


        // Prepare the coefficients once, outside the sample loop.
        // Check: restart Play Mode. The line still matches the gizmos,
        // and every sample uses power evaluation.
        // Next: Slice 7.4 in Bezier/QuadraticBezierMath.cs.
    }

    void OnDrawGizmos()
    {
        if (p0 == null || p1 == null || p2 == null) return;
        CurveGizmos.Draw(10,SamplePoint,p0,p1,p2);
    }

    public Vector3 SamplePoint(float t)
    {
        
        return QuadraticBezierMath.SamplePointDeCasteljau(p0.position, p1.position, p2.position, t);
        
    }

    public Vector3 SampleTangent(float t)
    {
        
        return QuadraticBezierMath.SampleTangentBernstein(p0.position, p1.position, p2.position, t);
    }
}
