# Little Paw Town - Data Draft v0.1

---

## 1. 문서 목적

본 문서는 `Little Paw Town GDD v0.1` 기준으로, MVP 구현에 필요한 **핵심 데이터 구조 초안**을 정리한 문서다.

목표는 다음과 같다.
- 저장 데이터와 마스터 데이터를 분리한다.
- 종 확장을 고려한 공통 레이어 / 종별 레이어 구조를 정의한다.
- 이벤트, 애정도, 추억, 하우징 데이터를 실제 구현 가능한 수준으로 초안화한다.

본 문서는 DB 스키마 확정본이 아니라, **게임 데이터 모델 설계 초안**이다.

---

## 2. 데이터 설계 원칙

### 2.1 기본 원칙
- **마스터 데이터**와 **유저 저장 데이터**를 분리한다.
- 가능한 한 공통 이벤트를 재사용하고, 종별 차이는 별도 레이어로 분리한다.
- 애정도와 친밀도는 혼합하지 않는다.
- UI 연출용 텍스트와 로직 조건을 분리한다.
- MVP 단계에서는 과한 정규화보다 **제작/수정 편의성**을 우선한다.

### 2.2 레이어 원칙
- **공통 레이어**: 계정, 반려동물, 애정도, 추억, 관계, 하우징, 재화
- **종별 레이어**: 파츠, 애니메이션 세트, 종별 반응 문구, 종별 이벤트 오버라이드

### 2.3 권장 구현 관점
- Unity 기준으로는 ScriptableObject 기반 마스터 데이터 + JSON/DB 기반 세이브 구조가 적합함
- 서버 연동 시에도 논리 모델은 동일하게 유지 가능하도록 설계

---

## 3. 데이터 분류

## 3.1 마스터 데이터
변하지 않거나 운영자가 제어하는 데이터
- SpeciesMaster
- PartMaster
- TraitMaster
- LocationMaster
- NPCMaster
- EventTemplateMaster
- EventBranchMaster
- EventRewardMaster
- AffectionLevelMaster
- HousingItemMaster
- MiniGameMaster
- PushScenarioMaster
- DialogueTextMaster

### 3.2 유저 저장 데이터
플레이에 따라 변하는 데이터
- PlayerProfile
- PetProfile
- PetAppearance
- PetTraitState
- PetAffectionState
- RelationshipState
- EventHistory
- MemoryCardState
- HomeInventory
- HomePlacement
- WalletState
- MiniGameRecord
- PushInboxState
- SettingsState

---

## 4. 핵심 엔티티 개요

| 영역 | 엔티티 | 역할 |
|---|---|---|
| 계정 | PlayerProfile | 유저 기본 정보 |
| 반려동물 | PetProfile | 반려동물 공통 상태 |
| 반려동물 | PetAppearance | 외형 파츠/컬러 저장 |
| 반려동물 | PetTraitState | 특성 상태 저장 |
| 반려동물 | PetAffectionState | 애정도 누적/단계 저장 |
| 세계 | LocationMaster | 장소 정의 |
| 세계 | NPCMaster | 주민/동물 친구 정의 |
| 관계 | RelationshipState | 반려동물 ↔ NPC 관계 상태 |
| 이벤트 | EventTemplateMaster | 이벤트 원형 |
| 이벤트 | EventBranchMaster | 분기 조건/연출 |
| 기록 | EventHistory | 발생/완료 이력 |
| 기록 | MemoryCardState | 추억 카드 저장 |
| 하우징 | HousingItemMaster | 소품 정의 |
| 하우징 | HomeInventory | 보유 소품 |
| 하우징 | HomePlacement | 배치 정보 |
| 경제 | WalletState | 재화 저장 |
| 플레이 | MiniGameRecord | 미니게임 결과 |
| 운영 | PushScenarioMaster | 푸시형 돌발 이벤트 템플릿 |

---

## 5. 마스터 데이터 초안

