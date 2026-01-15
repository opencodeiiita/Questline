# Combat System Documentation

## Overview
This combat system implements a basic interactions between the Player and AI Enemies using a modular component approach.

## Scripts Breakdown

### 1. Health.cs
- **Purpose:** Manages the entity's HP, damage processing, and death state.
- **Key Methods:** - `TakeDamage(float amount)`: Reduces HP and triggers visual feedback.
  - `Die()`: Handles logic when HP reaches zero.
- **VFX:** Changes material color to Red momentarily upon taking damage.

### 2. EnemyAI.cs
- **Purpose:** Controls enemy behavior based on distance to the player.
- **Logic:**
  - **Idle:** When player is outside `Detection Radius`.
  - **Chase:** When player is inside `Detection Radius` but outside `Attack Range`.
  - **Attack:** When player is inside `Attack Range`.

### 3. DamageDealer.cs
- **Purpose:** Attached to weapons or colliders to inflict damage on impact.
- **Usage:** Uses `OnTriggerEnter` to detect `Health` components on objects with specific tags.

### 4. CombatManager.cs
- **Purpose:** Manages the game lifecycle, specifically the Player's respawn logic.
- **Logic:** Subscribes to the Player's `OnDeath` event to trigger a respawn coroutine.

## How to Test
1. Open the Scene.
2. Ensure the Player has the `Health` script and is tagged "Player".
3. Ensure the Enemy has `Health`, `EnemyAI`, and a Collider/Rigidbody.
4. Move the Player close to the Enemy to trigger the Chase/Attack state.