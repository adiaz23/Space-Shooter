using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float imageWidth;
    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        LockCameraWidthToImage();
    }

    void Update()
    {
       float substract =  speed * Time.time % imageWidth;
       transform.position = initialPosition + substract * direction;
    }

    private void LockCameraWidthToImage(){
        Camera cam = Camera.main;
        float aspectRatio = (float)Screen.width / Screen.height;
        float requireHeight = imageWidth / aspectRatio;
        cam.orthographicSize = requireHeight / 2f;
    }
}
