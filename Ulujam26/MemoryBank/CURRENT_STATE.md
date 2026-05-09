# CHROMAVOID - Current State

## Summary
Proje artık pre-implementation aşamasından çalışan prototype mimarisi aşamasına geçti. Unity projesi, URP ve Starter Assets Third Person altyapısı korunuyor; CHROMAVOID'a özel gameplay kodları `Assets/_Project/` altında bağımsız ve sahneye bağlanabilir component'ler olarak eklendi.

Bu dosya her büyük değişiklik sonrası güncellenmelidir.

## Şu An Projede Ne Var
| Alan | Durum |
| --- | --- |
| Unity proje kurulumu | Var |
| Unity version | `6000.3.11f1` |
| URP | Var, `com.unity.render-pipelines.universal` `17.3.0` |
| Input System | Var, `com.unity.inputsystem` `1.19.0` |
| Starter Assets | ThirdPersonController, Mobile input ve Environment assetleri var |
| Scene | `Assets/Scenes/SampleScene.unity` |
| Settings | `Assets/Settings/` altında URP renderer/profile assetleri var |
| Shader denemeleri | `Assets/_AshAndPause/shadermat/` altında shader graph ve materyaller var |
| MemoryBank | Dokümantasyon sistemi mevcut ve güncelleniyor |
| CHROMAVOID prototype scripts | `Assets/_Project/Scripts/` altında eklendi |

## Hangi Sistemler Çalışıyor
- Unity proje açılabilir durumdadır.
- URP ayarları ve render pipeline assetleri projede mevcuttur.
- Starter Assets Third Person Controller paketi projeye import edilmiştir.
- Input action assetleri ve starter input scriptleri mevcuttur.
- SampleScene ve Starter Assets Playground scene dosyaları mevcuttur.
- Tile state sistemi kod seviyesinde çalışmaya hazır: Colored, Fading, Black.
- GridManager sahnedeki fiziksel tile objelerini kaydedebilir ve tile sorguları yapabilir.
- SpawnManager colored tile seçip fanus enemy spawn edebilir.
- EnemyContainer havadan hedef tile'a iner, tile fading başlatır, kill ile tile restore eder.
- WaveManager sonsuz wave döngüsü ve zorluk artışı başlatabilir.
- ScoreManager kill, rescued tile, survival ve wave score hesaplayabilir.
- SimpleRaycastWeapon crosshair merkezinden fanus vurabilir.
- PlayerTileDetector black tile giriş/çıkış event hook'larını yayınlayabilir.
- GameManager black tile oranı eşiğiyle Game Over log/event tetikler.
- `Playground.unity` içinde MainCamera, PlayerArmature altında FPS kamera pozisyonuna taşındı ve `FPSCameraAdapter` ile mouse look alacak şekilde bağlandı.
- Starter Assets `ThirdPersonController` geriye uyumlu bir `UseFpsStrafeMovement` modu kazandı; Playground instance'ında açık. A/D artık karakteri döndürmez, strafe yapar.
- `Playground.unity` içindeki kırık `PlayerFollowCamera` scene root referansı (`1402827283`) temizlendi.
- `_Project` script `.meta` dosyaları Unity `MonoImporter` formatına tamamlandı ve BOM'suz UTF-8 olarak normalize edildi; MainCamera üzerindeki `FPSCameraAdapter` script referansı artık GUID/meta tarafında çözülebilir durumda.

## Hangi Sistemler Placeholder
- Oyun adı ve konsepti artık dokümantasyon + başlangıç kod mimarisi seviyesindedir.
- Tile/grid sistemi sahne wiring bekleyen prototype kod olarak var.
- Enemy/fanus sistemi sahne wiring ve prefab görseli bekleyen prototype kod olarak var.
- Spawn/wave sistemi prototype kod olarak var.
- Weapon/raycast sistemi prototype kod olarak var.
- Score/UI/game over sistemi prototype kod olarak var.
- Shader denemeleri production asset olarak organize edilmedi.

## Eksik Assetler
- CHROMAVOID ana player prefab varyantı.
- Fanus enemy prefabı.
- Tile prefabları.
- Arena prefab/scene düzeni.
- Weapon model veya muzzle VFX.
- Hit/death/corruption VFX.
- UI prefabları.
- Audio feedback seti.

