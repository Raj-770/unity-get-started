# Solution Guide — building the two scenes step by step

This is the **detailed walkthrough** for the `solution` branch. Follow it exactly and you will end
up with the two finished scenes (`VR` and `Passthrough`) that the top-level [`README`](README.md)
describes, plus a button that switches between them in the headset.

Every step lists the **exact menu path**, the **GameObject names** to use, and the **values** to set.
Names matter: the `SceneSwitcher` script looks for scenes called **`VR`** and **`Passthrough`**.

> You are on the `solution` branch. Build the scenes here and commit them (last section). Keep `main`
> as the empty starting point.

---

## 0. Before you start

1. Open the `QuestDemo` project in Unity 6 (`6000.3.5f2`).
2. Wait for the Meta XR SDK to finish importing. If the **Meta XR — Project Setup Tool** window pops
   up, click **Fix All**, then **Apply All**.
3. Switch the platform to Android once: **File → Build Profiles → Android → Switch Platform**.
4. Create a folder **`Assets/Scenes`** if it does not exist (right-click in the Project window →
   **Create → Folder**).

You will use two windows constantly — keep them open:
- **Building Blocks**: **Meta → Tools → Building Blocks**
- The **Hierarchy** and **Inspector**.

---

## 1. Scene A — `VR`

### 1.1 Create the scene
1. **File → New Scene → Basic (Built-in)** → **Create**.
2. **File → Save As** → into `Assets/Scenes/`, name it exactly **`VR`**.

### 1.2 Add the Camera Rig
1. In the **Building Blocks** window, find **Camera Rig** and **drag it into the Scene view** (or
   click the **+** on the block).
2. It creates `[BuildingBlock] Camera Rig` (an `OVRCameraRig`). If Unity asks to remove the default
   **Main Camera**, accept — the rig provides its own cameras (`CenterEyeAnchor` is the eye camera).

### 1.3 Add tracking + interactors
Drag these Building Blocks into the scene (each parents itself under the rig automatically):
1. **Hand Tracking** — shows tracked hands.
2. **Controller Tracking** — shows controllers.
3. **Interaction** (from the Interaction SDK section) — this adds the `OVRInteraction` rig that hosts
   poke/ray interactors. If you also see **Interaction - Hand Tracking** and
   **Interaction - Controller Tracking** blocks, add those too; they attach poke/ray interactors to
   your hands and controllers.

> If you can't find the interaction blocks, you can skip them here — step **1.6** can add the poke
> interactor(s) for you with one click.

### 1.4 Add a simple virtual environment (so this scene clearly is *not* passthrough)
1. **GameObject → 3D Object → Plane**, rename to `Floor`, set **Transform → Position** `(0, 0, 0)`,
   **Scale** `(2, 1, 2)`.
2. (Optional) Leave the default skybox — that blue gradient is what makes it read as "VR".

### 1.5 Build the UI panel (a Button + a Slider)
1. **GameObject → UI → Canvas**. Rename it `UIPanel`.
   - On the `Canvas` component set **Render Mode = World Space**.
   - On its **Rect Transform** set:
     - **Pos X/Y/Z** = `0 / 1.3 / 1.5` (≈1.5 m in front of you, at eye height)
     - **Width / Height** = `600 / 400`
     - **Scale X/Y/Z** = `0.001 / 0.001 / 0.001` (so 600 px ≈ 0.6 m — a comfortable panel)
2. Creating the Canvas also created an **EventSystem** object — keep it.
3. Add a button: select `UIPanel` → **GameObject → UI → Button - TextMeshPro** (import TMP Essentials
   if prompted). Rename it `SwitchButton`. Set its child **Text** to `Go to Passthrough`.
4. Add a slider: select `UIPanel` → **GameObject → UI → Slider**. Rename it `DemoSlider`. Position it
   below the button (e.g. Rect Transform **Pos Y** = `-100`). This slider does nothing functional —
   it's there to show a working interactable control.

### 1.6 Make the panel interactable (poke)
1. In the **Hierarchy**, select the `UIPanel` Canvas.
2. **GameObject → Interaction SDK → Add Poke Interaction to Canvas** (also available via right-click
   on the canvas → **Interaction SDK → Add Poke Interaction to Canvas**).
3. In the wizard that appears, tick **Add Required Interactor(s)** so it adds **poke interactors** to
   your hands/controllers on the rig. Click **Create / Apply**.
   - This adds a `PointableCanvas` + `PokeInteractable` to the panel and a `PointableCanvasModule`
     to the EventSystem, wiring Interaction SDK pokes to standard Unity UI events.
4. (Optional) Repeat with **Add Ray Interaction to Canvas** if you also want to point at the panel
   from a distance.

### 1.7 Add the scene switcher and wire the button
1. **GameObject → Create Empty**, rename it `SceneController`.
2. With `SceneController` selected, in the Inspector click **Add Component** → search **Scene
   Switcher** → add it. Confirm the fields read **VR Scene Name = `VR`** and **Passthrough Scene
   Name = `Passthrough`**.
