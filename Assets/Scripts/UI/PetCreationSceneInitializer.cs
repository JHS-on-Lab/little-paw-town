using UnityEngine;
using LittlePawTown.UI.Screens;

namespace LittlePawTown.UI
{
    /// <summary>
    /// PetCreation 씬에 배치. 씬 로드 직후 PetCreationScreen 을 루트로 설정한다.
    /// </summary>
    public class PetCreationSceneInitializer : MonoBehaviour
    {
        [SerializeField] private PetCreationScreen petCreationScreen;

        private void Start()
        {
            var ctx = new PetCreationContext();
            petCreationScreen.SetContext(ctx);
            UIManager.Instance.SetRoot(petCreationScreen, instant: true);
            Debug.Log("[PetCreation] Scene initialized.");
        }
    }
}