## 5.1 SpeciesMaster
종 정보.

| 필드 | 타입 | 설명 |
|---|---|---|
| species_id | string | 종 ID |
| name_ko | string | 종 명칭 |
| enabled | bool | 사용 여부 |
| body_type_key | string | 기본 체형 키 |
| animation_set_id | string | 기본 애니메이션 세트 |
| ui_icon | string | UI 아이콘 리소스 키 |
| default_voice_set | string | 기본 사운드 세트 |
| notes | string | 운영 메모 |

### 예시
- dog
- cat

> MVP에서는 `dog`만 enabled여도 구조상 `cat`을 추가할 수 있게 유지한다.

---

## 5.2 PartMaster
외형 파츠 정의.

| 필드 | 타입 | 설명 |
|---|---|---|
| part_id | string | 파츠 ID |
| species_id | string | 대상 종 |
| part_category | enum | ear, eye, tail, pattern, accessory 등 |
| name | string | 파츠 이름 |
| sprite_key | string | 리소스 키 |
| sort_order | int | 렌더 순서 |
| colorable | bool | 컬러 변경 가능 여부 |
| default_unlock | bool | 기본 해금 여부 |
| enabled | bool | 사용 여부 |

---

## 5.3 TraitMaster
공통 특성 정의.

| 필드 | 타입 | 설명 |
|---|---|---|
| trait_id | string | 특성 ID |
| name_ko | string | 특성명 |
| min_value | int | 최소 단계 |
| max_value | int | 최대 단계 |
| description | string | 설명 |

### MVP 공통 특성
- curiosity
- activity
- sociability
- appetite
- caution

---

## 5.4 LocationMaster
장소 정의.

| 필드 | 타입 | 설명 |
|---|---|---|
| location_id | string | 장소 ID |
| name_ko | string | 장소명 |
| category | enum | home, outdoor, shop, square 등 |
| enabled_mvp | bool | MVP 사용 여부 |
| weather_tags | string[] | 허용 날씨 태그 |
| time_tags | string[] | 강조 시간대 태그 |
| bg_asset_key | string | 배경 리소스 |
| bgm_key | string | 배경음 |

### MVP 장소
- home
- park
- bakery
- plaza

---

## 5.5 NPCMaster
주민 / 동물 친구 정의.

| 필드 | 타입 | 설명 |
|---|---|---|
| npc_id | string | 캐릭터 ID |
| npc_type | enum | resident, animal_friend |
| name_ko | string | 이름 |
| location_default | string | 주 활동 장소 |
| personality_tags | string[] | 성격 태그 |
| portrait_key | string | 리소스 키 |
| enabled_mvp | bool | MVP 사용 여부 |

---

## 5.6 EventTemplateMaster
이벤트 원형 정의. 가장 중요.

| 필드 | 타입 | 설명 |
|---|---|---|
| event_id | string | 이벤트 ID |
| title | string | 내부용 제목 |
| category | enum | environment, habit, relation, affection, chain |
| enabled | bool | 사용 여부 |
| repeatable | bool | 반복 가능 여부 |
| location_ids | string[] | 발생 가능 장소 |
| species_scope | string[] | 허용 종 목록 또는 all |
| weather_scope | string[] | 허용 날씨 |
| time_scope | string[] | 허용 시간 |
| affection_min_level | int | 최소 애정도 단계 |
| affection_max_level | int | 최대 애정도 단계 |
| weight | int | 출현 가중치 |
| branch_group_id | string | 분기 그룹 |
| memory_card_group | string | 추억 카드 분류 |
| followup_group_id | string | 후속 이벤트 그룹 |
| reward_profile_id | string | 기본 결과 세트 |
| cooldown_hours | int | 재발생 제한 시간 |

---

## 5.7 EventBranchMaster
이벤트 조건 분기.

