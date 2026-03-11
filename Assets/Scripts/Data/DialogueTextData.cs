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
        [Header("Classification")]
        public DialogueOwnerType ownerType;
        [Tooltip("Species ID if species-specific. Leave empty for common.")]
        public string speciesId;
        public string locale = "ko";

        [Header("Text")]
        public string titleText;
        [TextArea(2, 6)]
        public string bodyText;
        [Tooltip("Emotion tag for facial expression/animation linkage.")]
        public string emotionTag;
    }
}
