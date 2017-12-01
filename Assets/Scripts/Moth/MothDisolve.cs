using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothDisolve : MonoBehaviour
{
    private Material curMaterial;
    private Renderer rend;
    private float mothDissolvedState = 1f;
    private float mothAssembledState = 0f;
    [SerializeField]
    private float assembleSpeed = 4f;
    private float mothCurrentDisolveState = 0f;
    [SerializeField]
    private Shader mothShader;
    [SerializeField]
    private ParticleSystem assembleParticle;
    private ParticleSystem particleHolder;
    private Vector3 puzzlePosition;
    private float mothAssembleTiming;
    [SerializeField]
    private float DKTiming = 5f, ENGTiming = 3.5f;

    private void OnEnable()
    {
        Puzzle.AssembleCall += AssembleEffect;
    }
    private void OnDisable()
    {
        Puzzle.AssembleCall -= AssembleEffect;
    }

    private void Start()
    {
        GetMaterial();
    }

    private void GetMaterial()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.shader = mothShader;
        }
    }

    public void AssembleEffect(Vector3 puzPos)
    {
        LocalizationItem.Language language;
        language = (LocalizationItem.Language)PlayerPrefs.GetInt("LANGUAGE");
        if (language == LocalizationItem.Language.DANISH)
        {
            mothAssembleTiming = DKTiming; //DK Timing
        }
        else if(language == LocalizationItem.Language.ENGLISH)
        {
            mothAssembleTiming = ENGTiming; // ENG Timing
        }

        puzzlePosition = puzPos;

        particleHolder = ParticleSystem.Instantiate(assembleParticle, puzzlePosition, assembleParticle.transform.rotation);
        var em = particleHolder.emission;
        em.enabled = true;

        StartCoroutine(MothMaterialisation());
    }

    IEnumerator MothMaterialisation()
    {
        float time = .0f;
        yield return new WaitForSeconds(mothAssembleTiming);
        while (time < 1)
        {
            mothCurrentDisolveState = Mathf.Lerp(mothDissolvedState, mothAssembledState, time);
            time += Time.deltaTime / assembleSpeed;
            rend.material.SetFloat("_DissolveAmount", mothCurrentDisolveState);
            yield return null;
        }
        yield return new WaitForSeconds(mothAssembleTiming + 3f);
        var em = particleHolder.emission;
        em.enabled = false;
        yield return new WaitForSeconds(4f);
        Destroy(particleHolder.gameObject);
        yield return null;
    }
}
