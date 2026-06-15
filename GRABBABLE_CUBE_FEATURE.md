# Feature: a grabbable cube whose color the slider controls

This adds a small interactive feature on top of the scenes from
[`SOLUTION_GUIDE.md`](SOLUTION_GUIDE.md):

- a **cube you can pick up** with your hands/controllers (Meta Interaction SDK grab), and
- the **slider** on the UI panel now **changes the cube's color** (sweeping through the full
  color spectrum as you drag it).

Do this in the **`VR`** scene first (it's easier to see against the virtual floor); you can repeat
the exact same steps in the `Passthrough` scene.

> The color logic is provided in [`Assets/Scripts/SliderColorChanger.cs`](QuestDemo/Assets/Scripts/SliderColorChanger.cs).
> It maps a slider value (0..1) to hue and writes it via a `MaterialPropertyBlock`, so it works with
> both the Built-in and URP lit shaders and creates no leaked material instances.

---

## 1. Add the cube

1. **GameObject → 3D Object → Cube**. Rename it `ColorCube`.
2. Set its **Transform**:
   - **Position** `0, 1.1, 0.6` (within arm's reach, in front of you, below the UI panel)
   - **Scale** `0.15, 0.15, 0.15` (a ~15 cm cube — comfortable to grab)
3. (Optional) Create a material so it starts with a clean color: **right-click in Project →
   Create → Material**, name it `CubeMat`, drag it onto `ColorCube`. The script overrides the color
   at runtime regardless, but a material makes the editor preview nicer.

## 2. Make the cube grabbable

1. Select `ColorCube` in the Hierarchy.
2. **GameObject → Interaction SDK → Add Grab Interaction** (also via right-click on the object →
   **Interaction SDK → Add Grab Interaction**).
3. In the wizard:
   - Tick **Add Required Interactor(s)** so grab interactors are added to your hands/controllers on
     the Camera Rig (skip if you already added them for another grabbable).
   - Click **Create / Apply**.
   This adds a `Rigidbody`, a `Grabbable`, and hand/controller `GrabInteractable` components so the
   cube can be picked up.
4. On the cube's **Rigidbody**, tick **Is Kinematic** if you don't want it to fall to the floor
   before you grab it (or leave it off and let it drop onto the `Floor` — your choice).

> Tip: there's also a **Grab Interaction** + **Cube** building block in **Meta → Tools → Building
> Blocks** if you prefer dragging a ready-made grabbable in. The manual steps above give you a cube
> you fully control.

## 3. Hook the slider up to the cube's color

1. Add the color script to the cube: select `ColorCube` → **Add Component** → search
   **Slider Color Changer** → add it.
2. On the `SliderColorChanger` component, set **Target Renderer** to the cube itself
   (drag `ColorCube` into the field, or leave it empty — it falls back to the Renderer on the same
   GameObject). Optionally tweak **Saturation** / **Brightness**.
3. Select the **`DemoSlider`** on your `UIPanel`. In its **Slider** component find
   **On Value Changed (Single)** → click **+**.
   - Drag `ColorCube` into the object slot.
   - In the function dropdown choose **SliderColorChanger → SetHue** under the **Dynamic Float**
     section (this passes the slider's live 0..1 value — do **not** pick the static-float version).
4. (Optional) Set the slider's **Min Value = 0** and **Max Value = 1** (these are the defaults) so
   the full hue range is reachable.

## 4. Test

1. Press **Play** (with Meta Quest Link or the Meta XR Simulator), or **Build And Run** to the
   headset.
2. **Grab** the cube with a hand/controller and move it around.
3. **Drag the slider** — the cube cycles through the color spectrum in real time.

## 5. Repeat in the Passthrough scene (optional)

Open `Passthrough.unity` and repeat sections **1–3** so the grabbable color cube also appears in
mixed reality. (Or copy the `ColorCube` object from the VR scene and re-link the slider event in the
Passthrough scene, since cross-scene event references don't persist.)

## 6. Commit

```bash
git add Assets/Scenes QuestDemo/Assets/Scripts/SliderColorChanger.cs* ProjectSettings/EditorBuildSettings.asset
git commit -m "Add grabbable color cube feature"
```

---

## Troubleshooting

- **Slider doesn't change the color** → make sure you picked **SetHue** under **Dynamic Float**
  (not the static value), and that the cube has a `Renderer` (a default cube does).
- **Color doesn't change but no error** → confirm `Target Renderer` points at the cube. If you put
  `SliderColorChanger` on a different object, the empty fallback won't find the cube's Renderer.
- **Can't grab the cube** → it needs the grab components from step 2 *and* grab interactors on your
  hands/controllers (the **Add Required Interactor(s)** checkbox). Also ensure the cube has a
  `Collider` (the default cube does) and a `Rigidbody`.
- **Cube falls through the floor / flies away** → set the `Rigidbody` to **Is Kinematic**, or make
  sure the `Floor` has a collider.
