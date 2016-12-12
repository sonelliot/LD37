using UnityEngine;
using System.Collections;

public class MixingTable : MonoBehaviour, IContainer
{
    private SpriteRenderer m_rendererA;
    private SpriteRenderer m_rendererB;
    private SpriteRenderer m_rendererMixture;
    private GameObject m_bar;

    public Ingredient a;
    public Ingredient b;
    public Combination combination;
    public float elapsed = 0f;

    public float Progress
    {
        get
        {
            if (this.combination == null)
                return 0f;

            return this.elapsed / this.combination.duration;
        }
    }

    public bool IsDone
    {
        get { return Progress >= 1f; }
    }

    public void Interact(Hand hand)
    {
        if (hand.Holding<Ingredient>())
        {
            if (this.a == Ingredient.Unknown)
            {
                this.a = hand.Give<Ingredient>();
                FormCombination();
            }
            else if (this.b == Ingredient.Unknown)
            {
                this.b = hand.Give<Ingredient>();
                FormCombination();
            }
        }

        if (hand.IsEmpty && IsDone)
        {
            hand.thing = this.combination.mix;
            Reset();
        }
    }

    public void Start()
    {
        m_bar = transform.Find("Progress").gameObject;

        var progress = m_bar.GetComponent<ProgressBar>();
        progress.percent = () => Progress;

        m_rendererA = transform.Find("Ingredient A")
            .gameObject
            .GetComponent<SpriteRenderer>();

        m_rendererB = transform.Find("Ingredient B")
            .gameObject
            .GetComponent<SpriteRenderer>();

        m_rendererMixture = transform.Find("Ingredient Mixture")
            .gameObject
            .GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        UpdateIngredients();
        UpdateMixing();
    }

    public void Reset()
    {
        this.a = Ingredient.Unknown;
        this.b = Ingredient.Unknown;
        this.combination = null;
        this.elapsed = 0f;
    }

    private void FormCombination()
    {
        if (this.a != Ingredient.Unknown &&
            this.b != Ingredient.Unknown)
        {
            var combination = this.a.Combine(this.b);
            if (combination != null)
            {
                this.combination = combination;
            }
            else
            {
                Reset();
            }
        }
    }

    private void UpdateMixing()
    {
        if (this.combination == null)
        {
            m_bar.SetActive(false);
            this.elapsed = 0f;
        }
        else
        {
            m_bar.SetActive(true);
            this.elapsed += Time.deltaTime;
        }
    }

    private void UpdateIngredients()
    {
        m_rendererA.enabled = this.a != Ingredient.Unknown && !IsDone;
        m_rendererB.enabled = this.b != Ingredient.Unknown && !IsDone;
        m_rendererMixture.enabled = IsDone;
        m_rendererA.color = this.a.GetColor();
        m_rendererB.color = this.b.GetColor();

        if (this.combination != null)
        {
            m_rendererMixture.color = this.combination.mix.GetColor();
        }
    }
}
