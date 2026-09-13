#!/usr/bin/env bash
set -euo pipefail

asset_root="Assets/Texture2D/SenCiddimisinReis"
for group in Source Gameplay YCTP Menu; do
  test -d "$asset_root/$group"
done

for asset in \
  Source/SenCiddimisinReis.png \
  Gameplay/Reis_Idle.png \
  Gameplay/Reis_Wave.png \
  Gameplay/Reis_Talk.png \
  Gameplay/Reis_Angry.png \
  Gameplay/Reis_Slap.png \
  YCTP/Reis_YCTP_Idle.png \
  YCTP/Reis_YCTP_Talk.png \
  Menu/Reis_Menu.png; do
  test -s "$asset_root/$asset"
done

for asset in $(find "$asset_root" -name '*.png' -type f); do
  file "$asset" | rg -q 'PNG image data.*RGBA'
done