3. Select `SwitchButton`. In its **Button** component find **On Click ()** → click **+**.
   - Drag the `SceneController` object into the object slot.
   - In the function dropdown choose **SceneSwitcher → LoadPassthroughScene ()**.

### 1.8 Sanity check + save
1. Press **Play**. With **Meta Quest Link** running (headset connected) or the **Meta XR Simulator**
   you should see hands/controllers and be able to poke the button (it will try to load the
   `Passthrough` scene — that's fine once it exists). Stop Play.
2. **File → Save** (saves `Assets/Scenes/VR.unity`).

---

## 2. Scene B — `Passthrough`

The fastest correct way is to copy the VR scene, then swap the background for passthrough.

### 2.1 Duplicate the VR scene
1. In the **Project** window select `Assets/Scenes/VR.unity`.
2. **Edit → Duplicate** (or `Ctrl/Cmd+D`). Rename the copy to exactly **`Passthrough`**.
3. Double-click `Passthrough.unity` to open it.

### 2.2 Add the Passthrough building block
1. Open **Meta → Tools → Building Blocks**.
2. Drag **Passthrough** into the scene. This:
   - adds an `OVRPassthroughLayer` (placed as an **Underlay**), and
   - enables Passthrough support in the project (`OVRManager` / OVR project config).

### 2.3 Show the real world through the camera
1. In the Hierarchy expand the rig: `[BuildingBlock] Camera Rig → TrackingSpace → CenterEyeAnchor`.
2. Select **CenterEyeAnchor** (it has a `Camera` component). Set:
   - **Clear Flags = Solid Color**
   - **Background** color → set **Alpha (A) = 0** (fully transparent). Leave RGB as-is.
3. Remove the virtual environment so you see your room:
   - Delete the `Floor` object.
   - **Window → Rendering → Lighting → Environment** → set **Skybox Material = None** (or just leave
     it; with the camera transparent + passthrough underlay you'll see the room regardless).

### 2.4 Re-point the switch button back to VR
1. Select the `SwitchButton` (under `UIPanel`).
2. Change its child **Text** to `Go to VR`.
3. In **On Click ()**, change the function from `LoadPassthroughScene` to
   **SceneSwitcher → LoadVRScene ()** (object stays `SceneController`).

### 2.5 Save
**File → Save** (saves `Assets/Scenes/Passthrough.unity`).

---

## 3. Register both scenes

1. **File → Build Profiles**.
2. In the **Scene List**, click **Add Open Scenes**, then make sure **both** `VR` and `Passthrough`
   are listed and ticked.
3. **Drag `VR` to the top so it is index 0** — index 0 is the scene that launches first.

---

## 4. Build & run on the headset

1. Connect the Quest 3 by USB-C (Developer Mode is already on).
2. In **Build Profiles**, set **Run Device** to your headset (use the refresh icon if it's missing).
3. Click **Build And Run**. Unity builds the APK, installs it, and launches it.
4. In the headset: you start in **VR** (virtual floor + skybox). Poke **Go to Passthrough** → you're
   now in the **Passthrough** scene, same panel floating in your real room. Poke **Go to VR** to
   switch back. Drag the **slider** to confirm interactable UI works.

> Later, the app lives at **App Library → Unknown Sources → QuestDemo**.

---

## 5. Commit the solution

From the repo root (still on the `solution` branch):

```bash
git status                       # should show Assets/Scenes/VR.unity, Passthrough.unity, their .meta,
                                 # ProjectSettings/EditorBuildSettings.asset, and a few OVR config assets
git add -A
git commit -m "Add solved VR and Passthrough scenes"
```

That's the finished solution. `main` stays the clean starting point; `solution` now has the working
scenes to compare against.

---

## Troubleshooting (solution-specific)

- **Poking the button does nothing** → the EventSystem must have a `PointableCanvasModule` and the
  canvas a `PointableCanvas` + `PokeInteractable`. Re-run **GameObject → Interaction SDK → Add Poke
  Interaction to Canvas** with **Add Required Interactor(s)** ticked. Also confirm the canvas has a
  `GraphicRaycaster`.
- **Passthrough scene is black, not your room** → CenterEyeAnchor camera **Clear Flags = Solid Color**
  with **Alpha = 0**, the **Passthrough** block is present, and Passthrough is enabled in the Oculus
  settings (Project Setup Tool → **Fix All**).
- **Switch button loads the wrong/empty scene** → both scenes must be in the Build Profiles Scene List
  and the names must be exactly `VR` and `Passthrough` (matching the `SceneSwitcher` fields).
- **UI panel is huge or tiny** → it's the world-space Canvas scale; `0.001` on all axes is the sweet
  spot for a ~0.6 m panel.
