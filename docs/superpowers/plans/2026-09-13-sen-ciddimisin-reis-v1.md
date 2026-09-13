# Sen Ciddimisin Reis? V1 Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Build an editable Unity 6 Android project named **Sen Ciddimisin Reis?** that preserves the template gameplay, replaces Baldi everywhere with the supplied animated character, and displays `SEN CİDDİ MİSİN?` for two seconds after each notebook collection.

**Architecture:** Preserve the existing scenes and gameplay scripts. Add small input and notification adapters, replace character assets at project level while preserving animation GUID wiring, and migrate package/player configuration to Unity 6. Verification combines Unity EditMode tests where available with deterministic repository checks that do not require the editor.

**Tech Stack:** Unity 6, C#, Unity UI/TextMesh Pro, legacy Input Manager compatibility, Android ARM64, PNG sprite assets, shell-based project validation.

**Spec:** `docs/superpowers/specs/2026-09-13-sen-ciddimisin-reis-v1-design.md`

## Global Constraints

- Keep school layout, notebook collection, math questions, NPC AI, win flow, and loss flow behavior unchanged.
- Set the visible product name to exactly `Sen Ciddimisin Reis?`.
- Use the supplied character for gameplay, main menu, and math/YCTP Baldi appearances.
- Show exactly `SEN CİDDİ MİSİN?` at screen center for two unscaled seconds after every notebook collection.
- Preserve keyboard and mouse input while adding touch controls.
- Configure Android landscape orientation and ARM64 support.
- Deliver an editable Unity 6 project as `BaldisCiddimisinV1.zip` without generated cache directories.

---

### Task 1: Establish Unity 6 project configuration and validation

**Files:**
- Modify: `ProjectSettings/ProjectVersion.txt`
- Modify: `Packages/manifest.json`
- Modify: `ProjectSettings/ProjectSettings.asset`
- Create: `tools/validate_project.sh`
- Create: `.gitignore`

**Interfaces:**
- Consumes: Existing Unity 2018 project YAML and scene paths.
- Produces: A Unity 6 project configuration and a repository-level validation command, `bash tools/validate_project.sh`.

- [ ] **Step 1: Write the failing validation script**

```bash
#!/usr/bin/env bash
set -euo pipefail
rg -q '^m_EditorVersion: 6000\.' ProjectSettings/ProjectVersion.txt
rg -q 'productName: Sen Ciddimisin Reis\?' ProjectSettings/ProjectSettings.asset
rg -q 'bundleVersion: 1\.0\.0' ProjectSettings/ProjectSettings.asset
rg -q 'AndroidTargetArchitectures: 2' ProjectSettings/ProjectSettings.asset
test -f Assets/Scene/MainMenu.unity
test -f Assets/Scene/School.unity
```

- [ ] **Step 2: Run the script and confirm failure**

Run: `bash tools/validate_project.sh`

Expected: FAIL because the editor version and product name still describe the source template.

- [ ] **Step 3: Apply the minimal migration settings**

Set `m_EditorVersion` to an installed Unity 6 editor version if present, otherwise a valid `6000.0` project version marker; replace obsolete analytics/package-manager dependencies with Unity 6-supported package declarations; set `productName`, `applicationIdentifier.Android` to `com.ajordiojo.senciddimisinreis`, `bundleVersion` to `1.0.0`, landscape orientation, and `AndroidTargetArchitectures: 2` for ARM64.

- [ ] **Step 4: Run validation**

Run: `bash tools/validate_project.sh`

Expected: PASS.

- [ ] **Step 5: Commit**

```bash
git add .gitignore Packages ProjectSettings tools/validate_project.sh
git commit -m "build: migrate project configuration to Unity 6"
```

### Task 2: Prepare and install the animated character asset set

**Files:**
- Create: `Assets/Texture2D/SenCiddimisinReis/Source/SenCiddimisinReis.png`
- Create: `Assets/Texture2D/SenCiddimisinReis/Gameplay/*.png`
- Create: `Assets/Texture2D/SenCiddimisinReis/YCTP/*.png`
- Create: `Assets/Texture2D/SenCiddimisinReis/Menu/*.png`
- Modify: `Assets/AnimationClip/*.anim`
- Modify: `Assets/AnimationClip/AnimatorController/*.controller`
- Modify: `Assets/Scene/MainMenu.unity`
- Modify: `Assets/PrefabInstance/MathGame.prefab`
- Modify: `Assets/PrefabInstance/MathGameNoKeypad.prefab`
- Create: `tools/validate_character_assets.sh`

**Interfaces:**
- Consumes: The supplied `61257.png` character reference and existing Baldi animation timing/state machines.
- Produces: Alpha-enabled normal, wave, talk, angry/slap, menu, and YCTP sprites referenced by all Baldi presentation surfaces.

