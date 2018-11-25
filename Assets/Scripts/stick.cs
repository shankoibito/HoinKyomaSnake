using UnityEngine;
using UnityEngine.EventSystems;

public class stick : MonoBehaviour, IDragHandler , IPointerDownHandler , IPointerUpHandler {

    [Range(0.4f, 1f)] public float lockjoypointer = 0.8f;
    protected Vector2 input = Vector2.zero;

    public RectTransform background, handle, ball;
    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal,Vertical); } }

    Vector2 StickPos = Vector2.zero;
    private Camera cam = new Camera();
    public Vector2 help;

    public void Start()
    {
        StickPos = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        handle.anchoredPosition3D = new Vector3(0, 56, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - StickPos;
        if (direction.magnitude > background.sizeDelta.x / 2f)
        {
            input = direction.normalized;
        }
        else {
            input = direction / (background.sizeDelta.x / 2f);
        }
        help = (input * background.sizeDelta.x / 2f) * lockjoypointer;

        /*if (help.magnitude >= 10)
        {
            handle.anchoredPosition = help;
        }*/
        handle.anchoredPosition = help;
        print("Ondrag called");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        PlayerPrefs.SetInt("helpmove",1);
        print("pointdown called");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition3D = new Vector3(0, 56, 0);
        print("ptrup called");
    }
}
