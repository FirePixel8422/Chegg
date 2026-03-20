using Fire_Pixel.Utility;
using UnityEngine;
using Unity.Netcode;


namespace Fire_Pixel.Networking
{
    public class MatchManager : SmartNetworkBehaviour
    {
        [SerializeField] private MatchSettings matchSettings;
        [SerializeField] private string nextSceneName;

        private int playerReadyCount;



        protected override void OnNetworkSystemsSetup()
        {
            MarkPlayerReady_ServerRPC();
        }

        [ServerRpc(RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
        public void MarkPlayerReady_ServerRPC()
        {
            playerReadyCount += 1;
            if (true || playerReadyCount == GlobalGameData.MAX_PLAYERS)
            {
                OnMatchLoaded_RPC(matchSettings);
                SceneManager.LoadSceneOnNetwork_OnServer(nextSceneName);
            }
        }

        [Rpc(SendTo.ClientsAndHost, Delivery = RpcDelivery.Reliable)]
        private void OnMatchLoaded_RPC(MatchSettings matchSettings)
        {
            this.matchSettings = matchSettings;
        }
    }
}