using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] private Texture2D defaultCursorImg;
    [SerializeField] private Texture2D glovedCursorImg;
    private Vector2 hotspot = new Vector2(10, 10);

    // Start is called before the first frame update
    void Start()
    {
        ResetCursor();
    }

    // Reset to default if needed
    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursorImg, hotspot, CursorMode.Auto);
    }

    public void SetGlovedCursor()
    {
        Cursor.SetCursor(glovedCursorImg, hotspot, CursorMode.Auto);
    }
}
