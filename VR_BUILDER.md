# Alternative: setting up the project with VR Builder

[VR Builder](https://www.mindport.co/vr-builder) (by MindPort) is an alternative, no-code toolkit
for building VR experiences in Unity. Its big convenience is that **it auto-configures the XR
environment for you**, so you can skip most of the manual project setup described in the main
[`README`](README.md).

## Create the project

Create a plain **3D (Universal / URP)** project. You **do not** need a VR template project — the VR
Builder setup handles the environment for you.

## Install VR Builder

There are two ways to install the VR Builder package:

- **Unity Asset Store + Package Manager** — this path is paid, so we ignore it here.
- **GitHub (free)** — go to the releases page:
  <https://github.com/MindPort-GmbH/VR-Builder/releases>, download the Unity package (`.unitypackage`)
  file, then in Unity go to **Assets → Import Package → Custom Package…**, select the downloaded
  package, and **Import All**.

## The setup wizard

A setup wizard appears after a minute or so.

> ⚠️ The documentation you'll find via Google is a little outdated when it comes to this wizard, so
> follow the steps below.

The wizard walks you through:

1. **OpenXR controller profile** — select the profile that matches your device. If you're working
   with **Meta**, select **both** Meta options.
2. **Localization settings** — choose **"Use a single language in this project"** for simplicity's
   sake.

   > **Unity Localization** is an official Unity package (`com.unity.localization`) that enables
   > developers to adapt their games and apps for multiple languages and regional audiences.

After that, the wizard shows some documentation links (see [Resources](#resources) below — the rest
are not very important) and offers to **load a demo**. If you are using the VR Builder toolkit for
the first time, start with the **demo** and follow along with the documentation's **Quick Start**
section to check out the features.

## Resources

- VR Builder releases (GitHub): <https://github.com/MindPort-GmbH/VR-Builder/releases>
- Features & tutorials: <https://www.mindport.co/vr-builder/tutorials>
