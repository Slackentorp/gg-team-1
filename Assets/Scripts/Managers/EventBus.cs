using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

public class EventBus : Singleton<EventBus>
{
    [SerializeField]
    private GameObject moth;

    private Dictionary<string, List<LightSourceInput>> lightsourceDictionary =
        new Dictionary<string, List<LightSourceInput>>();
    private Dictionary<string, List<Fragment>> fragmentDictionary =
        new Dictionary<string, List<Fragment>>();



    public void AddLightSourceListerer(string eventName,
        LightSourceInput component)
    {
        List<LightSourceInput> lst;
        if (!lightsourceDictionary.TryGetValue(eventName, out lst))
        {
            lst = new List<LightSourceInput>();
            lst.Add(component);
            lightsourceDictionary.Add(eventName, lst);
        }
        else {
            lst.Add(component);
            lightsourceDictionary[eventName] = lst;
        }
    }

    public void TriggerLightSources(string eventName)
    {
        List<LightSourceInput> lst;
        if (lightsourceDictionary.TryGetValue(eventName, out lst))
        {
            foreach (var light in lst)
            {
                light.Lit = true;
            }
        }
    }

    public void AddFragmentListerer(string eventName,
        Fragment component) {
        List<Fragment> lst;
        if (!fragmentDictionary.TryGetValue(eventName, out lst))
        {
            lst = new List<Fragment>();
            lst.Add(component);
            fragmentDictionary.Add(eventName, lst);
        }
        else
        {
            lst.Add(component);
            fragmentDictionary[eventName] = lst;
        }
        
    }

    public void TriggerFragments(string eventName)
    {
        List<Fragment> lst;
        if (!fragmentDictionary.TryGetValue(eventName, out lst))
        {
            foreach (var fragment in lst)
            {
                fragment.Play();
            }
        }
    }

    public void SetMothPosition(Vector3 position)
    {
        moth.SendMessage("SetMothPosition", position);
    }

    public void SetCameraPosition(Vector3 position)
    {
    //    GameController.Instance.SetCameraTarget(position); 
    }

    public void TriggerStoryBite(BasicBiteTrigger bite)
    {
        if (bite.FixedCamera)
        {
        //    GameObject.FindObjectOfType<CameraController>().SendMessage("SetStoryTarget", bite.FixedCamPos);
        }
    }
}
