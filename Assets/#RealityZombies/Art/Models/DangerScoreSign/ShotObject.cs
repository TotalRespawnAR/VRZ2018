using UnityEngine;

public class ShotObject : MonoBehaviour, IShootable
{//MonoBehaviour {

    bool wasShot = false;
    public Transform _transform;
    public new Transform transform
    {
        get
        {
            if (_transform == null)
            {
                _transform = GetComponent<Transform>();
            }
            return _transform;
        }
    }

    public Renderer myRenderer;

    public GameObject replacementPrefab;
    public GameObject particlesPrefab;

    protected GameObject replacement;
    protected GameObject particles;

    public bool givesScore = true;

    //TODO: hookup sound
    public AudioClip sound;

    // Use this for initialization
    //void Start () {
    public virtual void Shot()
    {

    }
    public void Reset()
    {
        if (myRenderer != null)
            myRenderer.enabled = true;
        if (replacement != null)
            replacement.SetActive(false);

    }

    public void Shot(Bullet argBullet)
    {
        if (wasShot) return;
        if (replacement == null)
        {
            if (replacementPrefab != null)
            {
                replacement = Instantiate(replacementPrefab, transform.position, transform.rotation, transform);
                replacement.transform.localScale = transform.localScale;
            }
        }
        else
            replacement.SetActive(true);
        if (myRenderer == null)
            myRenderer = GetComponent<Renderer>();
        if (myRenderer != null)
            myRenderer.enabled = false;
        if (particles == null && particlesPrefab != null)
        {
            //particles = Instantiate(particlesPrefab, transform.position, transform.rotation, transform.parent);
            particles = Instantiate(particlesPrefab, transform.position, transform.rotation, transform);
            //particles = Instantiate(particlesPrefab, Vector3.zero, Quaternion.identity, transform);
            //particles = Instantiate(particlesPrefab, transform.localPosition, transform.localRotation, transform);
            Destroy(particles, 5f);
        }
        wasShot = true;
        PlayHeadShotSound.Instance.PlayScoreSound("_BulbBreak");
        GameManager.Instance.AirHornEffect();
    }

    public void aimed(bool argTF)
    {
        throw new System.NotImplementedException();
    }
}
