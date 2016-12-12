using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RequestQueue : MonoBehaviour
{
    private List<Request> m_requests;
    private RecipeBook m_recipeBook;

    public GameObject prefab;
    public float interval = 120f;
    public float elapsed  = 0f;
    public int limit = 5;

    public bool IsFull
    {
        get { return m_requests.Count >= this.limit; }
    }

    public bool IsEmpty
    {
        get { return m_requests.Count == 0; }
    }

    public bool IsReady
    {
        get { return this.elapsed >= this.interval; }
    }

    public void Start()
    {
        m_requests = new List<Request>();
        m_recipeBook = GameObject.Find("Recipe Book")
            .GetComponent<RecipeBook>();
    }

    public void Update()
    {
        UpdatePositions();
        UpdateSpawning();
        UpdateRequests();
    }

    private void UpdatePositions()
    {
        const float offset = -1.7f;

        for (int i = m_requests.Count - 1; i >= 0; --i)
        {
            var child = transform.GetChild(i);
            child.localPosition = new Vector3(0f, i * offset, 0f);
        }
    }

    private void UpdateSpawning()
    {
        if (IsFull) return;

        if (IsReady || IsEmpty)
        {
            SpawnRequest();
        }

        this.elapsed += Time.deltaTime;
    }

    private void UpdateRequests()
    {
        var cull = m_requests.Where(r => r.IsExpired).ToList();
        foreach (var request in cull)
        {
            var go = request.gameObject;
            m_requests.Remove(request);
            Destroy(go);
        }
    }

    private void SpawnRequest()
    {
        var requestGO = Instantiate(this.prefab, this.transform)
            as GameObject;
        requestGO.transform.localPosition = Vector3.zero;

        var request = requestGO.GetComponent<Request>();
        var recipe  = m_recipeBook.Random();

        request.recipe  = recipe;
        request.timeout = recipe.duration * 3f;

        m_requests.Add(request);

        this.elapsed = 0f;
    }
}