| 필드 | 타입 | 설명 |
|---|---|---|
| branch_id | string | 분기 ID |
| event_id | string | 소속 이벤트 |
| condition_type | enum | trait, species, relationship, first_time, choice |
| condition_key | string | 조건 키 |
| operator | enum | eq, gte, lte, contains |
| condition_value | string | 조건 값 |
| action_text_key | string | 분기 텍스트 키 |
| animation_key | string | 연출 키 |
| priority | int | 우선순위 |

> 이벤트 하나가 모든 특성을 보지 않도록 제한한다. 일반적으로 1~2개 조건만 사용.

---

## 5.8 EventRewardMaster
이벤트 결과 정의.

| 필드 | 타입 | 설명 |
|---|---|---|
| reward_profile_id | string | 결과 세트 ID |
| affection_point | int | 애정도 포인트 |
| relationship_delta_json | json | 관계 변화 |
| money_delta | int | 머니 변화 |
| item_reward_ids | string[] | 지급 아이템 |
| memory_card_template_id | string | 추억 카드 템플릿 |
| unlock_flags | string[] | 해금 플래그 |

---

## 5.9 AffectionLevelMaster
애정도 단계 정의.

| 필드 | 타입 | 설명 |
|---|---|---|
| level | int | 단계 |
| name_ko | string | 단계명 |
| required_point | int | 도달 필요 포인트 |
| unlock_reaction_tags | string[] | 반응 해금 태그 |
| unlock_event_groups | string[] | 이벤트 해금 그룹 |
| unlock_home_behavior_tags | string[] | 홈 행동 해금 |

### 예시 단계
- 0: 낯섦
- 1: 익숙함
- 2: 편안함
- 3: 신뢰
- 4: 깊은 애착

---

## 5.10 HousingItemMaster
하우징 소품 정의.

| 필드 | 타입 | 설명 |
|---|---|---|
| item_id | string | 아이템 ID |
| name_ko | string | 이름 |
| category | enum | furniture, deco, toy, wall, floor |
| price_money | int | 가격 |
| placeable | bool | 배치 가능 여부 |
| interaction_tag | string | 행동 반응 태그 |
| size_x | int | 가로 크기 |
| size_y | int | 세로 크기 |
| enabled_mvp | bool | MVP 사용 여부 |

---

## 5.11 MiniGameMaster
미니게임 정의.

| 필드 | 타입 | 설명 |
|---|---|---|
| minigame_id | string | 게임 ID |
| name_ko | string | 이름 |
| enabled | bool | 사용 여부 |
| reward_money_base | int | 기본 보상 |
| affection_support_point | int | 보조 애정도 |
| cooldown_min | int | 재도전 제한 |

---

## 5.12 PushScenarioMaster
알림형 돌발 이벤트 템플릿.

| 필드 | 타입 | 설명 |
|---|---|---|
| push_id | string | 푸시 ID |
| title_text_key | string | 제목 키 |
| body_text_key | string | 본문 키 |
| trigger_type | enum | idle, time_based, event_followup |
| cooldown_hours | int | 중복 방지 시간 |
| related_event_group | string | 연계 이벤트 그룹 |
| affection_bonus_on_open | int | 진입 시 추가 애정도 |
| enabled | bool | 사용 여부 |

---

## 6. 유저 저장 데이터 초안

## 6.1 PlayerProfile
유저 기본 정보.

| 필드 | 타입 | 설명 |
|---|---|---|
| player_id | string | 유저 ID |
| account_type | enum | guest, apple, google 등 |
| created_at | datetime | 생성일 |
| last_login_at | datetime | 최근 접속 |
| tutorial_cleared | bool | 튜토리얼 완료 여부 |
| timezone | string | 타임존 |
| locale | string | 언어 |

---

## 6.2 PetProfile
반려동물 공통 프로필.

