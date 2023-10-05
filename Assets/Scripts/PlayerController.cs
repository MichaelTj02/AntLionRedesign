using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;

    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap wallsTilemap;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        controls.Walk.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (canMove(direction))
        {
            transform.position += ((Vector3)direction*0.16f);
        }
    }
 
    private bool canMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction*0.16f);
        if (!groundTilemap.HasTile(gridPosition) || wallsTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
}
