# Dimension Survivor

Dimension Survivor, Vampire Survivors tarzında bir rogue-like hayatta kalma oyunudur. Unity oyun motoru ile geliştirilmiştir.

## Özellikler

- **Oynanış:** Sonsuz düşman dalgalarına karşı hayatta kalmaya çalışın.
- **Karakter:** Oyuncu hareketleri, animasyonları ve istatistikleri.
- **Düşmanlar:** Farklı türde düşmanlar ve düşman üretim sistemi.
- **Silahlar:** Scriptable Object tabanlı silah sistemi ve çeşitli silah davranışları.
- **Harita:** Tilemap tabanlı harita ve rastgele nesne yerleşimi.
- **Sanat Varlıkları:** Oyuna özel sprite sheet'ler ve animasyonlar.
- **Kullanıcı Arayüzü:** XP barı, oyun sonu ekranı ve temel UI öğeleri.

## Klasör Yapısı

```
Assets/
 ├── Art/                # Sprite sheet'ler, animasyonlar ve görseller
 ├── Prefabs/            # Hazır nesneler
 ├── Resources/          # Oyun içi kaynaklar
 ├── Scenes/             # Oyun sahneleri (örn. Game.unity)
 ├── Scripts/            # Oyun kodları
 │    ├── Player/        # Oyuncu ile ilgili scriptler
 │    ├── Enemy/         # Düşmanlar ile ilgili scriptler
 │    ├── Weapons/       # Silah sistemi ve davranışları
 │    ├── UI/            # Kullanıcı arayüzü scriptleri
 │    └── Map/           # Harita ve çevre ile ilgili scriptler
 └── Scriptable Objects/ # Scriptable Object varlıkları
```

## Kurulum

1. **Unity 2020.3.33f1** veya daha güncel bir LTS sürümü ile projeyi açın.
2. Gerekli paketler otomatik olarak yüklenecektir (TextMesh Pro vb.).
3. `Assets/Scenes/Game.unity` sahnesini açarak oyunu başlatabilirsiniz.

## Katkıda Bulunma

Katkıda bulunmak isterseniz lütfen bir fork oluşturun ve pull request gönderin.

## Lisa
