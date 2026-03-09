using System;

namespace LittlePawTown.Data
{
    [Serializable]
    public class SettingsSaveData
    {
        public bool   soundOn  = true;
        public bool   bgmOn    = true;
        public bool   pushOn   = true;
        public string language = "ko";
    }
}
