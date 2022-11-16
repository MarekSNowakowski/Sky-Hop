using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject platformObject;
    [SerializeField] private GameObject cloudObject;
    [SerializeField] private float distanceStep = 2.5f;
    [SerializeField] private float shiftStep = 1.25f;
    [SerializeField][Range(0.01f,1f)] private float additionalPlatformChance = 0.1f;
    [SerializeField] private int initialRowsToGenerate = 5;
    [SerializeField] private GameObject coinObject;
    [SerializeField] private GameObject clockObject;
    [SerializeField][Range(0.01f, 1f)] private float bonusChance = 0.25f;
    [SerializeField] private float verticalBonusShift = 0.6f;

    private bool evenRow = false;
    private int platformIndex = 2;

    private void Start()
    {
        for(int i = 0; i < initialRowsToGenerate; i++)
            GeneratePlatformBatch();
    }

    private void OnEnable()
    {
        Movement.OnJump += GeneratePlatformBatch;
    }

    private void OnDisable()
    {
        Movement.OnJump -= GeneratePlatformBatch;
    }

    public void GeneratePlatformBatch()
    {
        int rowsToGenerate = evenRow ? transform.childCount - 1 : transform.childCount;

        for (int i = 0; i < rowsToGenerate; i++)
        {
            bool generateAdditionalPlatform = (i != 0 && i == platformIndex - 1 && Random.value < additionalPlatformChance)
                || (i != rowsToGenerate && i == platformIndex + 1 && Random.value < additionalPlatformChance);
            bool generatePlatform = i == platformIndex || generateAdditionalPlatform;      // Determine if platform should be generated

            Vector2 platformPosition = transform.GetChild(i).transform.position;

            if (transform.GetChild(i).gameObject.activeInHierarchy)
                Instantiate(generatePlatform ? platformObject : cloudObject, platformPosition, Quaternion.identity);
            if (generateAdditionalPlatform && Random.value < bonusChance)
                GenerateBonus(platformPosition);
        }

        transform.position = new Vector2(transform.position.x + (evenRow ? -shiftStep : shiftStep), transform.position.y + distanceStep);
        ChangePlatformIndex(rowsToGenerate);
        evenRow = !evenRow;
    }

    private void ChangePlatformIndex(int rowsToGenerate)
    {
        if (platformIndex == 0)
            platformIndex++;
        else if (platformIndex == (evenRow ? rowsToGenerate : rowsToGenerate-1))
            platformIndex--;
        else
        {
            if (evenRow)
            {
                if (Random.value < 0.5f)
                    platformIndex++;
            }
            else
            {
                if (Random.value < 0.5f)
                    platformIndex--;
            }
        }
    }

    private void GenerateBonus(Vector2 platformPosition)
    {
        Instantiate(Random.value < 0.5f ? coinObject : clockObject, new Vector2(platformPosition.x, platformPosition.y + verticalBonusShift), Quaternion.identity);
    }
}
