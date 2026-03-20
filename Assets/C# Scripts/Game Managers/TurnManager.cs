using System;
using UnityEngine;
using Unity.Netcode;


namespace Fire_Pixel.Networking
{
    /// <summary>
    /// MB manager class that tracks player on turn GameId through <see cref="ClientManager"/> GameId System. Also has callback event for OnTurnChanged and OnTurn -Started and -Ended
    /// </summary>
    public class TurnManager : SmartNetworkBehaviour
    {
        public static TurnManager Instance { get; private set; }
        private void Awake() => Instance = this;


        [SerializeField] private GameObject endTurnButtonObj;

        private int clientOnTurnId = -1;
        public static int ClientOnTurnId => Instance.clientOnTurnId;

        public static bool IsMyTurn => Instance.clientOnTurnId == LocalClientGameId;

#pragma warning disable UDR0001
        public static event Action<int> TurnChanged;
        public static event Action TurnStarted;
        public static event Action TurnEnded;
#pragma warning restore UDR0001


        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsServer)
            {
                SwapToNextTurn_ClientRPC(-1, 0);
            }
        }

        [ServerRpc(RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
        public void NextTurn_ServerRPC()
        {
            int prevClientOnTurnId = clientOnTurnId;
            clientOnTurnId.IncrementSmart(GlobalGameData.MAX_PLAYERS);

            SwapToNextTurn_ClientRPC(prevClientOnTurnId, clientOnTurnId);
        }
        [ClientRpc(RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
        private void SwapToNextTurn_ClientRPC(int prevClientOnTurnId, int nextClientOnTurnId)
        {
            clientOnTurnId = nextClientOnTurnId;

            // Invoke OnTurnChanged with new clientId.
            TurnChanged?.Invoke(clientOnTurnId);

            // If it becomes or stays local clients turn, Invoke OnMyTurnStarted.
            if (IsMyTurn)
            {
                endTurnButtonObj.SetActive(true);
                TurnStarted?.Invoke();
            }
            // If its not local clients turn, check if they lost the turn and Invoke OnTurnEnded if so.
            else if (prevClientOnTurnId == LocalClientGameId)
            {
                TurnEnded?.Invoke();
            }
        }
    }
}