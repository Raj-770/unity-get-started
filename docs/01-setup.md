# 1. Project setup

Get the `QuestDemo` project open and configured for Meta Quest. Do this once, then move on to
[Feature 1](02-two-scenes-and-switch.md).

> Prerequisites (Unity 6 with Android Build Support, a Quest 3 in Developer Mode, USB-C) are listed
> in the [main README](../README.md).

---

## 1.1 Open the project

1. In **Unity Hub → Projects → Add → Add project from disk**, select the **`QuestDemo`** subfolder
   (the one containing `Assets`, `Packages`, `ProjectSettings`) — **not** the repository root. The
   root is not a Unity project; opening it makes Unity create an empty project there.
2. Open it with Unity 6 (`6000.3.5f2`). On first open Unity downloads the **Meta XR SDK** packages
   from the Unity package registry — this can take a few minutes.

## 1.2 First-launch prompts (one time only)

These appear the first time the project is opened — handle them like this:

- **Interaction SDK OpenXR Hand Skeleton Upgrade** dialog → click **Use OpenXR Hand (Recommended)**.
  (This is the future-proof choice; the legacy OVR hand path is being removed after SDK v77.)
- **"Changes to OVRPlugin detected … please restart"** → **File → Save**, then **restart Unity**
  (quit and reopen `QuestDemo`) so the new plugin binary loads.
- Accept any other "Restart Editor" / API-update prompts.

## 1.3 Run the Project Setup Tool

If the **Meta XR — Project Setup Tool** window appears with warnings, click **Fix All**, then
**Apply All**. You can reopen it any time via **Meta → Tools → Project Setup Tool**. It configures
the Quest-specific player/XR settings in one click.

## 1.4 Switch the build target to Android

**File → Build Profiles → Android → Switch Platform.**

## 1.5 Verify the build settings

The Project Setup Tool normally sets these for you. Confirm them under **Edit → Project Settings**
(this is also a handy reference list of what a Quest build needs):

| Setting | Value | Where |
| --- | --- | --- |
| XR plug-in | **OpenXR** (the Oculus XR plugin is deprecated for Unity 6 / SDK v74+) | XR Plug-in Management → Android |
| OpenXR features | **Meta XR Feature**, **Meta XR Foveation**, **Meta XR Subsampled Layout**, **Meta Quest Support**, an **Oculus Touch Controller Profile** | XR Plug-in Management → OpenXR (Android) |
| Color space | **Linear** | Player → Other Settings |
| Scripting backend | **IL2CPP** | Player → Other Settings |
| Target architectures | **ARM64** only | Player → Other Settings |
| Graphics API | **Vulkan** | Player → Other Settings |
| Minimum API level | **Android 12L (API 32)** or higher | Player → Other Settings |
| Target API level | **Android 14 (API 34)** | Player → Other Settings |

> Set a real **Package Name** too (Player → Other Settings), e.g. `com.tum.questdemo`, instead of
> the default `com.DefaultCompany.QuestDemo`.

## 1.6 Create the scenes folder

In the Project window, create **`Assets/Scenes`** (right-click → **Create → Folder**) if it doesn't
already exist. Your two scenes go here.

---

**Next:** [`02-two-scenes-and-switch.md`](02-two-scenes-and-switch.md) — build the VR and Passthrough
scenes with a switch button.
