Sure, here's the revised README with your adjustments:

---

# Turnbase-Adventure

Turnbase-Adventure is a turn-based adventure game built with Unity. It features strategic combat, exploration, and character progression.

## Table of Contents

1. [Features](#features)
2. [Key Scripts](#key-scripts)
3. [Usage](#usage)
4. [How to Play](#how-to-play)

## Features

- **Turn-based combat**: Plan your moves and defeat enemies.
- **Exploration**: Discover the map to meet heroes and monsters.
- **Character Progression**: Collect heroes to your party and gain status from monsters.

## Key Scripts

- **Character.cs**: Manages player characters.
- **Enemy.cs**: Controls enemy behaviors.
- **GameController.cs**: Main game logic and state management.
- **TurnManager.cs**: Handles the turn-based mechanics.

## Usage

1. **Play the Game:**
    - Open Unity and load the `Game` scene from `Assets/Scenes/`.
    - Press the Play button in the Unity Editor to start the game.

2. **Customizing:**
    - Modify `WorldSetting` in the `Assets/Script/Setting` folder to adjust size and environment.
    - Modify `SpawnSetting` in the `Assets/Script/Setting` folder to adjust spawn settings for `Hero` and `Monster`.

3. **Adding New Features:**
    - Create new scripts in the `Assets/Scripts/` folder.
    - Integrate new scripts with existing game objects through the Unity Editor.

## How to Play

1. Control characters with `W`, `A`, `S`, `D` keys.
2. Collect heroes on the map to add them to your party.
3. Battle enemies.
4. Heroes in your party gain status after defeating monsters. Monsters evolve and gain new statuses after specific turns.
