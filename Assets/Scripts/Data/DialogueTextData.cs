using UnityEngine;

namespace LittlePawTown.Data
{
    public enum DialogueOwnerType
    {
        Event,
        NPC,
        System,
    }

    /// <summary>
    /// 대사 / 텍스트 리소스 마스터 데이터.
    /// text_key 로 조회하며, 종별 / 로케일별 오버라이드를 지원.
    /// Assets/Data/Events/ 하위 또는 별도 Dialogues 폴더에 생성.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogueText", menuName = "LittlePawTown/Master/DialogueText")]
    public class DialogueTextData : MasterDataBase
    {
        [Header("분류")]
        public DialogueOwnerType ownerType;
        [Tooltip("특정 종 전용이면 종 ID 입력. 비어 있으면 공통.")]
        public string speciesId;
        public string locale = "ko";

        [Header("텍스트")]
        public string titleText;
        [TextArea(2, 6)]
        public string bodyText;
        [Tooltip("표정/애니메이션 연동에 사용할 감정 태그.")]
        public string emotionTag;
    }
}
