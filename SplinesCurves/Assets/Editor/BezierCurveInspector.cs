using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor {
    private BezierCurve curve;

    // Handle properties
    private Transform handleTransform;
    private Quaternion handleRotation;

    // Steps to draw on the curve
    private const int lineSteps = 20;

    // Drawing
    private void OnSceneGUI() {
        curve = target as BezierCurve;
        handleTransform = curve.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local
            ? handleTransform.rotation
            : Quaternion.identity;

        // Draw point handles
        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);
        Vector3 p3 = ShowPoint(3);

        // Draw points
        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p2, p3);

        // Draw bezier curve
        Handles.color = Color.white;
        Vector3 lineStart = curve.GetPoint(0f);
        for (int i = 1; i <= lineSteps; i++) {
            Vector3 lineEnd = curve.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(lineStart, lineEnd);
            lineStart = lineEnd;
        }

        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + curve.GetDirection(0f));
        for (int i = 1; i <= lineSteps; i++) {
            Vector3 lineEnd = curve.GetPoint(i / (float)lineSteps);
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);
            Handles.color = Color.green;
            Handles.DrawLine(lineEnd, lineEnd + curve.GetDirection(i / (float)lineSteps));
            lineStart = lineEnd;
        }
    }

    private Vector3 ShowPoint(int index) {
        Vector3 point = handleTransform.TransformPoint(curve.points[index]);

        // Check for handle changes
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(curve, "Move Point");
            EditorUtility.SetDirty(curve);
            curve.points[index] = handleTransform.InverseTransformPoint(point);
        }

        return point;
    }
}
