# Swipe Platformer Prototype

# 작성 에디터 버전 : Unity 2022.3.62f1

---

## 📌 프로젝트 소개

Unity로 제작한 2D 플랫폼 게임 프로토타입입니다.
플레이어 이동, 점프, 충돌 처리, 애니메이션, 게임 상태 관리, 카메라 시스템까지 포함한 기본적인 게임 구조를 구현했습니다.

v3에서는 기존 기능 구현을 기반으로
구조 개선, 카메라 시스템 확장, 패럴랙스 효과 적용 등을 통해 완성도를 높이는 것을 목표로 했습니다.

---

## 🎮 주요 기능

* 좌/우 이동 (Input.GetAxisRaw 기반)
* 스프라이트 방향 전환 (localScale)
* 점프 시스템 (Rigidbody2D Impulse)
* 입력 / 물리 처리 분리 (Update / FixedUpdate)
* 바닥 체크 (Physics2D Linecast)
* 상태 기반 애니메이션 전환
* 게임 상태 관리 (playing / gameclear / gameover / gameend)
* Goal / Dead 트리거 기반 이벤트 처리
* 카메라 추적 시스템 (이동 제한 포함)
* 패럴랙스 효과 (배경 속도 차이를 통한 원근감 구현)

---

## 🧠 핵심 시스템 설명

### 이동 & 점프

* GetAxisRaw로 즉각적인 입력 처리
* Rigidbody2D.velocity로 이동 구현
* AddForce(Impulse)를 이용한 점프 처리
* Update에서 입력, FixedUpdate에서 물리 적용

---

### 상태 기반 제어

* string 기반 gameState로 전체 흐름 제어
* playing 상태에서만 입력 및 물리 처리 허용
* gameclear / gameover 시 게임 종료 처리

---

### 애니메이션 시스템

* 상태에 따라 애니메이션 분기
* 이전 상태(oldAnime)와 비교하여 변경 시에만 실행

---

### 카메라 시스템

* Player 위치를 기준으로 카메라 추적
* left / right / top / bottom 제한으로 이동 범위 제어

---

### 패럴랙스 (Parallax)

* 배경(subScreen)을 카메라보다 느리게 이동
* x좌표의 일부만 반영하여 깊이감 표현

```csharp
x / 2.0f
```

👉 멀리 있는 배경일수록 더 느리게 움직이는 효과 구현

---

## 🧾 주요 구조

* playerController.cs
  → 이동, 점프, 애니메이션, 상태 처리

* GameManager.cs
  → UI 및 게임 종료 상태 처리

* CameraManager.cs
  → 카메라 추적 및 패럴랙스 처리

---

## 🛠 사용 기술

* Unity (2022.3.62f1)
* C#
* Physics2D (Rigidbody2D, Linecast)
* Animator

---

## 📁 특징

* 입력 / 물리 / 상태 분리 구조
* 상태 기반 게임 흐름 제어
* 애니메이션 최적화 (중복 실행 방지)
* 카메라 이동 제한 시스템
* 패럴랙스를 활용한 원근감 구현

---

## ⚠️ 개선 예정

* string → enum 기반 상태 관리 전환
* GameObject.Find 제거 및 캐싱 구조 적용
* 카메라 부드러운 이동 (Lerp)
* 패럴랙스 다중 레이어 확장
* 코드 역할 분리 강화 (Player / Manager)

---

## 📌 상태

* v1 ~ v3 구현 완료
* 현재 구조 개선 및 최적화 단계 진행 중
