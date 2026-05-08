# CHROMAVOID - Current State

## Summary
Proje şu anda pre-implementation aşamasındadır. Unity projesi kurulmuş, URP ve Starter Assets Third Person altyapısı eklenmiş, SampleScene mevcut ve shader/material denemeleri başlamıştır. CHROMAVOID'a özel gameplay scriptleri, prefabları ve ana scene organizasyonu henüz oluşturulmamıştır.

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
| MemoryBank | Bu dokümantasyon sistemiyle başlatıldı |

## Hangi Sistemler Çalışıyor
- Unity proje açılabilir durumdadır.
- URP ayarları ve render pipeline assetleri projede mevcuttur.
- Starter Assets Third Person Controller paketi projeye import edilmiştir.
- Input action assetleri ve starter input scriptleri mevcuttur.
- SampleScene ve Starter Assets Playground scene dosyaları mevcuttur.

## Hangi Sistemler Placeholder
- Oyun adı ve konsepti dokümantasyon seviyesindedir.
- Tile/grid sistemi henüz kodlanmadı.
- Enemy/fanus sistemi henüz kodlanmadı.
- Spawn/wave sistemi henüz kodlanmadı.
- Weapon/raycast sistemi henüz kodlanmadı.
- Score/UI/game over sistemi henüz kodlanmadı.
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
- `Assets/CHROMAVOID/` üretim klasörü henüz yok.
- Gameplay scriptleri yok.
- Scene içinde CHROMAVOID loop'unu çalıştıran manager yapısı yok.
- Tile state görselleri production shader/material olarak ayrılmadı.
- Test/validation scene'i yok.
- Build hedefi ve platform ayarları netleştirilmedi.

## Temporary Implementations
- Starter Assets olduğu gibi duruyor; ileride FPS/arena shooter kontrolüne adapte edilecek.
- Shader Graph dosyaları deneme isimleriyle `_AshAndPause/shadermat` altında duruyor.
- SampleScene gerçek gameplay scene'i yerine başlangıç sahnesi olarak kabul ediliyor.

## Son Implement Edilen Feature
- MemoryBank dokümantasyon sistemi oluşturuldu.
- İlk mimari, design, roadmap, code rules ve session note kayıtları yazıldı.

## Son Çalışan Build Summary
Henüz CHROMAVOID gameplay build'i alınmadı. Mevcut proje Unity editör seviyesinde kurulu kabul edilmektedir. İlk build summary, player movement + arena + temel tile state + enemy spawn loop çalıştığında güncellenmelidir.

## Güncelleme Kuralı
Her büyük değişiklik sonrası şu alanlar güncellenmelidir:

- Yeni çalışan sistemler.
- Placeholder'dan production'a geçen sistemler.
- Eklenen/eksilen assetler.
- Bilinen bug ve teknik borçlar.
- Son çalışan build veya playtest sonucu.

