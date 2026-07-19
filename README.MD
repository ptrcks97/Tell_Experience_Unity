# Tell-Experience Unity
This repository contains the Unity frontend for my Bachelor0s thesis project.
The unity project includes the virtual character, lip-sync setup, environment, scene configuration and the connection setup for communicating with the Tell Experience agent backend.

Some required third-party assets are not included in this repository due to licensing restrictions. These assets mist ne added manually before the project can run correctly

## Features
- Unity-based frontend for the Tell Experience application
- Virtual character setup
- Lip-sync integration
- 3D environment
- Spatial Audio
- Agent connection configuration
- VR configuration
- LiveKit / Backend integration setup
- Animation and Sound handling

## Tech Stack
- Unity
- C#
- LiveKit
- Third-Party assets
- Tell Experience backend services

## Requirements
Before running this project, make sure the following tools are installed
- Unity
- Required third-party assets listed below

## Required Third-Party Assets
The following assets are required but are not included in this repostory because of licensing restrictions. LiveKit Client SDK can be loaded from an external repository.

|Asset|Required|Purpose|Notes|
|-----|--------|-------|-----|
|LiveKit Unity Client SDK - Own Version| Only for VR | Enables connection to the agent| A custom modification of the official Unity client was developed to support the Meta Quest's microphopne and replace the audio pipeline for lip-syncing. It can be cloned at: https://github.com/ptrcks97/Tell-Experience-client-sdk-unity|
|SpeechBlend|Yes|Lip-sync functionality|Required for character speech animation|
|Reallusion Unity Pipeline|Yes|Import tool for Character Creator Avatar|Required for shaders of ter avatar and must be imported manually|

Some large or licensed files have been intentionally excluded from this repository.
These may need to be added manually:
- Unity will automatically detect missing assets
- Alternatively, they can be replaced with your own assets

Once you've installed Speechblend, you'll need to link all Speechblend scripts to the corresponding blendshapes in the scene. They are all named the same.
When setting up the Speechblend components, make sure to select the correct avatar properties (CC3 / CC4)

## Installation
Open the project with Unity Hub and add all Third-Party assets in the Unity Package manager.
Make sure to assign the correct shaders to the assets. For MetaQuest the mobile shader is recommended.

To make sure all assets are beeing downloaded use Git LFS:

### Installation LFS:
```
git lfs install
```
### Pull the Repository
```
git clone https://github.com/ptrcks97/Tell_Experience_Unity
git lfs pull
```

