using UnityEngine.EventSystems;

namespace UnityEngine.UI
{

    [RequireComponent(typeof(Canvas))]
    [ExecuteInEditMode]
    [AddComponentMenu("Layout/Reference Resolution", 101)]
    public class ReferenceResolution : UIBehaviour
    {
        [SerializeField]
        protected Vector2 m_Resolution = new Vector2(800, 600);
        public Vector2 resolution { get { return m_Resolution; } set { m_Resolution = value; } }

        [Range(0, 1)]
        [SerializeField]
        protected float m_MatchWidthOrHeight = 0;
        public float matchWidthOrHeight { get { return m_MatchWidthOrHeight; } set { m_MatchWidthOrHeight = value; } }

        private Canvas m_Canvas;
        private Vector2 m_PrevScreenSize = Vector2.zero;

        // The log base doesn't have any influence on the results whatsoever, as long as the same base is used everywhere.
        private const float kLogBase = 2;

        protected ReferenceResolution()
        { }

        protected override void OnEnable()
        {
            m_Canvas = GetComponent<Canvas>();
            SetScaleFactor();
        }

        protected virtual void Update()
        {
            SetScaleFactor();
        }

        protected virtual void SetScaleFactor()
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            if (m_Canvas == null || screenSize == m_PrevScreenSize)
                return;

            m_PrevScreenSize = screenSize;

            // We take the log of the relative width and height before taking the average.
            // Then we transform it back in the original space.
            // the reason to transform in and out of logarithmic space is to have better behavior.
            // If one axis has twice resolution and the other has half, it should even out if widthOrHeight value is at 0.5.
            // In normal space the average would be (0.5 + 2) / 2 = 1.25
            // In logarithmic space the average is (-1 + 1) / 2 = 0
            float logWidth = Mathf.Log(screenSize.x / m_Resolution.x, kLogBase);
            float logHeight = Mathf.Log(screenSize.y / m_Resolution.y, kLogBase);
            float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, m_MatchWidthOrHeight);
            m_Canvas.scaleFactor = Mathf.Pow(kLogBase, logWeightedAverage);
        }
    }
}