| 필드 | 타입 | 설명 |
|---|---|---|
| pet_id | string | 반려동물 ID |
| player_id | string | 소유 유저 |
| name | string | 이름 |
| species_id | string | 종 ID |
| created_at | datetime | 생성일 |
| current_location_id | string | 현재 장소 |
| current_state | enum | idle, resting, exploring 등 |
| preferred_tags | string[] | 선호 태그 |
| system_flags | string[] | 진행 플래그 |

> MVP는 반려동물 1마리만 가정하되, 구조상 player:pet = 1:N으로 두는 편이 이후 확장에 유리하다.

---

## 6.3 PetAppearance
반려동물 외형 저장.

| 필드 | 타입 | 설명 |
|---|---|---|
| pet_id | string | 반려동물 ID |
| base_color | string | 기본 색상 |
| eye_part_id | string | 눈 파츠 |
| ear_part_id | string | 귀 파츠 |
| tail_part_id | string | 꼬리 파츠 |
| pattern_part_id | string | 무늬 파츠 |
| accessory_part_id | string | 소품 파츠 |
| custom_palette_json | json | 사용자 컬러 값 |

---

## 6.4 PetTraitState
특성 상태 저장.

| 필드 | 타입 | 설명 |
|---|---|---|
| pet_id | string | 반려동물 ID |
| curiosity | int | 1~3 |
| activity | int | 1~3 |
| sociability | int | 1~3 |
| appetite | int | 1~3 |
| caution | int | 1~3 |

---

## 6.5 PetAffectionState
애정도 저장.

| 필드 | 타입 | 설명 |
|---|---|---|
| pet_id | string | 반려동물 ID |
| affection_point | int | 누적 포인트 |
| affection_level | int | 현재 단계 |
| today_interaction_point | int | 일일 누적 포인트 |
| last_gain_at | datetime | 최근 상승 시간 |
| unlocked_affection_flags | string[] | 해금 반응/장면 |

### 비고
- MVP에서는 `감소 포인트` 필드를 두지 않는 편을 권장
- 필요 시 운영 실험용으로만 별도 필드 추가 가능

---

## 6.6 RelationshipState
NPC 관계 저장.

| 필드 | 타입 | 설명 |
|---|---|---|
| pet_id | string | 반려동물 ID |
| npc_id | string | 대상 NPC |
| intimacy_point | int | 관계 포인트 |
| intimacy_level | int | 관계 단계 |
| unlocked_flags | string[] | 해금 상태 |
| last_event_at | datetime | 최근 관계 이벤트 |

---

## 6.7 EventHistory
이벤트 발생/완료 이력.

| 필드 | 타입 | 설명 |
|---|---|---|
| history_id | string | 이력 ID |
| pet_id | string | 반려동물 ID |
| event_id | string | 이벤트 ID |
| branch_id | string | 적용 분기 |
| choice_key | string | 선택지 키 |
| occurred_at | datetime | 발생 일시 |
| completed | bool | 완료 여부 |
| result_snapshot_json | json | 결과 스냅샷 |

### 사용 목적
- 동일 이벤트 반복 제어
- 후속 이벤트 트리거
- 추억 카드 생성 근거 추적
- 분석 로그 기초 데이터

---

## 6.8 MemoryCardState
추억 카드 저장.

| 필드 | 타입 | 설명 |
|---|---|---|
| memory_id | string | 추억 ID |
| pet_id | string | 반려동물 ID |
| event_id | string | 원본 이벤트 |
| title_text | string | 카드 제목 |
| body_text | string | 카드 본문 요약 |
| image_key | string | 썸네일 리소스 |
| location_id | string | 장소 |
| weather_tag | string | 날씨 |
| time_tag | string | 시간대 |
| created_at | datetime | 획득 시각 |
| favorite | bool | 즐겨찾기 |
| album_tag | string | 앨범 분류 |

---

## 6.9 WalletState
재화 저장.

| 필드 | 타입 | 설명 |
|---|---|---|
| player_id | string | 유저 ID |
| money | int | 기본 머니 |
| emotional_token | int | 감성 토큰 |
| updated_at | datetime | 변경 시각 |

