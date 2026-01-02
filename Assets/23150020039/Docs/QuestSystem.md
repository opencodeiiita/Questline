# Quest System Framework Documentation

## Architecture Overview

The Quest System Framework is a modular system designed for the 3D Open World project. It supports multi-step quests, different objective types, quest chaining, and UI feedback.

## Core Components

### QuestManager
Central singleton that manages all quests. Handles starting, tracking, updating, and completing quests.

### QuestData (ScriptableObject)
Defines quest properties:
- Quest ID, Title, Description
- List of objectives
- Chained quests (auto-start after completion)

### QuestObjective
Individual objective within a quest:
- Objective type (ReachLocation, CollectItems, InteractWithNPC, DefeatEnemies)
- Target ID for matching
- Required and current amounts
- Completion status

### Quest States
- NotStarted
- InProgress
- Completed

## Objective Types

| Type | Description | Trigger |
|------|-------------|---------|
| ReachLocation | Player reaches a specific area | Collider trigger |
| CollectItems | Player collects items | Item pickup |
| InteractWithNPC | Player talks to NPC | E key interaction |
| DefeatEnemies | Player defeats enemies | Enemy death callback |

## Flow Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│                        QUEST FLOW                                │
└─────────────────────────────────────────────────────────────────┘

    ┌──────────────┐
    │ TriggerZone  │
    │ (Start Area) │
    └──────┬───────┘
           │ Player enters
           ▼
    ┌──────────────┐
    │ QuestManager │
    │ StartQuest() │
    └──────┬───────┘
           │ OnQuestStarted event
           ▼
    ┌──────────────┐
    │ Notification │
    │ "Quest       │
    │  Accepted"   │
    └──────┬───────┘
           │
           ▼
    ┌──────────────────────────────────────┐
    │         OBJECTIVES LOOP               │
    │  ┌─────────────────────────────────┐ │
    │  │ Objective 1: Reach Location     │ │
    │  │ ┌─────────────┐                 │ │
    │  │ │ Player      │──► UpdateQuest  │ │
    │  │ │ enters zone │    Progress()   │ │
    │  │ └─────────────┘                 │ │
    │  └─────────────────────────────────┘ │
    │                 │                     │
    │                 ▼                     │
    │  ┌─────────────────────────────────┐ │
    │  │ Objective 2: Collect Items      │ │
    │  │ ┌─────────────┐                 │ │
    │  │ │ Player      │──► UpdateQuest  │ │
    │  │ │ picks item  │    Progress()   │ │
    │  │ └─────────────┘                 │ │
    │  └─────────────────────────────────┘ │
    │                 │                     │
    │                 ▼                     │
    │  ┌─────────────────────────────────┐ │
    │  │ Objective 3: Talk to NPC        │ │
    │  │ ┌─────────────┐                 │ │
    │  │ │ Player      │──► UpdateQuest  │ │
    │  │ │ presses E   │    Progress()   │ │
    │  │ └─────────────┘                 │ │
    │  └─────────────────────────────────┘ │
    │                 │                     │
    │                 ▼                     │
    │  ┌─────────────────────────────────┐ │
    │  │ Objective 4: Defeat Enemies     │ │
    │  │ ┌─────────────┐                 │ │
    │  │ │ Enemy       │──► UpdateQuest  │ │
    │  │ │ defeated    │    Progress()   │ │
    │  │ └─────────────┘                 │ │
    │  └─────────────────────────────────┘ │
    └──────────────────┬───────────────────┘
                       │ All objectives complete
                       ▼
    ┌──────────────────────────────────────┐
    │           CheckQuestCompletion()      │
    │                  │                    │
    │                  ▼                    │
    │           CompleteQuest()             │
    │                  │                    │
    │    ┌─────────────┴─────────────┐     │
    │    ▼                           ▼     │
    │ OnQuestCompleted          Start      │
    │ Event                     Chained    │
    │    │                      Quests     │
    │    ▼                           │     │
    │ Notification                   │     │
    │ "Quest Complete"               │     │
    └────────────────────────────────┼─────┘
                                     │
                                     ▼
                          ┌──────────────────┐
                          │ Next Quest in    │
                          │ Chain Starts     │
                          │ Automatically    │
                          └──────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                      UI SYSTEM                                   │
└─────────────────────────────────────────────────────────────────┘

    ┌─────────────────┐     ┌─────────────────┐
    │ QuestLogUI      │     │ QuestNotification│
    │ (Press J)       │     │ UI              │
    ├─────────────────┤     ├─────────────────┤
    │ - Active Quests │     │ - Quest Accepted│
    │ - Objectives    │     │ - Quest Updated │
    │ - Progress      │     │ - Quest Complete│
    │ - Completed     │     │ - Fade In/Out   │
    └─────────────────┘     └─────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                    EVENT SYSTEM                                  │
└─────────────────────────────────────────────────────────────────┘

    QuestManager Events:
    ├── OnQuestStarted(QuestData)
    ├── OnQuestCompleted(QuestData)
    ├── OnObjectiveCompleted(QuestData, QuestObjective)
    └── OnQuestUpdated(QuestData)
```

## Sample Quests

### Quest 1: The Beginning
- Objective: Reach the village entrance
- Chains to: Quest 2

### Quest 2: Gather Supplies
- Objective 1: Collect 3 herbs
- Objective 2: Talk to the merchant
- Chains to: Quest 3

### Quest 3: The Hunt
- Objective 1: Defeat 5 wolves
- Objective 2: Return to the village

## Usage

### Creating a New Quest
1. Right-click in Project window
2. Create > Questline > Quest Data
3. Fill in quest details and objectives
4. Add to QuestManager's available quests list

### Setting Up Trigger Zones
1. Create empty GameObject with Collider (Is Trigger = true)
2. Add TriggerZone component
3. Set quest ID to start

### Setting Up Objectives in Scene
1. Add appropriate objective component to target objects
2. Set matching target IDs

## File Structure

```
Assets/23150020039/
├── Scripts/
│   ├── Quest/
│   │   ├── QuestManager.cs
│   │   ├── Quest.cs
│   │   ├── QuestObjective.cs
│   │   └── ObjectiveTypes/
│   │       ├── ReachLocationObjective.cs
│   │       ├── CollectItemObjective.cs
│   │       ├── InteractWithNPCObjective.cs
│   │       └── DefeatEnemyObjective.cs
│   └── UI/
│       └── Quest/
│           ├── QuestLogUI.cs
│           └── QuestNotificationUI.cs
└── Docs/
    └── QuestSystem.md
```