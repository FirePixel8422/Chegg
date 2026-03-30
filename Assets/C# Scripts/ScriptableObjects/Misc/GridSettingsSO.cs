using Unity.Netcode;
using UnityEngine;



[CreateAssetMenu(fileName = "New Grid Settings", menuName = "ScriptableObjects/Grid/GridSettings", order = -1003)]
public class GridSettingsSO : ScriptableObject
{
    public GridSettings Value;
}


[System.Serializable]
public struct GridSettings : INetworkSerializable
{
    public int Width;
    public int Height;
    public int GridLength => Width * Height;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Width);
        serializer.SerializeValue(ref Height);
    }
}