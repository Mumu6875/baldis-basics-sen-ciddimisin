#!/usr/bin/env bash
set -euo pipefail

test -f Assets/Plugins/Control-Freak-2/Scripts/System/CF2Input.cs
test -f Assets/Resources/Mobile/CF2-Rig.prefab
test -f Assets/Scripts/Mobile/MobileControlsBootstrap.cs
rg -q 'CF2Input.GetButton\("Fire1"\)' Assets/Scripts/Core/UI/Settings/ControlMapper/InputManager.cs
rg -q 'CF2Input.GetButton\("Run"\)' Assets/Scripts/Core/UI/Settings/ControlMapper/InputManager.cs
rg -q 'CF2Input.GetAxis\("Forward"\)' Assets/Scripts/Core/UI/Settings/ControlMapper/InputManager.cs
rg -q 'CF2Input.GetAxis\("Strafe"\)' Assets/Scripts/Core/UI/Settings/ControlMapper/InputManager.cs
rg -q 'CF2Input.GetAxis\("Mouse X"\)' Assets/Scripts/PlayerFunctions/PlayerMovement.cs
rg -q 'CF2Input.GetAxis\("Mouse X"\)' Assets/Scripts/PlayerFunctions/PlayerScript.cs
