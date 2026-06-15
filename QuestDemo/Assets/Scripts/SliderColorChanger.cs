using UnityEngine;

namespace QuestDemo
{
    /// <summary>
    /// Drives a Renderer's color from a single 0..1 value — wire a UI Slider's
    /// <c>On Value Changed (Single)</c> event to <see cref="SetHue"/> (dynamic float).
    ///
    /// The 0..1 value is mapped to hue, so dragging the slider sweeps the cube
    /// through the full color spectrum. Works with both the Built-in (<c>_Color</c>)
    /// and URP/HDRP (<c>_BaseColor</c>) lit shaders, and uses a MaterialPropertyBlock
    /// so it doesn't create leaked material instances.
    /// </summary>
    public class SliderColorChanger : MonoBehaviour
    {
        [Tooltip("The object whose color changes. Leave empty to use the Renderer on this GameObject.")]
        [SerializeField] private Renderer targetRenderer;

        [Tooltip("Color saturation of the result.")]
        [SerializeField, Range(0f, 1f)] private float saturation = 1f;

        [Tooltip("Color brightness (value) of the result.")]
        [SerializeField, Range(0f, 1f)] private float brightness = 1f;

        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor"); // URP / HDRP
        private static readonly int ColorId = Shader.PropertyToID("_Color");         // Built-in
        private MaterialPropertyBlock _block;

        private void Awake()
        {
            if (targetRenderer == null) targetRenderer = GetComponent<Renderer>();
            _block = new MaterialPropertyBlock();
        }

        /// <summary>Hook this to Slider.onValueChanged. <paramref name="t"/> is expected in 0..1.</summary>
        public void SetHue(float t)
        {
            ApplyColor(Color.HSVToRGB(Mathf.Clamp01(t), saturation, brightness));
        }

        /// <summary>Set an explicit color directly (handy for buttons / testing).</summary>
        public void SetColor(Color color)
        {
            ApplyColor(color);
        }

        private void ApplyColor(Color color)
        {
            if (targetRenderer == null) return;
            targetRenderer.GetPropertyBlock(_block);
            _block.SetColor(BaseColorId, color);
            _block.SetColor(ColorId, color);
            targetRenderer.SetPropertyBlock(_block);
        }
    }
}