> MVP에서는 `money`만 실사용, `emotional_token`은 reserve 처리 가능.

---

## 6.10 HomeInventory
보유 소품 저장.

| 필드 | 타입 | 설명 |
|---|---|---|
| inventory_id | string | 인벤토리 ID |
| player_id | string | 유저 ID |
| item_id | string | 아이템 ID |
| quantity | int | 수량 |
| acquired_at | datetime | 획득 시각 |

---

## 6.11 HomePlacement
홈 배치 저장.

| 필드 | 타입 | 설명 |
|---|---|---|
| placement_id | string | 배치 ID |
| player_id | string | 유저 ID |
| item_id | string | 아이템 ID |
| pos_x | int | 위치 X |
| pos_y | int | 위치 Y |
| rotation | int | 회전 |
| layer_order | int | 레이어 |
| placed | bool | 배치 여부 |

> 자유 배치형이 부담되면 MVP에서는 `slot_id` 기반으로 단순화 가능.

---

## 6.12 MiniGameRecord
미니게임 결과.

| 필드 | 타입 | 설명 |
|---|---|---|
| record_id | string | 기록 ID |
| player_id | string | 유저 ID |
| pet_id | string | 반려동물 ID |
| minigame_id | string | 게임 ID |
| score | int | 점수 |
| reward_money | int | 획득 머니 |
| affection_bonus | int | 애정도 보조 |
| played_at | datetime | 플레이 시각 |

---

## 6.13 PushInboxState
푸시/돌발 이벤트 상태.

| 필드 | 타입 | 설명 |
|---|---|---|
| inbox_id | string | ID |
| player_id | string | 유저 ID |
| push_id | string | 시나리오 ID |
| sent_at | datetime | 발송 시각 |
| opened_at | datetime | 열람 시각 |
| resolved | bool | 후속 처리 여부 |

---

## 6.14 SettingsState
설정 데이터.

| 필드 | 타입 | 설명 |
|---|---|---|
| player_id | string | 유저 ID |
| sound_on | bool | 사운드 설정 |
| bgm_on | bool | 배경음 설정 |
| push_on | bool | 푸시 허용 |
| language | string | 언어 |

---

## 7. 이벤트 데이터 상세 구조 제안

## 7.1 권장 구조
이벤트는 단일 테이블 하나로 해결하기보다 아래 4개 층으로 관리하는 것이 적절하다.

1. `EventTemplateMaster`
2. `EventBranchMaster`
3. `DialogueTextMaster`
4. `EventRewardMaster`

### 이유
- 텍스트 수정과 로직 수정의 충돌을 줄일 수 있음
- 종별 문구 오버라이드가 쉬워짐
- 후속 이벤트/애정도 조정이 분리됨

---

## 7.2 DialogueTextMaster 초안
| 필드 | 타입 | 설명 |
|---|---|---|
| text_key | string | 텍스트 키 |
| owner_type | enum | event, npc, system |
| species_id | string | 대상 종 또는 all |
| locale | string | 언어 |
| title_text | string | 제목 |
| body_text | string | 본문 |
| emotion_tag | string | 감정 태그 |

---

## 7.3 예시 이벤트 흐름

### 예시: bakery_sniff_01
- Template
  - 장소: bakery
  - 카테고리: environment
  - 종 범위: all
- Branch A
  - 조건: species=dog
  - 텍스트: 빵 냄새에 신나서 꼬리를 흔듦
- Branch B
  - 조건: species=cat
  - 텍스트: 멀찍이 냄새를 확인하다가 슬쩍 다가감
- Reward
  - affection +5
  - memory card 생성

이 구조면 공통 이벤트를 유지하면서 종별 반응만 얹을 수 있다.

---

## 8. 애정도 데이터 설계 제안

