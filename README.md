# Swipe Platformer Prototype

# 작성 에디터 버전 : Unity 2022.3.62f1

---

## 📌 프로젝트 소개

Unity로 제작한 2D 플랫폼 게임 프로토타입입니다.
플레이어 이동, 점프, 애니메이션, 카메라 시스템, 타이머, UI까지 포함하여
기본적인 게임 플레이 구조를 완성했습니다.

v4에서는 단순 기능 구현을 넘어서
각 시스템을 연결하고, 다양한 게임 상황(시간 제한, 강제 스크롤 등)을 처리할 수 있도록 확장했습니다.

---

## 🎮 주요 기능

* 좌/우 이동 (Input.GetAxisRaw 기반)
* 스프라이트 방향 전환 (localScale)
* 점프 시스템 (Rigidbody2D Impulse)
* 입력 / 물리 처리 분리 (Update / FixedUpdate)
* 바닥 체크 (Physics2D Linecast)
* 상태 기반 애니메이션 시스템
* 게임 상태 관리 (playing / gameclear / gameover / gameend)
* Goal / Dead 트리거 기반 이벤트 처리
* 카메라 추적 + 이동 제한 시스템
* 카메라 강제 스크롤 (자동 이동)
* 패럴랙스 효과 (배경 원근감)
* 타이머 시스템 (카운트다운 / 카운트업)
* 시간 종료 시 게임오버 처리
* UI (결과 화면 / 타이머 표시)

---

## 🧠 핵심 시스템 설명

### 🎯 Player Controller

* 입력(Update)과 물리(FixedUpdate) 분리 구조
* goJump 플래그로 입력 → 물리 타이밍 동기화
* 상태 기반으로 전체 행동 제어

---

### 🎬 Animation System

* 현재/이전 상태 비교 방식
* 변경 시에만 애니메이션 실행 (중복 방지)

---

### 🧩 Game State

* string 기반 상태 관리
* 게임 흐름 제어 핵심

```csharp
playing → gameclear / gameover → gameend
```

---

### ⏱ Timer System

* TimeController를 통한 시간 관리
* 카운트다운 / 카운트업 모두 지원

```csharp
displayTime = gameTime - times;
```

* 시간 종료 시 자동 GameOver 연결

---

### 🎥 Camera System

#### 1. 플레이어 추적

* Player 위치 기반 카메라 이동

#### 2. 이동 제한

* left / right / top / bottom 범위 제한

#### 3. 강제 스크롤

```csharp
x = transform.position.x + (forceSpeedX * Time.deltaTime);
```

👉 플레이어와 무관하게 카메라 자동 이동

---

### 🌄 Parallax (패럴랙스)

* 배경을 카메라보다 느리게 이동시켜 깊이감 표현

```csharp
x / 2.0f
```

👉 멀리 있는 배경일수록 더 느리게 움직임

---

## 🧾 주요 구조

* playerController.cs
  → 이동 / 점프 / 애니메이션 / 상태

* GameManager.cs
  → UI / 게임 상태 처리 / 타이머 연동

* CameraManager.cs
  → 카메라 추적 / 강제 스크롤 / 패럴랙스

* TimeController.cs
  → 시간 관리 (카운트다운/업)

* ItemData.cs
  → 아이템 데이터 저장

---

## 🛠 사용 기술

* Unity (2022.3.62f1)
* C#
* Physics2D (Rigidbody2D, Linecast)
* Animator
* UI (Text, Image, Button)

---

## 📁 특징

* 입력 / 물리 / 상태 분리 구조
* 시스템 간 연결 (Player ↔ Manager ↔ Timer)
* 다양한 게임 방식 대응 가능

  * 일반 플랫폼
  * 시간 제한 게임
  * 자동 스크롤 스테이지
* 애니메이션 최적화 구조
* 패럴랙스를 통한 원근감 구현

---

## ⚠️ 개선 예정

* string → enum 상태 관리 전환
* GameObject.Find 제거 및 캐싱
* 카메라 Lerp 적용 (부드러운 이동)
* 패럴랙스 다중 레이어 확장
* 코드 책임 분리 (Manager 분리)
* 이벤트 기반 구조 적용

---

## 📌 상태

* v1 ~ v4 구현 완료
* 현재 구조 개선 및 최적화 진행 중
