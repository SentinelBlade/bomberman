using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
   public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;
    

    public Explosion explosionPrefab;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;
    public LayerMask explosionLayerMask;

    public Destructible destructiblePrefab;
    public Tilemap destructibleTiles;

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }
    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }
    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        //rounding up values to keep bomb on the grid but the grid is off by 0.5...
        //position.x = Mathf.Round(position.x);
        //position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;
        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        //rounding up values to keep bomb on the grid but the grid is off by 0.5...
        //position.x = Mathf.Round(position.x);
        //position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        Destroy(explosion.gameObject, explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if(length <= 0)
        {
            return;
        }

        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one/2f, 0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length>1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        Destroy(explosion.gameObject, explosionDuration);

        Explode(position, direction, length - 1);



    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);
        if (tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
}
