# 2D Platformer Demo (Unity)

A compact, production-minded 2D platformer built with Unity 6 to showcase **clean code**, **incremental evolution**, and **readable gameplay scripts**. 
The first milestone ships fast with a single, self-contained `PlayerController` handling movement and jump using `Rigidbody2D` and the New Input System. 
Future milestones focus on adding features and improving architecture step by step.

## Purpose

This repository is a portfolio piece emphasizing readability, low coupling, and a clear upgrade path—from a working MVP to a scalable, maintainable codebase.

## Highlights

* Unity 6 • C# • New Input System
* Deterministic physics loop with `FixedUpdate`
* Simple, reusable ground check
* Small, focused components (separation of concerns)

## Current Architecture — Milestone 2
The initial monolithic `PlayerController` was split into small components:
- **PlayerInputAdapter** – reads `PlayerInput` and raises high-level events:
  - `OnMoveX(float)`, `OnJump()`
- **PlayerController** – thin orchestrator (wires input → abilities)
- **PlayerMovement** – horizontal movement (+ sprite flip)
- **PlayerJump** – jump request/consumption with grounded check
- **GroundSensor2D** (reusable) – overlap-based ground detection

## Folder Structure (lean)

```
Assets/
  _Project/
    Data/
      Input/
        GameInput.inputactions
    Scenes/
      20_Gameplay.unity
    Scripts/
      Input/
        GameInput.cs
        PlayerInputAdapter.cs
      Player/
        PlayerController.cs
        PlayerMovement.cs
        PlayerJump.cs
      Utilities/
        Sensors/
          GroundSensor2D.cs
```

## Getting Started

1. Open Unity 6 and clone the repo.
2. Open `Assets/_Project/Scenes/20_Gameplay.unity`.
3. Press Play.

### Controls

* **A/D** or **←/→**: Move
* **Space**: Jump

## Milestones & Roadmap

* ✅ **Milestone 1 (now):** Single-script `PlayerController` (move + jump).
* ✅ **Milestone 2:** Split into input adapter, controller (orchestrator), movement, jump, and reusable ground sensor
* ⏭️ **Milestone 3:** New feature: **Shooting** (projectiles or hitscan), basic VFX/SFX.
* ⏭️ **Milestone 4:** Enemies, hazards, checkpoints, basic UI (pause/restart).
* ⏭️ **Milestone 5:** Polish pass (animation states, coyote time, jump buffering, camera tweaks).

## Tech Notes

* Uses Unity’s **New Input System** (`GameInput.inputactions`) with an action map `Player`:

  * `Move` (Vector2)
  * `Jump` (Button)
* Physics tuned via `Rigidbody2D` linearVelocity writes in `FixedUpdate`.

## Commit Philosophy

Start **simple**, ship a working baseline, then iterate with small, focused commits that either:

* Add a feature, or
* Improve architecture/clarity without changing behavior.

## License

MIT (feel free to fork and extend).
