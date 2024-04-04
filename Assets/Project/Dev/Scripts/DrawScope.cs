using UnityEngine;

public class DrawScope : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D _circleCollider;
    
    private void Update()
    {
        DrawGizmoCircle();
    }

    private void DrawGizmoCircle()
    {
        if (_circleCollider == null)
            return;

        Vector2 center = transform.TransformPoint(_circleCollider.offset);
        float radius = _circleCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);

        const int segments = 32;
        for (int i = 0; i < segments; i++)
        {
            float angle1 = Mathf.PI * 2 * (i / (float)segments);
            float angle2 = Mathf.PI * 2 * ((i + 1) / (float)segments);
            Vector2 point1 = center + new Vector2(Mathf.Cos(angle1) * radius, Mathf.Sin(angle1) * radius);
            Vector2 point2 = center + new Vector2(Mathf.Cos(angle2) * radius, Mathf.Sin(angle2) * radius);
            Debug.DrawLine(point1, point2, Color.white);
        }
    }
}
