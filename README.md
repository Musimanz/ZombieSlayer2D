# Zombie Slayer | 2D Platformer Shooter Game (Live Preview: https://zombie-slayer-web.vercel.app/)

A fast-paced, action-driven 2D survival platformer built with Unity and C#. Players navigate a confined arena while defending against continuous waves of dynamic enemy spawns, utilizing spatial tracking tools and resource management to achieve high scores.

## Features

- **Dynamic Enemy Spawns:** Zombie hazards spawn continuously from arena flanks to keep gameplay fast and intense.
- **Off-Screen Threat Indicators:** Screen-edge directional arrows notify the player of incoming hazards outside the active viewport.
- **Resource Management:** Health and ammo tracking systems require strategic positioning and selective engagement.
- **Persistent High Scores:** Saves score and kill history locally using Unity's `PlayerPrefs`.
- **Custom UI & Viewport Controls:** Clean canvas implementation featuring dynamic status bars, threat markers, and score overlays.

## Tech Stack & Tools

- **Engine:** Unity (2D Pipeline)
- **Language:** C#
- **UI Framework:** Unity UI (`RectTransform`, `Canvas`)
- **Data Persistence:** `PlayerPrefs`

## Core Mechanics

- **Movement & Physics:** Responsive 2D character physics and directional controls.
- **Off-Screen Tracking:** Screen-space viewport math maps off-screen enemy vectors to UI edge indicators in real-time.
- **Spawning Logic:** Scalable hazard management to control enemy population and spawn rates.

## Getting Started

### Prerequisites

- [Unity Hub](https://unity.com/download)
- Unity Editor (version `2021.3` LTS or newer recommended)
