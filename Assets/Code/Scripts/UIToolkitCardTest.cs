using UnityEngine;
using UnityEngine.UIElements;

public class UIToolkitCardTest : MonoBehaviour
{
    private UIDocument _uiDocument;

    private VisualElement _root;

    private VisualElement _handZone;
    private VisualElement _useZone;
    private VisualElement _mouseFollower;

    private VisualElement _testCard;

    private float _rotation;

    private Vector3 _baseScale;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        _root = _uiDocument.rootVisualElement;

        // 名前で取得（UXML側でnameを設定しておく）
        _handZone = _root.Q<VisualElement>("HandZone");
        _useZone = _root.Q<VisualElement>("UseZone");
        _mouseFollower = _root.Q<VisualElement>("MouseFollower");

        _testCard = _root.Q<VisualElement>("TestCard");

        _baseScale = _testCard.style.scale.value.value;
    }

    void Update()
    {
        // キー入力で挙動確認

        // 1: Hand → MouseFollower
        if (Input.GetMouseButtonDown(0))
        {
            MoveToMouseFollower();
        }

        // 2: MouseFollower → UseZone
        if (Input.GetMouseButtonUp(0))
        {
            MoveToUseZone();
        }

        // 回転
        if (Input.GetKey(KeyCode.R))
        {
            RotateCard();
        }

        // マウス追従
        FollowMouse();
    }

    void MoveToMouseFollower()
    {
        _testCard.RemoveFromHierarchy();
        _mouseFollower.Add(_testCard);
    }

    void MoveToUseZone()
    {
        _testCard.RemoveFromHierarchy();
        _useZone.Add(_testCard);
    }

    void RotateCard()
    {
        _rotation += 1f * Time.deltaTime;

        var s = Mathf.Sin(_rotation);


        _testCard.style.scale = new Vector3(_baseScale.x * s, _baseScale.y, _baseScale.z);
    }

    void FollowMouse()
    {
        Vector2 mouse = Input.mousePosition;

        // UI Toolkitは左下原点なので変換
        mouse.y = Screen.height - mouse.y;

        _mouseFollower.style.left = mouse.x;
        _mouseFollower.style.top = mouse.y;
    }
}