## Structure
```
ASSETS
├───Behavior
├───Blender_Assets
│   ├───candle
│   │   ├───FBX
│   │   ├───Material
│   │   └───Texture
│   ├───Crossbow
│   │   ├───FBX
│   │   ├───Material
│   │   └───Texture
│   ├───House
│   │   ├───FBX
│   │   ├───Materials
│   │   └───Textures
│   └───Table
│       ├───FBX
│       ├───Materials
│       └───Textures
├───Environment
├───Handlers
├───LiveKitConnector
├───Oculus
├───Plugins
│   └───Android
├───Reallusion
│   └───CCiC Unity Tools
│       ├───Editor
│       ├───Runtime
│       ├───Textures
│       └───URP
│           ├───Material Templates
│           │   ├───Baked
│           │   ├───Default
│           │   └───HQ
│           ├───Preview Scene
│           └───Shaders
│               ├───Amplify
│               │   ├───Baked
│               │   │   └───Tessellation
│               │   ├───Function
│               │   └───HQ
│               │       └───Tessellation
│               └───Shader Graph
│                   ├───Baked
│                   ├───Basic
│                   ├───HQ
│                   └───Subgraph
├───Resources
├───Scenes
│   └───Wilhelm_Tell_Scene
├───Settings
├───Sounds
├───Speechblend
│   ├───Demo
│   │   ├───Animations
│   │   ├───AnimatorController
│   │   ├───Audio
│   │   ├───Materials
│   │   ├───Models
│   │   ├───Scenes
│   │   │   ├───SpeechBlendBasicDemo
│   │   │   └───SpeechBlendMicrophoneInputDemo
│   │   ├───Scripts
│   │   └───Textures
│   ├───Documentation
│   └───Scripts
│       └───WebGL
├───StreamingAssets
├───Terrain
│   ├───Dirt Layer
│   ├───Moss Layer
│   └───Stone Layer
├───TextMesh Pro
│   ├───Fonts
│   ├───Resources
│   │   ├───Fonts & Materials
│   │   ├───Sprite Assets
│   │   └───Style Sheets
│   ├───Shaders
│   └───Sprites
├───WilhelmTell
│   ├───Animations
│   │   └───WilhelmTell
│   ├───Baked
│   │   └───wilhelm_tell_3_sitting
│   │       ├───Materials
│   │       └───Textures
│   ├───Materials
│   │   └───wilhelm_tell_3_sitting
│   ├───Meshes
│   │   └───wilhelm_tell_3_sitting
│   ├───Prefabs
│   ├───Settings
│   │   ├───wilhelm_tell_3_sitting
│   │   └───wilhelm_tell_3_sitting_Reference
│   ├───textures
│   │   └───wilhelm_tell_3_sitting
│   │       ├───Boxers
│   │       │   └───Boxers
│   │       │       └───Boxers
│   │       ├───CC_Base_Eye
│   │       │   └───CC_Base_Eye
│   │       │       ├───Std_Cornea_L
│   │       │       ├───Std_Cornea_R
│   │       │       ├───Std_Eye_L
│   │       │       └───Std_Eye_R
│   │       ├───CC_Base_EyeOcclusion
│   │       │   └───CC_Base_EyeOcclusion
│   │       │       ├───Std_Eye_Occlusion_L
│   │       │       └───Std_Eye_Occlusion_R
│   │       ├───CC_Base_TearLine
│   │       │   └───CC_Base_TearLine
│   │       │       ├───Std_Tearline_L
│   │       │       └───Std_Tearline_R
│   │       ├───CC_Base_Teeth
│   │       │   └───CC_Base_Teeth
│   │       │       ├───Std_Lower_Teeth
│   │       │       └───Std_Upper_Teeth
│   │       ├───Chinstrap_Thick
│   │       │   └───Chinstrap_Thick
│   │       │       └───Beard1_Transparency
│   │       ├───Male_Bushy
│   │       │   └───Male_Bushy
│   │       │       ├───Male_Bushy_Base_Transparency
│   │       │       └───Male_Bushy_Transparency
│   │       ├───Mustache_Horseshoe
│   │       │   └───Mustache_Horseshoe
│   │       │       └───Beard3_Transparency
│   │       ├───Plane_003
│   │       │   └───Plane_003
│   │       │       └───Weaved_Material_002
│   │       ├───Short_blowback
│   │       │   └───Short_blowback
│   │       │       ├───Hair_Transparency
│   │       │       └───Scalp_Transparency
│   │       ├───Sideburns_Narrow
│   │       │   └───Sideburns_Narrow
│   │       │       └───Beard_Transparency
│   │       ├───Soul_Path_Thick
│   │       │   └───Soul_Path_Thick
│   │       │       └───Beard2_Transparency
│   │       └───wilhelm_tell_3_sitting
│   │           ├───CC_Base_Body
│   │           │   ├───Std_Eyelash
│   │           │   ├───Std_Nails
│   │           │   ├───Std_Skin_Arm
│   │           │   ├───Std_Skin_Body
│   │           │   ├───Std_Skin_Head
│   │           │   └───Std_Skin_Leg
│   │           ├───CC_Base_Tear_Ducts
│   │           │   └───CC_Base_Tear_Ducts
│   │           └───CC_Base_Tongue
│   │               └───Std_Tongue
│   └───wilhelm_tell_3_sitting.fbm
├───Wilhelm_Tell_Unity
├───XR
│   ├───Loaders
│   ├───Settings
│   └───Settings 1
└───_Recovery
```
## Notes
This project is designed to work with a LiveKit Agent.
