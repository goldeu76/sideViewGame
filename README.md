# Swipe Platformer Prototype

# 작성 에디터 버전 : Unity 2022.3.62f1

---

## 📌 프로젝트 소개

Unity로 제작한 2D 플랫폼 게임 프로토타입입니다.
플레이어의 이동, 점프, 충돌 판정, 게임 상태 처리 및 애니메이션 전환까지 포함한 기본적인 게임 구조를 구현했습니다.

현재 버전(v2)은 단순 이동 구현을 넘어 상태 기반 로직과 애니메이션 처리 흐름을 구성하는 단계입니다.

---

## 🎮 주요 기능

* 좌/우 이동 (Input.GetAxisRaw 기반 즉각 반응)
* 스프라이트 방향 전환 (localScale)
* 점프 시스템 (Rigidbody2D Impulse Force)
* 점프 입력 분리 (Update → FixedUpdate 구조)
* 바닥 체크 (Physics2D Linecast)
* 상태 기반 애니메이션 전환
* 게임 상태 처리 (playing / gameclear / gameover)
* Goal / Dead 트리거 기반 이벤트 처리

---

## 🧠 핵심 시스템 설명

### 이동

* GetAxisRaw로 즉각적인 입력 반응 처리
* Rigidbody2D.velocity를 이용한 이동 구현
* 방향에 따라 캐릭터 스케일 반전

### 점프

* Update에서 입력 감지 후 goJump 플래그 설정
* FixedUpdate에서 실제 물리 적용
* onGround 상태일 때만 점프 가능

### 바닥 체크

* Linecast를 이용해 캐릭터 아래 충돌 검사
* groundLayer 기준으로 바닥 여부 판단

### 애니메이션

* 상태에 따라 애니메이션 분기 (Idle / Move / Jump 등)
* 이전 상태와 비교하여 변경 시에만 실행

### 게임 상태

* string 기반 상태 관리
* playing 상태에서만 입력 및 물리 처리 허용
* Goal / Dead 충돌 시 상태 변경

---

## 🧾 주요 구조

* playerController.cs
  → 이동, 점프, 애니메이션, 상태 처리 통합 관리

* Update
  → 입력 처리

* FixedUpdate
  → 물리 연산 및 애니메이션 상태 결정

* OnTriggerEnter2D
  → 게임 이벤트 처리 (클리어 / 사망)

---

## 🛠 사용 기술

* Unity (2022.3.62f1)
* C#
* Physics2D (Rigidbody2D, Linecast)
* Animator

---

## 📁 특징

* 입력 / 물리 / 상태 로직 분리 구조
* 애니메이션 중복 실행 방지 처리
* 상태 기반 게임 흐름 제어
* 기본 플랫폼 게임 구조 구현

---

## ⚠️ 개선 예정

* string → enum 기반 상태 관리 전환
* Player / GameManager 역할 분리
* 애니메이션 처리 구조 개선
* 코드 재사용성 및 구조 정리

---

## 📌 상태

* v1 ~ v2 구현 완료
* 현재 구조 개선 및 리팩토링 진행 중
