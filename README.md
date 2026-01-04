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

## Current Architecture — Milestone 3
The initial monolithic `PlayerController` was split and extended with shooting and animation states:
- **PlayerInputAdapter** – reads `PlayerInput` and raises high-level events: `OnMoveX(float)`, `OnJump()`, `OnStartAttack()`, `OnStopAttack()`
- **PlayerController** – thin orchestrator wiring input to movement, jump, attack, facing updates, and attack animation flags
- **PlayerMovement** – horizontal movement (+ sprite flip)
- **PlayerJump** – jump request/consumption with grounded check
- **PlayerAttack** – toggles attack state, tracks facing, and spawns projectiles from the correct barrel on a timer
- **Projectile** – moves straight using `Rigidbody2D`, flips direction from the attacker, despawns on hit or when offscreen
- **PlayerAnimation** – resolves `Idle/Run/Jump/Attack/Run_Attack` based on velocity and attack flag; plays Animator states
- **GroundSensor2D** (reusable) – overlap-based ground detection
- **CameraController** – follows the player in the scene

## Folder Structure (lean)

```
Assets/
  _Project/
    Animations/
      Player/
      Projectile/
    Data/
      Input/
    Materials/
      Physics Materials/
    Prefabs/
    Scenes/
    Scripts/
      Animations States/
        Player/
      Camera/
      Input/
      Player/
      Projectile/
      Utilities/
        Sensors/
```

## Getting Started

1. Open Unity 6 and clone the repo.
2. Open `Assets/_Project/Scenes/20_Gameplay.unity`.
3. Press Play.

### Controls (Keyboard & Mouse)

* **A/D** or **←/→**: Move
* **Space**: Jump
* **Left Mouse Click**: Attack

## Milestones & Roadmap

* ✅ **Milestone 1:** Single-script `PlayerController` (move + jump).
* ✅ **Milestone 2:** Split into input adapter, controller (orchestrator), movement, jump, reusable ground sensor and camera script.
* ⏭️ **Milestone 3 (current):** New feature: **Shooting** (projectiles), new sprites and basic animations.
* ⏭️ **Milestone 4:** Enemies, hazards, checkpoints, basic UI (pause/restart).
* ⏭️ **Milestone 5:** Polish pass (animation states, coyote time, jump buffering, camera tweaks).

## Tech Notes

* Uses Unity’s **New Input System** (`GameInput.inputactions`) with an action map `Player`:

  * `Move` (Vector2)
  * `Jump` (Button)
  * `Attack` (Button)
* Character Physics tuned via `Rigidbody2D` linearVelocity writes in `FixedUpdate`.

## Coding Rules

* Always use curly braces, even in simple "if" statements.
* Use the C# standard for opening curly braces: on a new line.
* Avoid "nesting ifs": prefer using early return pattern, also known as a guard clause, and safety checks.
* Avoid excessive code coupling.
* Use #region/#endregion to organize your code.

## Commit Philosophy

Start **simple**, ship a working baseline, then iterate with focused commits that either:

* Add a feature, or
* Improve architecture/clarity without changing behavior.

Commits can be:

* Small: for code fixes and improvements
* Large: for new features added after testing

## License

MIT (feel free to fork and extend).
