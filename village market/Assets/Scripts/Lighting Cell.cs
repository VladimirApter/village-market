using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class LightingCell : MonoBehaviour
{
    public GameObject LightingCellPrefab;

    private GameObject b;
    // Start is called before the first frame update
    void Start()
    {

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var coordinates =
            SquareSection.ConvertVectorToSection(mousePosition +
                                                 new Vector3(0, SquareSection.SquareSectionScale.y / 2));
        b = Instantiate(LightingCellPrefab,
            SquareSection.ConvertSectionToVector(coordinates),
            Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var coordinates =
            SquareSection.ConvertVectorToSection(mousePosition +
                                                 new Vector3(0, SquareSection.SquareSectionScale.y / 2));
        var spriteRenderer = b.GetComponent<SpriteRenderer>();

        if (!SquareSection.GetCurrentSectionCoordinates().Contains(coordinates) ||
            Vector2.Distance(SquareSection.ConvertSectionToVector(coordinates), mousePosition) >
            new Vector2(SquareSection.SquareSectionScale.x, SquareSection.SquareSectionScale.y)
                .magnitude) spriteRenderer.enabled = false;
        else spriteRenderer.enabled = true;
        b.transform.position = SquareSection.ConvertSectionToVector(coordinates);
        

        //Destroy(b);
    }
}
