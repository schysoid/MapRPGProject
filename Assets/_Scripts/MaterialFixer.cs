using UnityEngine;
using UnityEngine.UI;
public class MaterialFixer : MonoBehaviour
{
    public Image canvasElement;
    // When the game starts,
    void Awake()
    {
       
        // Add an Image component to this GameObject
         canvasElement = gameObject.AddComponent<Image>();
        // and change the Image's material, shared between all canvas elements, back to white.
        canvasElement.material.color = Color.white;
    }
}