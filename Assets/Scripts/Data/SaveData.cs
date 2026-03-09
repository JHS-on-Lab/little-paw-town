using System;
using System.Collections.Generic;

namespace LittlePawTown.Data
{
    // ─────────────────────────────────────────────────────────────
    // Root save container  (파일 1개로 직렬화)
    // ─────────────────────────────────────────────────────────────
    [Serializable]
    public class SaveData
    {
        public PlayerSaveData      player      = new();
        public PetSaveData         pet         = new();
        public PetAppearanceSaveData appearance = new();
        public PetTraitSaveData    trait        = new();
        public PetAffectionSaveData affection   = new();
        public WalletSaveData      wallet       = new();
        public List<RelationshipSaveData>  relationships = new();
        public List<EventHistorySaveData>  eventHistory  = new();
        public List<MemoryCardSaveData>    memoryCards   = new();
        public List<HomeInventorySaveData> homeInventory = new();
        public List<HomePlacementSaveData> homePlacements= new();
    }

    // ─────────────────────────────────────────────────────────────
    // Player
    // ─────────────────────────────────────────────────────────────
    [Serializable]
    public class PlayerSaveData
    {
        public string playerId       = "";
        public string accountType    = "guest";
        public bool   tutorialCleared= false;
        public string timezone       = "";
        public string locale         = "ko";
        public string lastLoginAt    = "";
        public string createdAt      = "";
    }

    // ─────────────────────────────────────────────────────────────
    // Pet
    // ─────────────────────────────────────────────────────────────
    [Serializable]
    public class PetSaveData
    {
        public string petId             = "";
        public string name              = "";
        public string speciesId         = "";
        public string currentLocationId = "home";
        public string currentState      = "idle";
        public string createdAt         = "";
        public List<string> systemFlags = new();
    }

    [Serializable]
    public class PetAppearanceSaveData
    {
        public string baseColor        = "";
        public string eyePartId        = "";
        public string earPartId        = "";
        public string tailPartId       = "";
        public string patternPartId    = "";
        public string accessoryPartId  = "";
        public string customPaletteJson= "{}";
    }

    [Serializable]
    public class PetTraitSaveData
    {
        public int curiosity   = 2;
        public int activity    = 2;
        public int sociability = 2;
        public int appetite    = 2;
        public int caution     = 2;
    }

    [Serializable]
    public class PetAffectionSaveData
    {
        public int    affectionPoint          = 0;
        public int    affectionLevel          = 0;
        public int    todayInteractionPoint   = 0;
        public string lastGainAt             = "";
        public List<string> unlockedFlags     = new();
    }

    // ─────────────────────────────────────────────────────────────
    // Wallet
    // ─────────────────────────────────────────────────────────────
    [Serializable]
    public class WalletSaveData
    {
        public int money          = 0;
        public int emotionalToken = 0;   // MVP 예비
    }

    // ─────────────────────────────────────────────────────────────
    // Relationship (NPC 친밀도)
    // ─────────────────────────────────────────────────────────────
    [Serializable]
    public class RelationshipSaveData
    {
        public string npcId          = "";
        public int    intimacyPoint  = 0;
        public int    intimacyLevel  = 0;
        public string lastEventAt    = "";
        public List<string> unlockedFlags = new();
    }

    // ─────────────────────────────────────────────────────────────
    // Event History
    // ─────────────────────────────────────────────────────────────
    [Serializable]
    public class EventHistorySaveData
    {
        public string historyId          = "";
        public string eventId            = "";
        public string branchId           = "";
        public string locationId         = "";
        public string npcId              = "";
        public string choiceKey          = "";
        public string occurredAt         = "";
        public bool   completed          = false;
        public int    affectionDelta     = 0;
        public int    moneyDelta         = 0;
    }

    // ─────────────────────────────────────────────────────────────
    // Memory Card
    // ─────────────────────────────────────────────────────────────
    [Serializable]
    public class MemoryCardSaveData
    {
        public string memoryId      = "";
        public string eventId       = "";
        public string historyId     = "";
        public string titleText     = "";
        public string bodyText      = "";
        public string imageKey      = "";
        public string locationId    = "";
        public string weatherTag    = "";
        public string timeTag       = "";
        public string albumTag      = "";
        public bool   favorite      = false;
        public string createdAt     = "";
    }

    // ─────────────────────────────────────────────────────────────
    // Housing
    // ─────────────────────────────────────────────────────────────
    [Serializable]
    public class HomeInventorySaveData
    {
        public string inventoryId = "";
        public string itemId      = "";
        public int    quantity    = 0;
        public string acquiredAt  = "";
    }

    [Serializable]
    public class HomePlacementSaveData
    {
        public string placementId = "";
        public string itemId      = "";
        public string slotId      = "";   // MVP: 슬롯형
        public int    posX        = 0;
        public int    posY        = 0;
        public int    rotation    = 0;
        public int    layerOrder  = 0;
        public bool   placed      = true;
    }
}
