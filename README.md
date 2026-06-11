# Meta Quest 3 + Unity 6 — Getting Started

A short, hands-on tutorial for building your first **Meta Quest 3** app with **Unity 6**,
using Meta's **Building Blocks** (drag-and-drop XR components). You will build a tiny app with
**two scenes** and switch between them at runtime:

| Scene | What it shows |
| --- | --- |
| **VR** | A fully virtual world (skybox + environment). Camera Rig, controller/hand tracking, and a UI panel with **buttons and a slider** you interact with by poking or pointing. |
| **Passthrough** | The *same* UI Building Blocks, but with **Passthrough** turned on so the panel floats in your real room (mixed reality). |

The goal is to learn the workflow, not to build something big — most of the work is dragging
Building Blocks into a scene and pressing **Build And Run**.

> The companion Unity project lives in [`QuestDemo/`](QuestDemo). Open that folder in Unity — the
> Meta XR SDK installs automatically from the Unity package registry (see
> [How the project is wired](#how-the-project-is-wired)).
>
> **Branches:** `main` is the starting point (configured project + this tutorial). The
> **`solution`** branch contains the finished `VR` and `Passthrough` scenes if you want to compare.

---

## Prerequisites

- **Unity 6** (this project was built with `6000.3.5f2`) installed via **Unity Hub**.
  When installing, tick the **Android Build Support** module (with **OpenJDK** and **Android SDK & NDK Tools**).
- A **Meta Quest 3** (or 3S) headset, in **Developer Mode**, connected with a **USB-C cable**.

---

## 1. Open the project

1. In **Unity Hub → Projects → Add → Add project from disk**, select the `QuestDemo` folder.
2. Open it with Unity 6. On first open Unity downloads the **Meta XR SDK** packages from the Unity
   package registry — this can take a few minutes. Accept any "Restart Editor" / API-update prompts.
3. If a **Meta XR — Project Setup Tool** window appears with warnings, click **Fix All** and
   **Apply All**. (You can reopen it any time via **Meta → Tools → Project Setup Tool**.) This
   configures the Android player settings, color space, XR plug-in, etc. for Quest in one click.

---

## 2. Configure the build target for Quest

**File → Build Profiles** (Unity 6) → select **Android** → **Switch Platform**. Then, still in
Build Profiles, confirm under **Player Settings**:

- **Other Settings → Minimum API Level**: Android 12L (API 32) or higher.
- **Other Settings → Scripting Backend**: IL2CPP, **Target Architectures**: **ARM64** only.
- **XR Plug-in Management → Android tab**: **Oculus** is enabled.

(The Project Setup Tool from step 1 normally sets all of these for you.)

---

## 3. Build the **VR** scene with Building Blocks

Building Blocks are pre-wired prefabs you drag into a scene. Open the window via
**Meta → Tools → Building Blocks**.

1. **Create the scene**: **File → New Scene → Basic (Built-in)**, then **Save As** `VR` into
   `Assets/Scenes/`.
2. From the **Building Blocks** window, drag these into the scene (each one auto-configures itself):
   - **Camera Rig** — the player's head + hands rig (replaces the default Main Camera; delete the
     default camera if asked).
   - **Controller Tracking** and/or **Hand Tracking** — so your controllers/hands appear.
   - **Controller Buttons Mapper** *(optional)* — quick button input.
3. Add interactable **UI**:
   - Drag the **Interaction → Poke** (and **Ray**) building blocks so you can touch/point at UI.
   - Add a world-space **Canvas** with a couple of **Buttons** and a **Slider**
     (`GameObject → UI → ...`; set the Canvas **Render Mode = World Space** and scale it down to
     ~0.001). Meta's Interaction SDK samples include ready-made poke-able button/slider prefabs —
     import them from **Package Manager → Meta XR Interaction SDK → Samples** if you want a shortcut.
4. Add scene switching:
   - Create an empty GameObject `SceneController` and add the **`SceneSwitcher`** script
     (`Assets/Scripts/SceneSwitcher.cs`).
   - On one button's **OnClick**, call `SceneSwitcher → LoadPassthroughScene()`.
5. **Save** the scene.

Press **Play** in the Editor (with Quest Link, or the **Meta XR Simulator**) to sanity-check.

---

## 4. Build the **Passthrough** scene

1. **Duplicate** the `VR` scene: in the Project window, copy `VR.unity` → rename to
   `Passthrough.unity`, and open it.
2. From **Building Blocks**, drag in the **Passthrough** block. This adds an `OVRPassthroughLayer`
   and enables Passthrough support on the Camera Rig.
3. Make the background show the real world:
   - Select the Camera Rig's center/eye camera → set **Clear Flags = Solid Color** and the color's
     **alpha to 0** (transparent), so passthrough shows through.
   - Remove/disable the skybox or virtual environment you used in the VR scene.
4. Point this scene's switch button back to the VR scene: on a button's **OnClick**, call
   `SceneSwitcher → LoadVRScene()`.
5. **Save** the scene.

---

## 5. Register both scenes & build to the headset

1. **File → Build Profiles → Scene List → Add Open Scenes** so both `VR` and `Passthrough` are
   listed. Put **VR** first (index 0 = the scene that loads on launch).
2. Make sure the headset is connected and shows up: in **Build Profiles**, the **Run Device**
   dropdown should list your Quest.
3. Click **Build And Run**. Unity compiles an APK, installs it, and launches it on the headset.
4. Put the headset on. You start in the **VR** scene; poke the **Switch** button to jump to
   **Passthrough** and see the same UI in your real room. The **slider** and **buttons** work via
   poke/ray in both scenes.

To find the app later on the headset: **App Library → Unknown Sources → `QuestDemo`**.

---

## How the project is wired

- **Meta XR SDK from the Unity registry.** `QuestDemo/Packages/manifest.json` depends on
  **`com.meta.xr.sdk.core`** (Camera Rig, Passthrough, Building Blocks) and
  **`com.meta.xr.sdk.interaction.ovr`** (poke/ray interaction + UI interactables), pinned to
  **v77.0.0**. These resolve from Unity's default package registry, so the SDK installs reproducibly
  on `git clone` — no manual Asset Store import or extra registry setup needed. (To move to a newer
  SDK, bump the version in `manifest.json`; Unity downloads it on next open.)
- **`Assets/Scripts/SceneSwitcher.cs`** — tiny `MonoBehaviour` exposing `LoadVRScene()`,
  `LoadPassthroughScene()`, and `ToggleScene()` to wire onto Building Block buttons.

---

## Troubleshooting

- **Headset not in the Run Device list** → reconnect USB-C and put the headset on. Confirm with
  `adb devices` (adb ships with Android Build Support).
- **Passthrough shows black** → the eye camera's background must be transparent (Solid Color, alpha 0),
  the **Passthrough** building block must be present, and **Passthrough** must be enabled in the
  Oculus XR settings (the Project Setup Tool handles the last one).
- **Pink/magenta materials** → wrong render pipeline/color space; rerun **Project Setup Tool → Fix All**.
- **App installs but is a black screen** → the **VR** scene isn't at index 0 in the Scene List, or
  the Camera Rig is missing from that scene.
- **Package errors on open** → check your internet connection, then **Edit → Project Settings →
  Package Manager** and re-resolve, or delete `Library/` and reopen.

---

## Useful links

- Meta — Set up Unity development: <https://developers.meta.com/horizon/documentation/unity/unity-gs-overview/>
- Meta — Building Blocks: <https://developers.meta.com/horizon/documentation/unity/bb-overview/>
- Meta — Passthrough in Unity: <https://developers.meta.com/horizon/documentation/unity/unity-passthrough/>
- Unity — XR / Meta Quest: <https://docs.unity3d.com/Manual/xr-meta.html>
