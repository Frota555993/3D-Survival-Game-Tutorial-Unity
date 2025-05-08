using UnityEngine;
using UnityEngine.UI;

public class UICursorManager : MonoBehaviour
{
    [Header("Referências UI")]
    public Canvas uiCanvas;              // Arraste aqui seu Canvas (Screen Space – Overlay ou Camera)
    public Image cursorImage;            // Arraste aqui o Image UICursor dentro do Canvas

    [Header("Sprites de Cursor")]
    public Sprite defaultCursorSprite;
    public Sprite hoverCursorSprite;

    [Header("Offset do Hotspot")]
    public Vector2 hotspotOffset = Vector2.zero;

    [Header("Raycast Settings")]
    public LayerMask interactableMask;
    public float rayRange = 100f;

    private Camera cam;
    private RectTransform canvasRect;

    void Awake()
    {
        cam = Camera.main;
        Cursor.visible = false;

        if (uiCanvas == null)
            Debug.LogError("UI Canvas não definido em UICursorManager!");

        canvasRect = uiCanvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        // 1) Converter posição de tela para ponto local no RectTransform do Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            uiCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : uiCanvas.worldCamera,
            out Vector2 localPoint
        );

        // 2) Aplicar ponto local + offset ao cursor UI
        cursorImage.rectTransform.anchoredPosition = localPoint + hotspotOffset;

        // 3) Raycast para objetos interagíveis
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, rayRange, interactableMask))
            cursorImage.sprite = hoverCursorSprite;
        else
            cursorImage.sprite = defaultCursorSprite;
    }
}
