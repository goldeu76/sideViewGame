# Swipe Platformer Prototype

# 작성 에디터 버전 : Unity 2022.3.62f1

---

## 📌 프로젝트 소개
Unity로 제작한 2D 이동/점프 기반 캐릭터 컨트롤 프로토타입입니다.  
키보드 입력과 Rigidbody2D 물리를 이용해 기본적인 플랫폼 이동 구조를 구현했습니다.

---

## 🎮 주요 기능

- 좌/우 이동 (Input.GetAxisRaw 기반 즉각 반응)
- 스프라이트 방향 전환
- 점프 시스템 (Rigidbody2D Impulse Force)
- 2단 점프 방지 (상태 변수 사용)
- 바닥 체크 (Physics2D Linecast)
- 물리 기반 이동 및 감속 처리

---

## 🧠 핵심 시스템 설명

### 이동
- GetAxisRaw로 즉각적인 입력 반응 처리
- Rigidbody2D.velocity를 통해 이동 구현

### 점프
- Space 입력 시 점프 요청
- onGround 상태일 때만 점프 가능
- AddForce(Impulse) 방식 사용

### 바닥 체크
- Linecast로 캐릭터 아래 일정 거리 검사
- groundLayer에 속한 오브젝트만 감지

---

## 🧾 주요 구조

- PlayerController.cs (이동 / 점프 / 상태 관리)
- Update: 입력 처리
- FixedUpdate: 물리 처리
- 상태 변수 기반 점프 제어 구조

---

## 🛠 사용 기술

- Unity :contentReference[oaicite:0]{index=0} (2022.3.62f1)
- C#
- Physics2D (Rigidbody2D, Linecast)

---

## 📁 특징

- 입력 / 물리 / 상태 로직 분리 구조
- 확장 가능한 2D 컨트롤러 구조
- 플랫폼 기본 이동 시스템 이해용 프로젝트

---

## 📌 상태

- v1 ~ v4 구현 완료
- 현재 기능 기반 정리 및 개선 진행 중
