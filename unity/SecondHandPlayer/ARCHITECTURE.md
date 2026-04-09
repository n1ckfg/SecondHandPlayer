# SecondHandPlayer Architecture

## Overview
SecondHandPlayer is a Unity-based application designed to visualize motion capture data received over the OSC (Open Sound Control) protocol. The system maps incoming telemetry to a 3D avatar rig in real-time.

## System Architecture

The project architecture is centered around a reactive pipeline that ingests OSC messages and updates 3D transforms.

### 1. Communication Layer (UnityOSC)
The project utilizes the `UnityOSC` package (`Assets/UnityOSC`) for low-level UDP networking and OSC packet parsing. 
- **`OSCHandler`**: Serves as the central manager for setting up OSC clients and servers to facilitate communication, primarily used for receiving mocap data.

### 2. Data Handling (MocapOscHandler)
- **`MocapOscHandler.cs`**: This component bridges the OSC network layer and the Unity visualization system. It listens for specific OSC addresses corresponding to joint positions and rotations, parsing the raw data into Unity-compatible formats.

### 3. Visualization Management (MocapVisualizer)
- **`MocapVisualizer.cs`**: Coordinates the overall visualization of the mocap actors. It manages the lifecycle and state of the mocap characters in the scene, instantiating the necessary `MocapJoint` prefabs and updating their transforms based on the incoming data stream provided by the handler.

### 4. Entity Component (MocapJoint)
- **`MocapJoint.cs`**: Attached to individual joint game objects (e.g., prefabs in `Assets/Prefab` like `MocapJoint.prefab`). It handles the local logic for a single data point, such as applying smoothing, local transform offsets, and visual updates for a specific part of the rig.

### 5. Assets & Prefabs
The visual representation relies on standard Unity assets:
- **Models**: FBX files (e.g., `k4a.fbx`, `Loops*_export.fbx`) provide the base meshes and rig structures.
- **Prefabs**: Dynamic instantiation of the rig is driven by prefabs like `MocapJoint.prefab` to allow flexible actor setups.
- **Materials**: Custom materials (`Assets/Materials`) are used to render the mocap data and the environment (e.g., skyboxes).

## Key Design Patterns

- **Observer/Listener Pattern**: Employed for OSC message handling. Components subscribe to specific OSC paths to react to incoming data asynchronously.
- **Component-Based Architecture**: Follows standard Unity practices where logic is decoupled into specific, reusable scripts (`MocapJoint`, `MocapVisualizer`, `MocapOscHandler`).
- **Bridge Pattern**: `MocapOscHandler` acts as a bridge, isolating the external OSC data format and network logic from the internal Unity transform and visualization logic.