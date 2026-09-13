#!/usr/bin/env bash
set -euo pipefail

rg -q '^m_EditorVersion: 6000\.' ProjectSettings/ProjectVersion.txt
rg -q '^  productName: Sen Ciddimisin Reis\?$' ProjectSettings/ProjectSettings.asset
rg -q '^  bundleVersion: 1\.0\.0$' ProjectSettings/ProjectSettings.asset
rg -q '^  AndroidTargetArchitectures: 2$' ProjectSettings/ProjectSettings.asset
rg -q 'com\.ajordiojo\.senciddimisinreis' ProjectSettings/ProjectSettings.asset
test -f Assets/Scene/MainMenu.unity
test -f Assets/Scene/School.unity
