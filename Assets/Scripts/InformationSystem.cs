using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationSystem : StaticReference<InformationSystem>
{
    [SerializeField] private List<string> informations = new List<string>();

    private void Awake()
    {
        BaseAwake(this);
    }

    public void AddInformation(string informationKey)
    {
        if (HasInformation(informationKey)) 
        { 
            print($"INFORMATION: duplication warning! failed ({informationKey})");
            return;
        }

        informations.Add(informationKey);
        print($"INFORMATION: {informationKey} added to informations");
    }

    public bool HasInformation(string informationKey)
    {
        return informations.Contains(informationKey);
    }


    private void OnDestroy()
    {
        BaseOnDestroy();
    }
}
