using UnityEngine;

public class InputController
{
    private readonly GrabService _grabService;
    private readonly ExplosionService _explosionService;

    private int _leftMouseButton = 0;
    private int _rightMouseButton = 1;

    public InputController(GrabService grabService, ExplosionService explosionService)
    {
        _grabService = grabService;
        _explosionService = explosionService;
    }

    public void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            _grabService.OnGrab(Input.mousePosition);
        }

        if (Input.GetMouseButton(_leftMouseButton))
        {
            _grabService.OnHold(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(_leftMouseButton))
        {
            _grabService.OnRelease();
        }

        if (Input.GetMouseButtonDown(_rightMouseButton))
        {
            _explosionService.ApplyExplosion(Input.mousePosition);
        }
    }
}