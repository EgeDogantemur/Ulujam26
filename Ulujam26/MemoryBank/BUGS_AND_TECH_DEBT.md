# CHROMAVOID - Bugs and Tech Debt

## Summary
Bu dosya bilinen bugları, eksikleri ve teknik borçları takip eder. Proje henüz pre-implementation aşamasında olduğu için kayıtların çoğu "eksik sistem" veya "organizasyon borcu" niteliğindedir.

## Known Issues
| Bug Description | Severity | Temporary Workaround | Proper Future Solution | Owner |
| --- | --- | --- | --- | --- |
| CHROMAVOID gameplay loop henüz yok | Critical | Starter Assets scene üzerinden prototip başlat | Core manager, grid, enemy, weapon ve wave sistemlerini kur | Optional |
| Ana gameplay scene'i yok | Critical | `SampleScene` geçici başlangıç noktası | `Assets/_Project/Scenes/` altında dedicated arena scene oluştur | Optional |
| Tile/grid sistemi yok | Critical | Manuel zemin veya primitive cube testleri | Tile prefab + GridManager + TileCorruptionController implementasyonu | Optional |
| Enemy/fanus prefabı yok | Critical | Primitive sphere/capsule ile test | Enemy component seti ve fanus visual prefab | Optional |
| Weapon sistemi yok | Critical | Debug raycast test scripti | Config destekli raycast weapon sistemi | Optional |
| UI/game over yok | High | Console log ile test | Minimal HUD ve game over panel | Optional |

## Tech Debt
| Description | Severity | Temporary Workaround | Proper Future Solution | Owner |
| --- | --- | --- | --- | --- |
| Shader denemeleri `_AshAndPause/shadermat` altında isimlendirme standardı dışında | Medium | Mevcut assetleri referans olarak bırak | Production shader/materialleri `Assets/_Project/Materials` altına taşı; shader klasörü gerekiyorsa `_Project/Shaders` aç | Optional |
| Starter Assets ve oyun kodu ayrımı sahne/prefab seviyesinde tamamlanmadı | Medium | Starter Assets'e dokunmadan `_Project` scriptlerini kullan | Player prefab adapter/wiring işlemlerini `_Project` altında tut | Optional |
| Config sistemi yok | Medium | Serialized field ile tuning | ScriptableObject tabanlı Wave/Enemy/Weapon/Tile configs | Optional |
| Object pooling yok | Medium | İlk prototipte instantiate/destroy | Enemy/VFX pool sistemi | Optional |
| Event sözleşmeleri henüz kodda tanımlı değil | Medium | Doğrudan manager referanslarıyla kısa prototip | Ortak event yapısı veya gameplay event hub | Optional |
| Build/playtest kaydı yok | Low | Editör testleri | Her milestone sonrası build summary'yi `CURRENT_STATE.md` içine yaz | Optional |

## Tracking Rules
- Yeni bug bulunduğunda severity net yazılmalı: Critical, High, Medium, Low.
- Temporary workaround gerçekçi olmalı; "fix later" tek başına yeterli değildir.
- Proper future solution kalıcı çözüm yönünü tarif etmelidir.
- Fixlenen buglar silinmek yerine gerekirse "Resolved" bölümüne taşınabilir.
