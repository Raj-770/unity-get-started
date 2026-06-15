# Meta Quest 3 + Unity 6 — Getting Started

A short, hands-on tutorial for building your first **Meta Quest 3** app with **Unity 6**, using
Meta's **Building Blocks** (drag-and-drop XR components). You'll set up the project, then build two
small features and learn the tools that make development fast.

**What you'll build:**

| | |
| --- | --- |
| **Feature 1 — two scenes + switch button** | A **VR** scene (fully virtual) and a **Passthrough** scene (mixed reality), with a UI **button** that switches between them and a **slider** on the panel. |
| **Feature 2 — grabbable color cube** | A **cube you can pick up**, and the slider now **changes its color** through the full spectrum. |

The goal is to learn the workflow, not to build something big — most of the work is dragging
Building Blocks into a scene and wiring a couple of events.

---

## Follow these in order

1. **[Setup](docs/01-setup.md)** — open the `QuestDemo` project, handle the first-launch prompts, run
   the Project Setup Tool, and confirm the Quest build settings.
2. **[Feature 1 — two scenes & a switch button](docs/02-two-scenes-and-switch.md)** — build the `VR`
   and `Passthrough` scenes and switch between them.
3. **[Feature 2 — grabbable color cube](docs/03-grabbable-color-cube.md)** — add a grabbable cube and
   give the slider a real job.
4. **[Tools — MQDH & XR Simulator](docs/04-mqdh-and-simulator.md)** — deploy to the headset and read
   logs with **Meta Quest Developer Hub**, and test without a headset using the **Meta XR Simulator**.

> Prefer a no-code route? See the alternative [VR Builder setup](docs/alternative-vr-builder.md).

---

## Prerequisites

- **Unity 6** (this project was built with `6000.3.5f2`), installed via **Unity Hub** with the
  **Android Build Support** module (including **OpenJDK** and **Android SDK & NDK Tools**).
- A **Meta Quest 3** (or 3S) headset, in **Developer Mode**, connected with a **USB-C cable**.
  (You can also do most of the tutorial on the **Meta XR Simulator** without a headset.)

## Branches

- **`main`** — the starting point: the configured project + this tutorial. You build the scenes
  yourself by following the docs.
- **`solution`** — the same project with the finished `VR` and `Passthrough` scenes already built, to
  compare against.

## How the project is wired

- **Meta XR SDK from the Unity registry.** `QuestDemo/Packages/manifest.json` depends on
  **`com.meta.xr.sdk.core`** (Camera Rig, Passthrough, Building Blocks) and
  **`com.meta.xr.sdk.interaction.ovr`** (poke/ray interaction + UI interactables), pinned to
  **v77.0.0**. They resolve from Unity's default package registry, so the SDK installs on
  `git clone` — no manual Asset Store import needed.
- **`QuestDemo/Assets/Scripts/SceneSwitcher.cs`** — switches between the two scenes.
- **`QuestDemo/Assets/Scripts/SliderColorChanger.cs`** — maps the slider value to the cube's color.

## Useful links

- Meta — Set up Unity development: <https://developers.meta.com/horizon/documentation/unity/unity-gs-overview/>
- Meta — Building Blocks: <https://developers.meta.com/horizon/documentation/unity/bb-overview/>
- Meta — Passthrough in Unity: <https://developers.meta.com/horizon/documentation/unity/unity-passthrough/>
- Unity — XR / Meta Quest: <https://docs.unity3d.com/Manual/xr-meta.html>