## Bilinen Eksikler
- İstenen üretim kökü `Assets/_Project/` olarak kuruldu.
- Gameplay scriptleri var; scene wiring henüz yapılmadı.
- Scene içinde CHROMAVOID loop'unu çalıştıran manager yapısı yok.
- Tile state görselleri production shader/material olarak ayrılmadı.
- Test/validation scene'i yok.
- Build hedefi ve platform ayarları netleştirilmedi.

## Temporary Implementations
- Starter Assets olduğu gibi duruyor; FPS/arena shooter davranışı için `_Project/Scripts/Player/FPSCameraAdapter.cs` eklendi.
- Starter Assets `PlayerFollowCamera` Cinemachine prefab instance'ı `Playground.unity` sahnesinden kaldırıldı; prefab asset silinmedi. MainCamera prefab instance'ındaki CinemachineBrain component'i de sahne override'ı ile kaldırıldı. Cinemachine package eksik olduğu için aktif kamera kontrolü MainCamera + FPSCameraAdapter üzerinden ilerliyor.
- Shader Graph dosyaları deneme isimleriyle `_AshAndPause/shadermat` altında duruyor.
- SampleScene gerçek gameplay scene'i yerine başlangıç sahnesi olarak kabul ediliyor.
- SpawnManager prefab verilmezse runtime placeholder sphere fanus üretebilir.

## Son Implement Edilen Feature
- `Assets/_Project/` altında CHROMAVOID başlangıç gameplay mimarisi oluşturuldu.
- Tile/grid, enemy/fanus, spawn, wave, score, weapon, FPS camera adapter, player tile detector ve debug HUD scriptleri eklendi.
- `Assets/StarterAssets/ThirdPersonController/Scenes/Playground.unity` FPS camera test sahnesi olarak güncellendi.
- ThirdPerson movement rotasyon çakışması çözüldü: karakter yaw kontrolü mouse adapter'da, movement yönü FPS strafe modunda.
- Unity warning hotfix: broken text PPtr ve MainCamera missing script hatalarına sebep olan sahne root/meta problemleri düzeltildi.
- `StarterAssetsThirdPerson.controller` sabit `RifleAimingIdle` tek-state controller olarak temizlendi; eski jump/fall transition kalıntılarından gelen broken PPtr hataları kaldırıldı.
- `Assets/_Project/Animations/Rifle Aiming Idle.fbx` Mixamo silah tutma idle animasyonu ana test pose'u olarak kullanılacak.
- `Playground.unity` RigBuilder sadeleştirildi; aktif rig layer artık yalnızca `WeaponRig`. Boş/eski `Rig 1` layer RigBuilder listesinden çıkarıldı.
- Animation Rigging bind hatası nedeniyle `Playground.unity` üzerindeki `RigBuilder` geçici olarak disabled yapıldı. Tekrar açmadan önce rig objeleri Animator hierarchy içinde doğru yere taşınmalı veya karakter FBX optimize hierarchy kapatılmalı.
- El/silah tutuşu için Animation Rigging yerine `HumanoidWeaponHandIK` eklendi. Sistem Unity Humanoid `OnAnimatorIK` kullanır; oyuncu sadece silah üzerindeki `RightHandGrip` ve `LeftHandGrip` transformlarını ayarlayarak ellerin gideceği noktayı belirler.
- `StarterAssetsThirdPerson.controller` üzerinde IK Pass açıldı (`m_IKPass: 1`).
- MemoryBank Session 002 oluşturuldu.

## Son Çalışan Build Summary
Henüz CHROMAVOID gameplay build'i alınmadı. Kod altyapısı eklendi ancak Unity sahnesinde prefab/material/manager referansları bağlanarak editör playtest yapılması gerekiyor.

## Güncelleme Kuralı
Her büyük değişiklik sonrası şu alanlar güncellenmelidir:

- Yeni çalışan sistemler.
- Placeholder'dan production'a geçen sistemler.
- Eklenen/eksilen assetler.
- Bilinen bug ve teknik borçlar.
- Son çalışan build veya playtest sonucu.
