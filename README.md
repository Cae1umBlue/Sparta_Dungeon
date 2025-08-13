<div align="center">
<img width="1081" height="834" alt="스크린샷 2025-08-13 112526" src="https://github.com/user-attachments/assets/36746935-f543-40d1-a0a5-3e06f553c1c2" />
</div>

---

<div align="center">
<img src="https://img.shields.io/badge/Unity-222324?style=flat&logo=unity&logoColor=white"/>
<img src="https://img.shields.io/badge/-C%23-663399?logo=Csharp&style=flat&logoColor=white"/>
</div>

---

# 스파르타 던전(SpartaDungeon)

## 목차
[프로젝트 개요](#프로젝트-개요)

[기술 스택](#기술-스택)

[프로젝트 구조](#프로젝트-구조)

[구현 목록](#구현목록)

[트러블 슈팅](#트러블-슈팅)

[에셋 출처](#에셋-출처)

---

## 프로젝트 개요

| 항목            | 내용                                   |
|-----------------|--------------------------------------|
| **게임명**       | Sparta Dungeon                         |
| **장르**         | 3D, 플랫포머(Platformer) |
| **개발 기간**    | 2025.08.08(금) ~ 2025.08.13(수)       |
| **타겟 플랫폼**  | PC             |

조작법
- 이동 : WASD
- 점프 : Spacebar
- 상호작용 : E
- 인벤토리 : Tab
- 아이템 선택 상호작용 : 마우스 클릭, 사용/버리기 버튼

---

## 기술 스택

| 항목            | 내용                                   |
|-----------------|--------------------------------------|
| **Language**    | C#                                   |
| **Engine**      | Unity 2022.3.2f1                     |
| **IDE**         | Visual Studio 2022                   |
| **Verson Control**  | Git, GitHub              |
| **Library**  | TextMeshPro              |

---
## 프로젝트 구조

<pre>
Assets/
├── 00.Scenes/                # 씬 파일 저장
├── 01.Scripts/              # 주요 스크립트 폴더
│   ├── Character/           # 캐릭터 관련 스크립트
│   │   ├── CharacterManager/
│   │   ├── Player/
│   │   ├── PlayerCondition/
│   │   └── PlayerController/
│   ├── Item/                # 아이템 관련 스크립트
│   │   ├── ItemObject/
│   │   └── ItemSlot/
│   ├── Map/                 # 맵 관련 기능
│   │   ├── DamageZone/
│   │   ├── JumpZone/
│   │   └── SimpleGemsAnim/
│   ├── ScriptableObject/   # 스크립터블 오브젝트
│   │   └── Data/
│   │       └── ItemData/
│   └── UI/                  # UI 관련 스크립트
│       ├── Condition/
│       ├── DamageIndicator/
│       ├── UICondition/
│       └── UIInventory/
├── 02.Prefabs/              # 프리팹 저장
├── Externals/               # 외부 리소스
├── InputAction/             # 입력 액션 설정
├── Material/                # 머티리얼 파일
├── TextMesh Pro/            # 텍스트 관련 리소스
Packages/                    # 패키지 설정
</pre>

---

## 구현목록
**필수기능**
- **기본 이동 및 점프**
  
  <img width="559" height="380" alt="image" src="https://github.com/user-attachments/assets/2e27e017-b3d4-4d9a-a952-40a1462e91c0" />
  
  - InputAction 기능을 사용하여 외부 입력을 처리
  - Behavior - Invoke Unity Events를 사용하여 인스펙터에서 직접 이벤트 함수 연결
- **체력바 UI**

  ![녹음 2025-08-13 120406](https://github.com/user-attachments/assets/cd95b98d-6bff-4b58-af97-f1799980e62f)

  -  데미지를 받을 경우 체력 감소
  
- **동적 환경 조사**

  ![녹음 2025-08-13 121353](https://github.com/user-attachments/assets/7db27059-5aec-45a2-bd8c-874a2d723aa6)

  - Ray, Raycast를 사용하여 collider가 달려있는 아이템의 prompt를 UI에 출력
  
- **점프대**

  ![녹음 2025-08-13 122240](https://github.com/user-attachments/assets/38fff31f-25d2-44b8-85ba-0a9afd4a8169)

  - JumpZone.cs 를 점프대 오브젝트에 부착
  - OnCollisionEnter를 사용하여 플레이어와의 접촉 확인
  - Rigidbody - AddForce - ForceMode.Impulse 를 사용하여 점프 구현
    
- **아이템 데이터**

   <img width="571" height="640" alt="image" src="https://github.com/user-attachments/assets/a281b3d5-85f1-403c-8fd9-1da3812ab97e" />

  - [CreatAssetMenu]를 이용해 ItemData.cs에서 ScriptableObject 데이터 객체를 생성
    
- **아이템 사용**

  ![녹음 2025-08-13 124643](https://github.com/user-attachments/assets/bf1af62d-17b1-4f6b-88e4-1b2c10da1152)

  - UI - Button 을 생성하여 OnClick 이벤트를 통해 아이템 사용/버리기 기능을 구현


**도전기능**
- **추가 UI**

  ![녹음 2025-08-13 130007](https://github.com/user-attachments/assets/ee475a86-3457-4eb2-90a2-06236c581bce)

  - 스태미나 UI 구현
  - 점프 시 스태미나 감소
    
- **다양한 아이템 구현**
  
    ![녹음 2025-08-13 125047](https://github.com/user-attachments/assets/86a117bb-dae8-4155-8c6b-512c89a31d3b)
  - Coroutine을 사용하여 이동속도 증가 후 5초 뒤에 원래 이동속도로 복귀할 수 있는 스피드 부스트 아이템 구현
 
---

## 트러블 슈팅
자세한 내용은 [트러블슈팅 문서](TroubleShooting.md)를 참고하세요.

---

## 에셋 출처
- 맵 : https://assetstore.unity.com/packages/3d/environments/free-modular-3d-platformer-pack-319018
- 아이템 : https://assetstore.unity.com/packages/3d/props/simple-gems-and-items-ultimate-animated-customizable-pack-73764
