using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class GameMaster : StaticReference<GameMaster>
{
    [System.Serializable]
    public class GameState
    {
        public string key;
        public UnityEvent eventsOnThisState;
        public List<string> informationRequirements;
        public string nextStateKey;
    }

    [SerializeField] private List<GameState> gameStates;
    [SerializeField] private string initialGameStateKey;
    [SerializeField] private GameState currentGameState;

    private void Awake()
    {
        BaseAwake(this);
        currentGameState = gameStates.Find(element => element.key == initialGameStateKey);

        //JumpToState("a");
    }


    public void TryProgressState()
    {
        var requirementFulfilled = CheckRequirement();

        if(requirementFulfilled)
        {
            print("====> PROGRESS STATE");
            ProgressState();
        } else
        {
            print("not progressing state...");
        }
    }

    private bool CheckRequirement()
    {
        bool result = false;

        var currentInformations = InformationSystem.Instance().GetInformations();
        if (currentInformations.All(element => currentGameState.informationRequirements.Contains(element)))
        {
            result = true;
        }

        return result;
    }

    private void ProgressState()
    {
        var nextGameState = GetNextState();
        if (nextGameState == null)
        {
            print("ERROR");
            return;
        }

        currentGameState = nextGameState;
        currentGameState.eventsOnThisState.Invoke();
    }

    private GameState GetNextState()
    {
        return gameStates.Find(element => element.key == currentGameState.nextStateKey);
    }





    // Helper, debugging
    private void JumpToState(string stateKey)
    {
        while(currentGameState.key != stateKey)
        {
            var nextGameState = GetNextState();
            currentGameState = nextGameState;
            currentGameState.eventsOnThisState.Invoke();
        }
    }

    private void OnDestroy()
    {
        BaseOnDestroy();
    }
}