- [ ] **Step 1: Write a failing asset/reference validator**

```bash
#!/usr/bin/env bash
set -euo pipefail
root=Assets/Texture2D/SenCiddimisinReis
for group in Source Gameplay YCTP Menu; do test -d "$root/$group"; done
find "$root" -name '*.png' -type f | rg -q '.'
! rg -q 'Texture2D/(CharacterSprites|YCTPTextures)/Baldi/' Assets/Scene/MainMenu.unity Assets/PrefabInstance/MathGame*.prefab
```

- [ ] **Step 2: Run it and confirm failure**

Run: `bash tools/validate_character_assets.sh`

Expected: FAIL because the new asset tree is absent.

- [ ] **Step 3: Produce transparent and posed sprites**

Use the supplied image as the visual reference. Remove the black background, preserve the full sombrero/body/feet, and prepare coherent pose variants for idle, talk, wave, angry, and ruler-slap states. Keep pixel-art edges crisp, RGBA transparency real, and use the same character identity in every variant.

- [ ] **Step 4: Replace serialized sprite bindings**

Import the variants with point filtering, no mipmaps, transparent alpha, and consistent pixels-per-unit. Preserve existing animation clip timing and animator transitions while redirecting object references to the new sprite GUIDs. Update main-menu and both math prefabs to use the new assets directly.

- [ ] **Step 5: Run asset validation and reference scans**

Run: `bash tools/validate_character_assets.sh`

Expected: PASS, with every required asset group populated and no checked surface retaining a Baldi asset path.

- [ ] **Step 6: Commit**

```bash
git add Assets/Texture2D/SenCiddimisinReis Assets/AnimationClip Assets/Scene/MainMenu.unity Assets/PrefabInstance tools/validate_character_assets.sh
git commit -m "feat: replace Baldi presentation with animated Reis character"
```

### Task 3: Add the two-second notebook notification

**Files:**
- Create: `Assets/Scripts/Core/UI/NotebookNotification.cs`
- Create: `Assets/Tests/EditMode/NotebookNotificationTests.cs`
- Modify: `Assets/Scripts/Core/GameControllerScript.cs`
- Modify: `Assets/Scene/School.unity`

**Interfaces:**
- Consumes: `GameControllerScript.CollectNotebook()` notebook event.
- Produces: `NotebookNotification.Show()` and `NotebookNotification.VisibleUntil` with a two-second unscaled lifetime.

- [ ] **Step 1: Write failing EditMode tests**

```csharp
[Test]
public void Show_UsesExactTextAndTwoSecondLifetime()
{
    var notification = NotebookNotification.CreateForTest();
    notification.Show(10f);
    Assert.AreEqual("SEN CİDDİ MİSİN?", notification.CurrentText);
    Assert.AreEqual(12f, notification.VisibleUntil, 0.001f);
}

[Test]
public void Show_RestartsLifetimeOnNextNotebook()
{
    var notification = NotebookNotification.CreateForTest();
    notification.Show(10f);
    notification.Show(11f);
    Assert.AreEqual(13f, notification.VisibleUntil, 0.001f);
}
```

- [ ] **Step 2: Run tests and confirm failure**

Run with the installed Unity 6 editor in batch mode against `SenCiddimisinReis.EditMode`.

Expected: FAIL because `NotebookNotification` does not exist.

- [ ] **Step 3: Implement the focused notification component**

Implement `Show(float unscaledNow)` to set the exact text, activate the centered UI, and set `VisibleUntil = unscaledNow + 2f`. In `Update`, hide the UI when `Time.unscaledTime >= VisibleUntil`. Repeated calls restart the deadline without duplicating UI objects.

- [ ] **Step 4: Connect it once to notebook collection**

Add a serialized `NotebookNotification notebookNotification` field to `GameControllerScript` and call `notebookNotification.Show(Time.unscaledTime)` exactly once inside `CollectNotebook()`. Add the safe-area-centered UI object to `School.unity` using the existing pixel-style font.

- [ ] **Step 5: Run tests and static trigger scan**

Run the EditMode tests, then run: `test "$(rg -c 'notebookNotification\.Show' Assets/Scripts/Core/GameControllerScript.cs)" -eq 1`

Expected: Tests PASS and the call count equals one.

- [ ] **Step 6: Commit**

```bash
git add Assets/Scripts/Core/UI/NotebookNotification.cs Assets/Tests Assets/Scripts/Core/GameControllerScript.cs Assets/Scene/School.unity
git commit -m "feat: show Sen Ciddi Misin notification on notebooks"
```

### Task 4: Integrate touch input without changing gameplay

