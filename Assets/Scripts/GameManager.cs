using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private List<Request> m_requests;
    private IntervalTimer m_villagerTimer;
    private GameObject m_villagerSpawn;
    private GameObject m_villager;

    public GameObject villagerPrefab;

    public void Awake()
    {
        m_villagerSpawn = transform.Find("VillagerSpawn").gameObject;

        m_requests = new List<Request>();

        m_villagerTimer = GetComponent<IntervalTimer>();
        m_villagerTimer.StartTimer(SpawnVillager);
    }

    public void Update()
    {
    }

    private void SpawnVillager()
    {
        if (m_villager == null)
        {
            m_villager = Instantiate(this.villagerPrefab,
                                     m_villagerSpawn.transform.position,
                                     Quaternion.identity) as GameObject;

            var request = Request.Random();

            var controller = m_villager.GetComponent<VillagerController>();
            controller.MakeRequest(request);
        }
    }
}
