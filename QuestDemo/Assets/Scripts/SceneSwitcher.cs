using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuestDemo
{
    /// <summary>
    /// Loads scenes by name. Hook the public methods up to Building Block UI
    /// buttons (Interaction SDK PokeInteractable / RayInteractable) via the
    /// button's OnClick / WhenSelect event in the Inspector.
    ///
    /// Both scene names below must be added to
    /// File > Build Profiles > Scene List.
    /// </summary>
    public class SceneSwitcher : MonoBehaviour
    {
        [Tooltip("Name of the VR (no passthrough) scene, exactly as in Build Profiles.")]
        [SerializeField] private string vrSceneName = "VR";

        [Tooltip("Name of the Passthrough (MR) scene, exactly as in Build Profiles.")]
        [SerializeField] private string passthroughSceneName = "Passthrough";

        public void LoadVRScene()
        {
            SceneManager.LoadScene(vrSceneName);
        }

        public void LoadPassthroughScene()
        {
            SceneManager.LoadScene(passthroughSceneName);
        }

        /// <summary>Loads whichever of the two scenes is not currently active.</summary>
        public void ToggleScene()
        {
            string active = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(active == vrSceneName ? passthroughSceneName : vrSceneName);
        }
    }
}
