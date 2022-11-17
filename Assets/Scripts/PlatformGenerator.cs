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
    private int rowIndex = 0;

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
        int elementsToGenerate = evenRow ? transform.childCount - 1 : transform.childCount;

        for (int i = 0; i < elementsToGenerate; i++)
        {
            // Determine if platform should be generated
            bool generateAdditionalPlatform = (i != 0 && i == platformIndex - 1 && Random.value < additionalPlatformChance)
                || (i != elementsToGenerate && i == platformIndex + 1 && Random.value < additionalPlatformChance);  // Platforms not on the path
            bool generatePlatform = i == platformIndex || generateAdditionalPlatform;      

            Vector2 platformPosition = transform.GetChild(i).transform.position;

            // Generate platforms and clouds
            if (transform.GetChild(i).gameObject.activeInHierarchy)
                Instantiate(generatePlatform ? platformObject : cloudObject, platformPosition, Quaternion.identity);

            // Generate bonus (time or coin)
            bool generateBonus = generatePlatform && rowIndex != 0 &&
                Random.value < bonusChance * (generateAdditionalPlatform ? 2 : 1); // Double chance for loot on bonus platform
            if (generateBonus)  
                GenerateBonus(platformPosition);    
        }

        transform.position = new Vector2(transform.position.x + (evenRow ? -shiftStep : shiftStep), transform.position.y + distanceStep);
        ChangePlatformIndex(elementsToGenerate);
        evenRow = !evenRow;
        rowIndex++;
    }

    /// <summary>
    /// Change next row's position of main platform assuring there's a constant path
    /// </summary>
    private void ChangePlatformIndex(int elementsToGenerate)
    {
        if (platformIndex == 0)
            platformIndex++;
        else if (platformIndex == (evenRow ? elementsToGenerate : elementsToGenerate - 1))
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
        Instantiate(Random.value < 0.5f ? coinObject : clockObject,
            new Vector2(platformPosition.x, platformPosition.y + verticalBonusShift), Quaternion.identity);
    }
}