## 8.1 저장 최소 단위
- 누적 포인트
- 현재 단계
- 오늘 획득량
- 해금 플래그

### 이유
- 단계 계산을 빠르게 할 수 있음
- 일일 효율 제한 실험이 가능함
- 단계별 반응/이벤트 해금을 제어할 수 있음

---

## 8.2 권장 계산 방식
- `affection_point`는 누적 총량
- 단계는 `AffectionLevelMaster.required_point` 기준 계산
- 일일 상한 대신 `today_interaction_point`에 따른 효율 감소 방식 고려 가능

### 예시
- 0~19: 낯섦
- 20~49: 익숙함
- 50~99: 편안함
- 100~179: 신뢰
- 180+: 깊은 애착

> 수치는 설계 초안이며 밸런싱 필요.

---

## 9. 분석/로그 관점 최소 수집 항목

MVP라도 아래 항목은 꼭 남기는 편이 좋다.

| 로그 항목 | 목적 |
|---|---|
| 생성 완료 여부 | 생성 이탈 확인 |
| 장소 이동 기록 | 루프 사용성 확인 |
| 이벤트 발생/완료 | 핵심 콘텐츠 소비 확인 |
| 애정도 상승 원천 | 상승 구조 검증 |
| 추억 카드 생성/열람 | 감정 보상 검증 |
| 하우징 진입/배치 | 메타 체류 동기 확인 |
| 미니게임 플레이 | 보조 루프 유효성 확인 |

---

## 10. 구현 단순화 권장안

### 10.1 MVP에서 단순화 가능한 부분
- 종별 세부 특성 테이블 분리 생략
- HomePlacement 자유 좌표 대신 슬롯 배치 채택 가능
- EventBranch 조건식 DSL 대신 단순 key-value 조건 구조 사용 가능
- 대사 로컬라이징은 1언어 우선으로 시작 가능

### 10.2 단순화하면 안 되는 부분
- 애정도와 친밀도 분리
- 이벤트 원형과 종별 분기 분리
- 추억 카드와 이벤트 이력 분리
- 플레이어 데이터와 마스터 데이터 분리

---

## 11. 권장 ID 규칙

| 대상 | 예시 |
|---|---|
| species_id | dog, cat |
| location_id | home, park, bakery, plaza |
| npc_id | npc_baker_mina |
| event_id | evt_bakery_sniff_01 |
| branch_id | br_evt_bakery_sniff_01_dog |
| reward_profile_id | reward_evt_bakery_sniff_01_a |
| item_id | home_sofa_01 |
| minigame_id | mg_ball_play |

---

## 12. 오픈 이슈

### 12.1 추후 확정 필요
- 반려동물 생성 시 특성을 직접 고를지, 질문형/숨은 결정형으로 할지
- 홈 배치를 자유 좌표형으로 할지 슬롯형으로 할지
- 푸시형 돌발 이벤트를 서버 생성형으로 할지, 클라이언트 템플릿형으로 할지
- 이벤트 결과 저장 스냅샷 범위를 어디까지 둘지

### 12.2 권장 판단
- MVP는 **수정이 쉬운 구조**가 최우선
- 데이터 구조가 지나치게 일반화되면 실제 제작 속도가 느려질 수 있음
- 종 확장은 “미리 다 만들기”가 아니라 “나중에 넣을 수 있게만 해두기”가 적절함

---

## 13. 최종 정리

본 초안의 핵심은 다음이다.
- 저장 데이터와 마스터 데이터를 분리한다.
- 애정도/관계/추억/하우징을 별도 축으로 저장한다.
- 이벤트는 공통 원형 + 종별 분기 + 결과 세트로 관리한다.
- MVP는 1종 우선 출시를 가정하되, species_id 중심 구조로 확장 여지를 남긴다.

이 정도 구조면 MVP 제작 속도를 크게 해치지 않으면서도, 이후 **고양이 추가 및 기타 종 확장**에 대응 가능하다.
