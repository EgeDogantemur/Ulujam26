# CHROMAVOID - Session 003

## Summary
Bu session'da Playground sahnesindeki kamera kontrol problemi incelendi. Starter Assets PlayerFollowCamera'ın Cinemachine bağımlılığı missing script ürettiği görüldü; prototype için MainCamera doğrudan PlayerArmature altında FPS kamera olarak yapılandırıldı.

## Changes
- `Assets/StarterAssets/ThirdPersonController/Scenes/Playground.unity` içinde MainCamera local position `x=0`, `y=1.62`, `z=0.35` yapıldı.
- MainCamera FOV `65` olarak ayarlandı.
- MainCamera prefab instance'ına `FPSCameraAdapter` eklendi.
- `FPSCameraAdapter` player body referansı PlayerArmature root transform'a, camera pivot referansı MainCamera transform'a bağlandı.
- Cinemachine missing script uyarısı veren `PlayerFollowCamera` prefab instance'ı Playground sahnesinden kaldırıldı; prefab asset silinmedi.
- MainCamera prefab instance'ındaki CinemachineBrain component'i Playground sahnesinde removed component override ile kaldırıldı.
- `ThirdPersonController` içine default kapalı FPS strafe modu eklendi.
- Playground PlayerArmature instance'ında `UseFpsStrafeMovement` açıldı.
- Broken scene root reference `1402827283` temizlendi.
- `_Project` script `.meta` dosyaları `MonoImporter` formatına tamamlandı ve BOM'suz UTF-8 olarak normalize edildi; `FPSCameraAdapter` GUID'i MainCamera scene reference ile uyumlu korundu.

## Notes
- Starter Assets scriptleri değiştirilmedi.
- Mouse look artık Cinemachine beklemeden MainCamera üzerinden çalışmalı.
- A/D artık karakteri döndürmemeli; gövde yaw'ı mouse tarafından, hareket yönü FPS strafe tarafından kontrol edilmeli.
- Unity Console'daki `Broken text PPtr` ve `MainCamera missing script` uyarıları için sahne/meta hotfix uygulandı; Editor import sonrası tekrar kontrol edilmeli.
- Karakter gövdesi/kafa mesh'i kameraya çok yakın görünürse bir sonraki adımda first-person visual mask veya head mesh hide çözümü eklenmeli.

## Next Priorities
1. Unity Play Mode'da mouse look doğrula.
2. Kamera karakter mesh'iyle çakışıyor mu kontrol et.
3. Gerekirse PlayerArmature içinde head/upper body renderer visibility ayarı veya FPS arms yaklaşımı planla.
