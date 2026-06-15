# 4. Tools — Meta Quest Developer Hub & Meta XR Simulator

Two Meta tools make the build/test loop much smoother. Use them throughout the tutorial:

- **Meta Quest Developer Hub (MQDH)** — a desktop companion app to connect your headset, install
  apps, and read gameplay/device logs.
- **Meta XR Simulator** — a virtual Quest that runs on your PC/Mac, for rapid iteration **without
  putting the headset on**.

You don't strictly need either (Unity's **Build And Run** can deploy directly), but together they
remove most of the friction in day-to-day development.

---

## Meta Quest Developer Hub (MQDH)

MQDH is a companion app for **Windows and macOS** that manages your Quest devices, deploys builds,
reads logs, and captures screenshots/video.

### Install & connect
1. Download and install MQDH:
   <https://developers.meta.com/horizon/documentation/unity/ts-mqdh-getting-started/>
2. Open it and **log in** with your Meta developer account.
3. Connect the Quest 3 by **USB-C**, put the headset on, and **Allow USB debugging** when prompted
   (tick *Always allow from this computer*). The device then appears in MQDH as connected.
   - MQDH can also set up **ADB over Wi-Fi**, so you can deploy wirelessly after the first USB pairing.

### Putting your app on the headset
- Build an APK in Unity: **File → Build Profiles → Build** (not Build And Run) and save the `.apk`.
- In MQDH, open the **Device Manager / Apps** panel and **drag-and-drop the `.apk`** onto your device
  (or use **Add Build → Install**). It installs in seconds.
- The app appears in the headset under **App Library → Unknown Sources → QuestDemo**.

### Viewing gameplay / debug logs
- MQDH has a **logs / logcat** panel that streams the device log live — this is where your
  `Debug.Log` output and any runtime errors/crashes show up. Filter by your app to watch what your
  scene is doing while you wear the headset.
- It can also **cast** the headset view to your screen and **record** screenshots/video for sharing.

> MQDH replaces fiddling with raw `adb` on the command line — device pairing, install, and logs are
> all in one UI.

## Meta XR Simulator

The Meta XR Simulator is a lightweight **OpenXR runtime** that emulates a Quest headset on your
computer (Windows, and macOS on **Apple Silicon**). It lets you press **Play** in the Editor and test
in a simulated headset — no device, no putting the headset on and off — which is ideal for rapid
iteration on UI and interactions like our switch button, slider, and grab.

### Activate it
1. The Simulator ships with the Meta XR SDK that's already in this project.
2. In Unity, go to **Meta → Meta XR Simulator → Activate** (this makes the Simulator the active
   OpenXR runtime for Play mode). Use **Deactivate** to switch back to your real headset / Link.
3. Press **Play**. A Simulator window opens showing your scene as a virtual Quest would render it.

### Simulating input
- Open the **Input Simulation** tab in the Simulator window to drive the headset and hands/controllers
  with **mouse and keyboard**.
- It can simulate hand gestures (aim, poke, pinch, grab) — enough to test poking the UI button,
  dragging the slider, and grabbing the color cube from [Feature 2](03-grabbable-color-cube.md).

### When to use which
| Situation | Use |
| --- | --- |
| Fast iteration on UI / logic, no headset handy | **Meta XR Simulator** |
| Final look-and-feel, real passthrough, performance | **Headset** (deploy via MQDH or Build And Run) |
| Reading runtime logs / diagnosing a crash on device | **MQDH** logs panel |

---

## Reference links

- Meta Quest Developer Hub — get started: <https://developers.meta.com/horizon/documentation/unity/ts-mqdh-getting-started/>
- Meta Quest Developer Hub — overview: <https://developers.meta.com/horizon/documentation/unity/ts-mqdh/>
- Meta XR Simulator — get started: <https://developers.meta.com/horizon/documentation/unity/xrsim-getting-started/>
- Meta XR Simulator — overview: <https://developers.meta.com/horizon/documentation/unity/xrsim-intro/>
- Set up your headset for development: <https://developers.meta.com/horizon/documentation/unity/unity-env-device-setup/>
