# Asset Pipeline & Optimization Guidelines (Low Poly)

## 1. Overview
This document establishes the import rules and optimization standards for the Low Poly Open World project. The focus is on a flat-shaded aesthetic, shared texture palettes, and efficient geometry.

## 2. Naming Conventions
Files must use **PascalCase** with underscores `_` as separators.
**Format:** `Prefix_AssetName_Variant_Suffix`

### 2.1 Asset Prefixes
| Asset Type | Prefix | Example |
| :--- | :--- | :--- |
| **Static Mesh** | `SM_` | `SM_PineTree_01` |
| **Skeletal Mesh** | `SK_` | `SK_Player_Knight` |
| **Material** | `M_` | `M_GlobalPalette` |
| **Texture** | `T_` | `T_ColorPalette_01` |
| **Prefab** | `PF_` | `PF_Barn_Red` |

---

## 3. Import Rules (Models)

### 3.1 Model Settings
* **Scale Factor:** `1.0` (1 Unit = 1 Meter).
* **Mesh Compression:** **Medium**.
* **Read/Write Enabled:** **OFF** (unless script access is required).
* **Rig Type:** **None** (for environment) / **Generic** (for simple animations).

### 3.2 Normals & Shading (Crucial for Low Poly)
* **Normals:** Set to **Import** or **Calculate**.
* **Smoothing Angle:** Set to **0** (or check "Hard Edges") to ensure the "faceted" low poly look. **Do not smooth normals.**

### 3.3 Pivot Points
* **Environment:** Bottom-Center (Y=0) for easy placement.
* **Modular Pieces:** Corner pivots for precise grid snapping.

---

## 4. Optimization & Budgets

### 4.1 Polygon Limits (Triangles)
Low poly relies on shape, not density. Stick to these strict budgets:

| Asset Type | Target Triangles | Max Limit |
| :--- | :--- | :--- |
| **Main Character** | 800 - 1,200 | 1,500 |
| **Large Buildings** | 1,000 - 1,500 | 2,000 |
| **Trees/Vegetation** | 300 - 500 | 800 |
| **Small Props (Crates)** | 50 - 150 | 200 |

### 4.2 Texture Guidelines
* **Workflow:** Use **Texture Atlasing** (Color Palettes).
* **Resolution:**
    * **Global Palette:** 256x256 or 512x512 (Shared across multiple assets).
    * **UI/Icons:** 512x512 max.
* **Filter Mode:** **Point (No Filter)**. This keeps pixel art or solid colors crisp; do not use Bilinear/Trilinear filtering.
* **Compression:** `RGBA Compressed` or `High Quality`.

---

## 5. Prefabs & Organization

### 5.1 Prefab Rule
* All meshes must be converted to **Prefabs** before placement in the scene.
* **Material Batching:** Try to share **one single material** (using a texture atlas) across as many environment objects as possible to reduce Draw Calls.

### 5.2 LOD (Level of Detail)
Even in low poly, LODs help performance on mobile/web.
* **LOD0:** 100% tris.
* **LOD1:** 50% tris (remove small details like door knobs, window frames).
* **Culling:** Objects should disappear completely at >200 meters (depending on size).

---

## 6. Directory Structure

```text
Assets/
├── <your_rollno>/
│   ├── Docs/              # This file goes here
│   ├── Models/            # FBX files
│   ├── Materials/         # Shared Palette Materials
│   ├── Textures/          # Palette Textures
│   └── Prefabs/           # Final Game Objects
