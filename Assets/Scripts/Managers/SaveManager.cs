using System;
using System.IO;
using UnityEngine;
using LittlePawTown.Data;
using LittlePawTown.Utils;

namespace LittlePawTown.Core
{
    /// <summary>
    /// JSON 로컬 저장/로드 담당. DontDestroyOnLoad 싱글톤.
    /// 저장 위치: Application.persistentDataPath/save.json
    /// </summary>
    public class SaveManager : Singleton<SaveManager>
    {
        private SaveData _data;
        private SettingsSaveData _settings;

        private string SavePath     => Path.Combine(Application.persistentDataPath, Constants.SaveFileName);
        private string SettingsPath => Path.Combine(Application.persistentDataPath, Constants.SettingsFileName);

        // ── Public Accessors ───────────────────────────────────
        public PlayerSaveData       PlayerData   => _data.player;
        public PetSaveData          PetData      => _data.pet;
        public PetAppearanceSaveData Appearance  => _data.appearance;
        public PetTraitSaveData     TraitData    => _data.trait;
        public PetAffectionSaveData AffectionData=> _data.affection;
        public WalletSaveData       WalletData   => _data.wallet;
        public SaveData             AllData      => _data;
        public SettingsSaveData     Settings     => _settings;

        // ── Load ───────────────────────────────────────────────
        public void Load()
        {
            _data     = LoadFile<SaveData>(SavePath)     ?? new SaveData();
            _settings = LoadFile<SettingsSaveData>(SettingsPath) ?? new SettingsSaveData();

            // 최초 실행 — 플레이어 ID 발급
            if (string.IsNullOrEmpty(_data.player.playerId))
            {
                _data.player.playerId  = Guid.NewGuid().ToString("N");
                _data.player.createdAt = DateTime.UtcNow.ToString("o");
            }

            _data.player.lastLoginAt = DateTime.UtcNow.ToString("o");

            Debug.Log($"[SaveManager] Loaded. Player: {_data.player.playerId}");
        }

        // ── Save ───────────────────────────────────────────────
        public void Save()
        {
            SaveFile(SavePath, _data);
            SaveFile(SettingsPath, _settings);
        }

        // ── Internal ───────────────────────────────────────────
        private static T LoadFile<T>(string path) where T : class
        {
            if (!File.Exists(path)) return null;
            try
            {
                var json = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[SaveManager] Load failed ({path}): {e.Message}");
                return null;
            }
        }

        private static void SaveFile<T>(string path, T data)
        {
            try
            {
                var json = JsonUtility.ToJson(data, prettyPrint: true);
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveManager] Save failed ({path}): {e.Message}");
            }
        }
    }
}
