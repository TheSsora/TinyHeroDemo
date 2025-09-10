using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerController player; 
    [SerializeField] private EnemyController enemy;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!enemy.IsDead)
            player.SelectNewTarget(enemy);
    }
}
