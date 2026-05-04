# Swipe Platformer Prototype v5

# 작성 에디터 버전 : Unity 2022.3.62f1

---

## 📌 프로젝트 소개

Unity로 제작한 2D 플랫폼 액션 프로토타입입니다.
이동 / 점프 / 아이템 / 기믹 / 타이머 / 점수 / 카메라 / 씬 전환까지 포함한 확장형 구조입니다.

---

## 🎮 주요 기능

* 좌/우 이동 (Rigidbody2D 기반)
* 점프 시스템 (Impulse Force)
* 바닥 판정 (Linecast)
* 애니메이션 상태 전환
* 아이템 획득 및 점수 시스템
* 타이머 시스템 (카운트다운 / 카운트업)
* 게임 상태 관리 (playing / gameclear / gameover)
* 씬 전환 기능
* 결과 UI 및 최종 점수 출력
* 기믹 블록 (낙하 / 삭제 / 페이드)
* 카메라 제한 이동 + 강제 스크롤
* 패럴랙스 효과 (배경 시차 이동)

---

## 🧠 핵심 시스템 설명

### 플레이어 이동

* Input.GetAxisRaw 사용
* Rigidbody2D.velocity 기반 이동
* 좌우 반전 (transform.localScale)

### 점프

* Impulse Force 기반 점프
* onGround 상태에서만 점프 가능

### 아이템

* ScoreItem 태그로 충돌 감지
* ItemData.value로 점수 저장
* 획득 시 Player score 증가

### 타이머

* 카운트다운 / 카운트업 지원
* gameTime 기준 제한
* UI Text 출력
* 시간 종료 시 GameOver

### 게임 매니저

* 게임 상태별 UI 전환
* 점수 통합 (스테이지 + 시간 보너스)
* 버튼 활성/비활성 처리

---

## 🧱 기믹 블록

* 플레이어 거리 감지 후 활성화
* Static → Dynamic 전환 (낙하 시작)
* 충돌 시 페이드 아웃 후 삭제

---

## 🎥 카메라 시스템

* 플레이어 추적 + 이동 제한 (좌/우/상/하)
* 강제 스크롤 (자동 진행 스테이지)

### 📌 패럴랙스 효과

배경이 카메라보다 느리게 움직이는 효과

* 가까운 오브젝트 → 빠르게 이동
* 먼 배경 → 느리게 이동

👉 시각적으로 거리감을 만들어 2D에서 3D 느낌을 구현

---

## 🔄 씬 시스템

* SceneManager.LoadScene 사용
* 버튼 기반 씬 이동 구조

---

## 🧾 주요 스크립트

* PlayerController.cs : 이동 / 점프 / 상태 / 점수
* GameManager.cs : UI / 게임 상태 / 점수 관리
* TimeController.cs : 타이머
* CameraManager.cs : 카메라 + 패럴랙스 + 스크롤
* GimmickBlock.cs : 기믹 블록 시스템
* ItemData.cs : 아이템 데이터
* ChangeScene.cs : 씬 전환
* ResultManager.cs : 결과 점수 출력

---

## 🛠 사용 기술

* Unity 2022.3.62f1
* C#
* Rigidbody2D Physics
* Unity UI (Text / Image / Button)
* Scene Management

---

## 📌 상태

* v5 시스템 통합 완료
* 점수 / 시간 / 기믹 / 카메라 / UI 전체 연결
* 이후 구조 최적화 및 enum 기반 리팩토링 예정
