# Modular Quest System Framework

## Overview

This document describes the design and implementation of a modular Quest System for a 3D Open World project.  
The system supports multi-step quests, multiple objective types, quest chaining, trigger-based progression, and UI feedback.

The goal is to provide a flexible and extensible framework that can be easily expanded with new quest types and objectives.

---

## Core Components

### 1. QuestManager

The QuestManager is the central controller of the quest system.

Responsibilities:
- Stores all available quests
- Starts quests using Quest ID
- Tracks active and completed quests
- Updates objectives
- Completes quests
- Starts chained quests automatically
- Sends UI notifications

Implemented as a Singleton so that all objectives can notify it globally.

---

### 2. Quest

Each Quest acts as a container for multiple objectives.

Fields:
- Quest ID
- Quest Name
- Quest State (NotStarted, InProgress, Completed)
- List of Objectives
- Current Objective Index
- Next Quest ID (for chaining)

Only one objective is active at a time.  
When all objectives are completed, the quest is marked as Completed.

---

### 3. QuestObjective (Base Class)

All objectives inherit from the QuestObjective base class.

Responsibilities:
- Provide common completion logic
- Notify QuestManager when completed

This allows easy extension of new objective types using inheritance.

---

## Objective Types Implemented

All objective types inherit from QuestObjective:

1. ReachLocationObjective  
   - Trigger-based completion when player enters a zone

2. CollectItemObjective  
   - Completes after collecting required number of items

3. InteractNPCObjective  
   - Completes when player interacts with an NPC

4. DefeatEnemyObjective  
   - Completes after killing required number of enemies

Additional objective types can be added by inheriting QuestObjective.

---

## Trigger Zones

Trigger zones are used for:
- Starting quests
- Completing reach-location objectives

They are implemented using Box Colliders with IsTrigger enabled.

When the player enters the trigger:
- QuestTriggerZone starts a quest
- ReachLocationObjective completes an objective

---

## Quest Chaining

Each Quest contains a Next Quest ID field.

When a quest is completed:
- QuestManager automatically starts the next quest if the ID is valid

This allows creation of continuous story-driven quest flows.

---

## UI System

### Notifications

Simple text-based notifications are shown when:
- Quest is accepted
- Objective is updated
- Quest is completed

This provides immediate player feedback.

### Quest Log

The Quest Log UI displays:
- Active quests
- Completed quests

The log refreshes whenever quest state changes.

---

## Example Quest Flow

Three playable quests are implemented:

### Quest 1: Enter the Village
Objectives:
1. Reach village gate
2. Talk to village elder

Chains to Quest 2

---

### Quest 2: Collect Healing Herbs
Objectives:
1. Collect herbs
2. Return to elder

Chains to Quest 3

---

### Quest 3: Clear the Forest
Objectives:
1. Defeat enemies
2. Reach forest shrine

End of quest chain

All objective types are demonstrated across these quests.

---

## System Flow Diagram

Player Action
↓
Objective Triggered
↓
QuestObjective Completed
↓
QuestManager Updates Quest
↓
Next Objective Activated
↓
All Objectives Done
↓
Quest Completed
↓
Next Quest Started (if chained)



---

## Extensibility

To add a new objective type:

1. Create a new class inheriting from QuestObjective
2. Implement completion condition
3. Attach it to a scene object
4. Add it to a quest's objective list

No changes are required in QuestManager or Quest classes.

---

## Conclusion

This quest system provides:
- Modular objective design
- Clear quest progression
- Automatic quest chaining
- Trigger-based interaction
- UI feedback and quest logging

It is suitable for open-world gameplay and can be expanded easily for future features.
