using UnityEngine;
using UnityEngine.UI;

public class MouseCursorLocker : MonoBehaviour
{
    public Image lockImage;
    public bool lockCursor = true;
    public GameObject cursorPrefab;
    private GameObject cursor;
    private RectTransform rectTransform;
    private Rect rect;



    private Vector2 notGoodPos;
    [SerializeField] private int x;
    [SerializeField] private int y;
    private void Start()
    {
        rectTransform = lockImage.GetComponent<RectTransform>();
        Canvas canvas = lockImage.GetComponentInParent<Canvas>();
        Vector2 min = RectTransformUtility.PixelAdjustPoint(rectTransform.rect.min, rectTransform, canvas);
        Vector2 max = RectTransformUtility.PixelAdjustPoint(rectTransform.rect.max, rectTransform, canvas);
        rect = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);

        cursor = Instantiate(cursorPrefab);
        cursor.transform.SetParent(canvas.transform);
        cursor.SetActive(false);
    }

    private void Update()
    {
        if (lockCursor)
        {
            notGoodPos = Input.mousePosition;
            notGoodPos.x = Mathf.Clamp(notGoodPos.x, rect.xMin, rect.xMax);
            notGoodPos.y = Mathf.Clamp(notGoodPos.y, rect.yMin, rect.yMax);

            notGoodPos.x += x;
            notGoodPos.y += y;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursor.transform.position = notGoodPos;
            cursor.SetActive(true);
        }
        else
        {
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursor.SetActive(false);
        }
    }
    
}