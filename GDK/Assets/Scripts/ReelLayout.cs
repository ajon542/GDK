using UnityEngine;
using System.Collections.Generic;

public class ReelLayout : MonoBehaviour
{
    [SerializeField]
    private GameObject symbolPrefab;
    [SerializeField]
    private int visibleSymbols;

    private List<GameObject> symbolObjects = new List<GameObject>();
    private AnimationCurve layoutCurve;
    private float topSymbolPosition;
    private float symbolEnterPosition;

    [SerializeField]
    private float speed = 0.003f;

    private Queue<GameObject> symbolPool = new Queue<GameObject>();

    // NOTE: PROTOTYPE CODE ONLY
    // This code is written mainly to test some ideas out. It is horrible and
    // I'm not convinced a single animation curve for the reel is the best way
    // to go at this point.

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        layoutCurve = AnimationCurve.Linear(0, 10, 1, -10);

        // Get the position of the top symbol. This will never change.
        // All other symbol positions will be calculation from this value.
        int totalSymbols = visibleSymbols;

        symbolEnterPosition = 1.0f / (totalSymbols + 1);
        topSymbolPosition = symbolEnterPosition;

        for (int i = 1; i <= totalSymbols; ++i)
        {
            float y = layoutCurve.Evaluate(i * topSymbolPosition);

            Gizmos.DrawCube(new Vector3(gameObject.transform.position.x, y, -1), Vector3.one);
        }
    }

    // TODO: Draw a gizmo for the animation curve and symbol locations.
    void Start()
    {
        if (symbolPrefab == null)
        {
            return;
        }

        for (int i = 0; i < 20; ++i)
        {
            GameObject symbol = Instantiate(symbolPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            symbol.transform.parent = gameObject.transform;
            symbol.SetActive(false);
            symbolPool.Enqueue(symbol);
        }

        layoutCurve = AnimationCurve.Linear(0, 10, 1, -10);

        // Get the position of the top symbol. This will never change.
        // All other symbol positions will be calculation from this value.
        int totalSymbols = visibleSymbols;

        symbolEnterPosition = 1.0f / (totalSymbols + 1);
        topSymbolPosition = symbolEnterPosition;

        for (int i = 1; i <= totalSymbols; ++i)
        {
            float y = layoutCurve.Evaluate(i * topSymbolPosition);

            GameObject symbol = symbolPool.Dequeue();
            symbol.transform.position = new Vector3(gameObject.transform.position.x, y, -1);
            symbol.SetActive(true);
            symbolObjects.Add(symbol);
        }
    }

    void Update()
    {
        // Calculate the position of all symbols based on the position of the top symbol.
        float symbolPosition = topSymbolPosition;

        foreach (GameObject symbol in symbolObjects)
        {
            float y = layoutCurve.Evaluate(symbolPosition);
            Vector3 currentPos = symbol.transform.position;

            // TODO: Making the assumption the curve just represents y-coord. Though, this
            // can simply be a single implementation. Other implementations can provide the
            // specifics of any sort of movement.
            currentPos.y = y;
            symbol.transform.position = currentPos;
            symbolPosition += symbolEnterPosition;
        }

        // TODO: Deal with the speed of a spin.
        // TODO: Deal with moving backwards / forwards (possibly based on user interaction).
        topSymbolPosition += speed;

        // TODO: Need to implement a pool of objects / symbols.
        // TODO: Probably want to use a linked list to be able to remove from head and tail efficiently.
        // TODO: Reduce the curve Evaluate method calls
        if (layoutCurve.Evaluate(symbolPosition - symbolEnterPosition) <= -5.0f)
        {
            // Add a new symbol to the start of the list.
            topSymbolPosition -= symbolEnterPosition;

            GameObject symbol = symbolPool.Dequeue();
            symbol.transform.position = new Vector3(gameObject.transform.position.x, layoutCurve.Evaluate(topSymbolPosition));
            symbol.SetActive(true);
            symbolObjects.Insert(0, symbol);

            // Remove the last symbol.
            symbolObjects[symbolObjects.Count - 1].SetActive(false);
            symbolPool.Enqueue(symbolObjects[symbolObjects.Count - 1]);
            symbolObjects.RemoveAt(symbolObjects.Count - 1);
        }
    }
}
