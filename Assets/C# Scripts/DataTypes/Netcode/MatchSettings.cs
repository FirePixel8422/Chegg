using Unity.Netcode;


namespace Fire_Pixel.Networking
{
    [System.Serializable]
    public struct MatchSettings : INetworkSerializable
    {
        public int GetSavedInt(int id)
        {
            return id switch
            {
                0 => RecycleKilledUnits ? 1 : 0,
                _ => -1,
            };
        }
        public void SetIntData(int id, int value)
        {
            switch (id)
            {
                case 0:
                    RecycleKilledUnits = value == 1;
                    break;
                default:
#if UNITY_EDITOR
                    DebugLogger.LogError("Error asigning value in MatchSettings.cs");
#endif
                    break;
            }
        }


        public bool RecycleKilledUnits;


        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref RecycleKilledUnits);
        }
    }
}