**Files:**
- Create: `Assets/Scripts/Mobile/MobileInputState.cs`
- Create: `Assets/Scripts/Mobile/MobileLookArea.cs`
- Create: `Assets/Scripts/Mobile/MobileActionButton.cs`
- Create: `Assets/Tests/EditMode/MobileInputStateTests.cs`
- Modify: `Assets/Scripts/PlayerFunctions/PlayerMovement.cs`
- Modify: `Assets/Scripts/PlayerFunctions/PlayerScript.cs`
- Modify: `Assets/Scripts/PlayerFunctions/ItemFunctions/NotebookScript.cs`
- Modify: `Assets/Scripts/PlayerFunctions/ItemFunctions/EndlessNotebookScript.cs`
- Modify: `Assets/Scene/School.unity`

**Interfaces:**
- Consumes: Joystick/look/button values from the supplied mobile control package plus existing keyboard/mouse axes.
- Produces: `MobileInputState.Move`, `LookDelta`, `RunHeld`, and one-frame `InteractPressed` values; desktop and mobile inputs are combined by the existing gameplay scripts.

- [ ] **Step 1: Write failing input-state tests**

```csharp
[Test]
public void ConsumeInteract_ReturnsTrueOnlyOnce()
{
    var input = new MobileInputState();
    input.PressInteract();
    Assert.IsTrue(input.ConsumeInteract());
    Assert.IsFalse(input.ConsumeInteract());
}

[Test]
public void ReleaseRun_ClearsHeldState()
{
    var input = new MobileInputState();
    input.SetRun(true);
    input.SetRun(false);
    Assert.IsFalse(input.RunHeld);
}
```

- [ ] **Step 2: Run tests and confirm failure**

Expected: FAIL because `MobileInputState` does not exist.

- [ ] **Step 3: Import and inspect the mobile package**

Extract only Unity assets from `mobilecontrollfolder.rar`; reject executables, editor binaries, caches, or duplicate project settings. Preserve original package metadata when valid.

- [ ] **Step 4: Implement the input adapter**

Implement normalized move/look state, held run state, and one-shot interaction consumption. Modify player scripts to combine legacy axes with touch values while keeping existing desktop behavior unchanged.

- [ ] **Step 5: Wire landscape UI controls**

Place movement at bottom-left, look drag across the right-side free area, and run/interact buttons at bottom-right. Anchor controls within the safe area and enable the canvas only for touch-capable/mobile builds.

- [ ] **Step 6: Run tests and scene validation**

Run EditMode tests, then verify that `School.unity` serializes exactly one mobile input state, one movement control, one look area, one run button, and one interact button.

- [ ] **Step 7: Commit**

```bash
git add Assets/Scripts/Mobile Assets/Tests/EditMode/MobileInputStateTests.cs Assets/Scripts/PlayerFunctions Assets/Scene/School.unity
git commit -m "feat: add Android touch controls"
```

### Task 5: Validate, clean, and package the deliverable

**Files:**
- Modify: `tools/validate_project.sh`
- Create: `README-SenCiddimisinReis.md`
- Create: `BaldisCiddimisinV1.zip` outside the project directory.

**Interfaces:**
- Consumes: Completed Unity project and all validation scripts/tests.
- Produces: A clean, reopenable project archive.

- [ ] **Step 1: Extend final validation**

Add checks for required scenes, scripts, character groups, exact title strings, exact notification text, Android settings, absence of duplicate notification triggers, and absence of `Library`, `Temp`, `Logs`, and `obj` in the archive list.

- [ ] **Step 2: Run all available verification**

Run: `bash tools/validate_project.sh && bash tools/validate_character_assets.sh`

If Unity 6 is installed, also run batchmode EditMode tests and a no-output Android build validation. Any unavailable editor-level check must be reported explicitly rather than represented as passed.

- [ ] **Step 3: Add concise project instructions**

Document the required Unity 6 editor, Android Build Support modules, the opening scene, mobile layout, and build target in `README-SenCiddimisinReis.md`.

- [ ] **Step 4: Create and inspect the archive**

Run from the project parent directory:

```bash
zip -qr BaldisCiddimisinV1.zip 'Sen Ciddimisin Reis' \
  -x '*/Library/*' '*/Temp/*' '*/Logs/*' '*/obj/*' '*/.git/*' '*/.vscode/*' '*.csproj' '*.sln'
unzip -t BaldisCiddimisinV1.zip
unzip -Z1 BaldisCiddimisinV1.zip | rg 'Sen Ciddimisin Reis/(Assets|Packages|ProjectSettings)/'
```

Expected: Archive integrity PASS and all three required Unity project directories present.

- [ ] **Step 5: Commit source changes**

```bash
git add tools README-SenCiddimisinReis.md
git commit -m "docs: add build and validation instructions"
```
