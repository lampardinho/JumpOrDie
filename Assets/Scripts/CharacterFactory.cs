using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private GameObject _circlePrefab;
    [SerializeField] private GameObject _squarePrefab;
    [SerializeField] private GameObject _trianglePrefab;

    public BaseCharacter CreateCharacter(CharacterType type)
    {
        GameObject selectedPrefab;
        switch (type)
        {
            case CharacterType.Circle:
                selectedPrefab = _circlePrefab;
                break;
            case CharacterType.Square:
                selectedPrefab = _squarePrefab;
                break;
            case CharacterType.Triangle:
                selectedPrefab = _trianglePrefab;
                break;
            default:
                return null;
        }
        return Instantiate(selectedPrefab).GetComponent<BaseCharacter>();
    }
}
