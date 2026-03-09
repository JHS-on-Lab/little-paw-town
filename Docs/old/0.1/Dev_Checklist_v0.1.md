# Little Paw Town - 개발 체크리스트 v0.1

> MVP Scope v0.1 기준 / Vertical Slice 순서로 구성
> 우선순위: 🔴 P0 (필수) / 🟡 P1 (권장) / 🟢 P2 (선택/후기)

---

## PHASE 0. 프로젝트 셋업

### 0-1. Unity 프로젝트 구조
- [x] 폴더 구조 정의 및 생성
  - Assets/Scripts / Data / Scenes / Prefabs / Sprites / Audio / Fonts
  - Scripts 하위: Core / Data / UI / Game / Utils / Managers
- [ ] Assembly Definition 설정 (필요 시)
- [ ] URP 2D 기본 설정 확인 (카메라, 렌더러)
- [ ] 해상도/종횡비 기준 확정 (모바일 9:16 기준)
- [x] Git .gitignore 정리

### 0-2. 아키텍처 설계
- [x] 씬 구성 방식 결정 (씬 단위 vs Single Scene + UI Stack)
- [x] GameManager / SceneLoader 기본 구조 작성
- [x] 저장/로드 구조 결정 (JSON 로컬 저장 우선)
- [x] ScriptableObject 마스터 데이터 관리 방식 확정
- [ ] 이벤트/메시지 시스템 방식 결정 (C# event / UnityEvent / 커스텀)

---

## PHASE 1. 데이터 구조 구현 🔴

### 1-1. 마스터 데이터 (ScriptableObject)
- [x] `SpeciesData` SO 작성
- [x] `PartData` SO 작성
- [x] `TraitData` SO 작성
- [x] `LocationData` SO 작성
- [x] `NPCData` SO 작성
- [x] `EventTemplateData` SO 작성
- [x] `EventBranchData` SO 작성
- [x] `EventRewardData` SO 작성
- [x] `AffectionLevelData` SO 작성
- [x] `HousingItemData` SO 작성
- [x] `DialogueTextData` SO 작성
- [x] `MiniGameData` SO 작성
- [x] `PushScenarioData` SO 작성

### 1-2. 유저 저장 데이터 (C# 클래스 + JSON)
- [x] `PlayerSaveData` 클래스
- [x] `PetSaveData` 클래스
- [x] `PetAppearanceSaveData` 클래스
- [x] `PetTraitSaveData` 클래스
- [x] `PetAffectionSaveData` 클래스
- [x] `RelationshipSaveData` 클래스
- [x] `EventHistorySaveData` 클래스
- [x] `MemoryCardSaveData` 클래스
- [x] `HomeInventorySaveData` 클래스
- [x] `HomePlacementSaveData` 클래스
- [x] `WalletSaveData` 클래스
- [x] `SettingsSaveData` 클래스

### 1-3. 저장/로드 시스템
- [x] `SaveManager` 구현 (JSON 직렬화/역직렬화)
- [x] 로컬 저장 경로 및 파일 구조 확정
- [ ] 저장 데이터 무결성 검증 기본 처리

---

## PHASE 2. 반려동물 생성 시스템 🔴

### 2-1. 생성 UI (SCR-012, SCR-013, SCR-014)
- [x] 생성 화면 씬/프리팹 기본 구성
- [x] 이름 입력 필드
- [x] 파츠 선택 UI (귀, 눈, 꼬리, 무늬, 소품)
- [x] 색상 선택 UI (기본 컬러 팔레트)
- [ ] 미리보기 렌더링 (파츠 레이어 조합) - 코드 완성, 스프라이트 에셋 미연결
- [x] 랜덤 생성 버튼
- [x] 특성 설정 화면 (5개 슬라이더)
- [x] 생성 완료 연출 (등장 + 이름 호명)
- [x] 생성 데이터 저장 (PetSaveData, PetAppearanceSaveData, PetTraitSaveData)

### 2-2. 파츠 렌더링 시스템
- [x] 파츠 레이어 구조 설계 (sort_order 기반)
- [ ] 컬러 오버라이드 적용 (Sprite Color 또는 Material) - 코드 완성, 스프라이트 미연결
- [x] 파츠 조합 프리뷰 컴포넌트 (PetRenderer)

---

## PHASE 3. 홈 화면 🔴

### 3-1. 홈 씬 (SCR-020)
- [ ] 홈 배경 기본 구성 (시간대/날씨별 배경)
- [ ] 반려동물 메인 뷰 배치
- [ ] 시간대 판별 로직 (아침/낮/저녁/밤)
- [ ] 날씨 판별 로직 (맑음/비 - 기기 날씨 API 또는 랜덤)
- [ ] 애정도 요약 표시 UI
- [ ] 간단 상호작용 버튼 (쓰다듬기, 간식 주기)
- [ ] 장소 이동 버튼
- [ ] 추억/하우징/상점/설정 진입 버튼

### 3-2. 반려동물 표시
- [ ] 홈에서 반려동물 애니메이션 (idle 기본)
- [ ] 애정도 단계별 홈 행동 변화 (다가오기, 따라다니기 등)
- [ ] 시간/날씨에 따른 기본 반응 연출

### 3-3. 오늘의 상태 패널 (CMP-022)
- [ ] 시간대 + 날씨 표시
- [ ] 반려동물 현재 기분/행동 힌트 표시

---

## PHASE 4. 장소 이동 시스템 🔴

### 4-1. 장소 선택 (SCR-030)
- [ ] 장소 선택 UI (4곳 카드)
- [ ] 현재 시간/날씨 반영 장소 썸네일
- [ ] 장소 전환 애니메이션

### 4-2. 장소 씬 (SCR-031~033)
- [ ] 공원 배경 + 기본 구성
- [ ] 베이커리 배경 + NPC 배치
- [ ] 광장 배경 + 관계 캐릭터 배치
- [ ] 각 장소 내 이벤트 발생 포인트 구성
- [ ] 홈 복귀 버튼

---

## PHASE 5. 이벤트 시스템 🔴

### 5-1. 이벤트 엔진
- [ ] 이벤트 선정 로직 (장소, 시간, 날씨, 애정도, 특성 필터링)
- [ ] 가중치(weight) 기반 랜덤 선정
- [ ] 분기(branch) 조건 평가 로직
  - [ ] species 조건
  - [ ] trait 조건 (1~2개 체크)
  - [ ] affection_level 조건
  - [ ] first_time 조건
  - [ ] choice 조건
- [ ] 이벤트 cooldown 처리
- [ ] 연쇄 이벤트(followup) 트리거 처리

### 5-2. 이벤트 화면 (SCR-040, SCR-041)
- [ ] 이벤트 발생 화면 구성
  - [ ] 상황 문구 표시
  - [ ] 반려동물 표정/행동 연출
  - [ ] NPC 연출 (해당 시)
- [ ] 선택지 모달 (CMP-042) - 2~3개 선택지
- [ ] 이벤트 결과 화면
  - [ ] 결과 요약 문구
  - [ ] 애정도 변화 표시
  - [ ] 추억 카드 획득 알림 (CMP-043)

### 5-3. 이벤트 결과 처리
- [ ] 애정도 포인트 반영 (PetAffectionSaveData 갱신)
- [ ] 애정도 단계 상승 판정 및 해금 처리
- [ ] 머니 변화 반영
- [ ] 관계 친밀도 반영
- [ ] EventHistory 저장
- [ ] 추억 카드 생성 및 저장

### 5-4. 이벤트 원형 데이터 작성
- [ ] 환경 반응 이벤트 10~15개
- [ ] 습관 반응 이벤트 10~15개
- [ ] 관계 이벤트 8~10개
- [ ] 애정도 이벤트 8~10개
- [ ] 연쇄 이벤트 4~5개
- [ ] 각 이벤트에 branch 데이터 연결

---

## PHASE 6. 애정도 시스템 🔴

### 6-1. 애정도 로직
- [ ] 누적 포인트 → 단계 계산 로직
  - 0~19: 낯섦
  - 20~49: 익숙함
  - 50~99: 편안함
  - 100~179: 신뢰
  - 180+: 깊은 애착
- [ ] 일일 상호작용 효율 감소 처리 (today_interaction_point)
- [ ] 단계 상승 시 해금 플래그 처리

### 6-2. 애정도 보상 연출
- [ ] 애정도 상승 팝업 (CMP-021)
- [ ] 단계 상승 특별 연출
- [ ] 단계별 홈 행동 변화 적용

---

## PHASE 7. 추억 시스템 🔴

### 7-1. 추억 카드
- [ ] MemoryCardSaveData 생성 로직 (이벤트 완료 시 자동)
- [ ] 추억 카드 획득 팝업 (CMP-043)
- [ ] 추억 앨범 목록 화면 (SCR-050)
  - [ ] 카드 리스트 표시
  - [ ] 필터 (장소 / 날짜 / 즐겨찾기)
  - [ ] 새 카드 표시
- [ ] 추억 카드 상세 화면 (SCR-051)
  - [ ] 제목 / 본문 / 장소 / 시간 / 날씨 표시
  - [ ] 즐겨찾기 토글

---

## PHASE 8. 관계 시스템 🟡

### 8-1. NPC 데이터
- [x] 주민 2명 데이터 작성 (NPCData SO) - 미나, 톰
- [x] 동물 친구 1마리 데이터 작성 - 코코

### 8-2. 관계 로직
- [ ] RelationshipSaveData 저장/갱신
- [ ] 친밀도 단계 계산
- [ ] 친밀도 단계별 이벤트 해금 조건 적용

### 8-3. 관계 화면
- [ ] 관계 목록 화면 (SCR-052)
- [ ] 관계 상세 (SCR-053) 또는 확장 패널

---

## PHASE 9. 하우징 시스템 🟡

### 9-1. 하우징 데이터
- [x] 하우징 소품 8종 HousingItemData 작성

### 9-2. 홈 꾸미기 편집 (SCR-060)
- [ ] 슬롯 기반 배치 UI
- [ ] 보유 소품 목록 표시
- [ ] 소품 선택 / 배치 / 회수
- [ ] 저장 버튼 → HomePlacementSaveData 갱신
- [ ] 일부 소품이 반려동물 행동 반응 변화 유발하는 로직

---

## PHASE 10. 경제 시스템 🟡

### 10-1. 재화
- [ ] WalletSaveData 저장/갱신
- [ ] 머니 표시 UI 컴포넌트

### 10-2. 상점 (SCR-061)
- [ ] 소품/장난감/간식 탭
- [ ] 아이템 가격 표시 및 구매 버튼
- [ ] 구매 확인 팝업 (CMP-062)
- [ ] HomeInventorySaveData 갱신

---

## PHASE 11. 미니게임 🟡

- [ ] 미니게임 1종 선택 (공놀이 / 숨은 장난감 찾기 / 냄새 추적)
- [ ] 미니게임 선택 화면 (SCR-070)
- [ ] 미니게임 플레이 화면 (SCR-071)
- [ ] 미니게임 결과 화면 (SCR-072)
- [ ] 결과 → 머니 보상 + 애정도 보조 상승 처리
- [ ] MiniGameRecord 저장
- [ ] 미니게임 cooldown 처리
- [ ] (선택) 미니게임 2종째 추가

---

## PHASE 12. 온보딩 🔴

- [ ] 스플래시 화면 (SCR-001)
- [x] 타이틀 화면 (SCR-002)
- [ ] 로그인/게스트 진입 (SCR-003) - 타이틀 모달 통합 가능
- [ ] 튜토리얼 인트로 (SCR-010) - 짧은 문장 2~4장
- [x] 튜토리얼 완료 플래그 저장

---

## PHASE 13. 시스템 / 공통 🟡

- [ ] 로딩/에러 공통 화면 (SCR-091)
- [ ] 설정 화면 (SCR-090) - 사운드/BGM/푸시 on/off
- [ ] SettingsSaveData 저장
- [ ] BGM / 효과음 기본 구성
- [ ] 공통 팝업 컴포넌트

---

## PHASE 14. 돌발 이벤트 / 푸시 🟢

- [ ] PushScenarioData 3~5종 작성
- [ ] 돌발 이벤트 진입 화면 (SCR-080) - 일반 이벤트 화면 재사용
- [ ] 푸시 빈도 제한 로직
- [ ] 동일 유형 쿨다운 처리
- [ ] PushInboxState 저장

---

## PHASE 15. QA / 출시 준비 🔴

### 15-1. 핵심 루프 검증
- [ ] Vertical Slice 1 플레이테스트 (생성→홈→이벤트→추억)
- [ ] 이벤트 40~50개 전체 발생 테스트
- [ ] 애정도 5단계 전체 달성 테스트
- [ ] 저장/로드 무결성 검증

### 15-2. 지표 수집 준비
- [ ] 생성 완료 여부 로그
- [ ] 장소 이동 기록 로그
- [ ] 이벤트 발생/완료 로그
- [ ] 애정도 상승 원천 로그
- [ ] 추억 카드 생성/열람 로그

### 15-3. 최소 출시 조건
- [ ] 이벤트 원형 40개 이상 완성
- [ ] 애정도 5단계 보상 모두 구현
- [ ] 추억 카드 저장/열람 정상 동작
- [ ] 홈 꾸미기 기본 동작
- [ ] 에러 대응 및 크래시 처리
- [ ] 빌드 (iOS / Android) 정상 확인

---

## 개발 진입점 권장 순서

```
PHASE 0 (셋업)
→ PHASE 1 (데이터 구조)
→ PHASE 2 (반려동물 생성)
→ PHASE 3 (홈 화면)
→ PHASE 4 (장소 이동)
→ PHASE 5 (이벤트 시스템)
→ PHASE 6 (애정도)
→ PHASE 7 (추억)
→ [Vertical Slice 1 플레이테스트]
→ PHASE 8~11 (관계/하우징/경제/미니게임)
→ PHASE 12 (온보딩)
→ PHASE 13 (시스템/공통)
→ PHASE 14 (돌발이벤트)
→ PHASE 15 (QA/출시)
```
