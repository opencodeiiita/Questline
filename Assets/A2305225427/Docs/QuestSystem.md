# Quest System Framework

## Overview

This Quest System Framework is a modular and extensible solution designed for a 3D Open World game.
It supports multi-step quests, multiple objective types, quest chaining, trigger-based progression,
and real-time UI feedback.

The system is data-driven, editor-friendly, and easy to expand without modifying core logic.

---

## Core Architecture

The quest system is built around a centralized manager that coordinates quests, objectives,
and UI updates.

### Main Components

- QuestManager
- Quest (ScriptableObject)
- QuestObjective
- Objective Behaviours
- UI Layer

---

## Quest States

- NotStarted
- Active
- Finished

---

## Objective System

Objectives are processed sequentially.

Supported types:
- Location
- Item
- NPC
- Enemy

---

## Trigger Zones

Trigger zones control quest start and progression using collider detection.

---

## Quest Chaining

Quests can automatically start follow-up quests once completed.

---

## UI System

- Quest Notifications
- Quest Log

---

## Execution Flow

Player Action → Objective Behaviour → QuestManager → UI → Quest Completion

---

## File Structure

Assets/A2305225427/
├── Scripts/
│   └── Quest/
|           └── ObjectiveTypes/
├── UI/
│   └── Quest/
└── Docs/
    └── QuestSystem.md

---

## Summary

A clean, scalable quest framework for open-world gameplay.